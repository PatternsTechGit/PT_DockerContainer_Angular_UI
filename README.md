# Containerize *Angular* WebApp using **Docker** 

### **What does Containerization mean?**

Containerization is a form of virtualization where applications run in isolated user spaces, called containers, while using the same shared operating system (OS).

### **What is Docker Container?**

Docker is an open source platform that **enables developers to build, deploy, run, update and manage containers**—standardized, executable components that combine application source code with the operating system (OS) libraries and dependencies required to run that code in any environment.

------------
## About this exercise 

### Previously:

- We developed a base structure of an api solution in Asp.net core that have just two api functions *GetLast12MonthBalances* & *GetLast12MonthBalances/{userId}* which returns data of the last 12 months total balances.
- In previous lab we have also hosted this .Net6 API in Docker container. [Read More](https://github.com/PatternsTechGit/PT_DockerContainer_.Net6_API)

    ![](/After/BBBank_API/assets/images/6.jpg)

- We also have a sample **Angular WebApp** which calls are WebApi and displays its data in form of a table.
![](/After/BBBank_API/assets/images/1.jpg) 

------------------

### In this exercise we will:

- Install Windows Subsystem for Linux (WSL)
- Install Docker for windows
- Configure *dockerfile* for our Angular UI
- Build a **docker image** of our UI
- Run container using our UI image
- Create, run, push and pull our docker image in **Docker Hub**

--------------

### **Step 1: Installing WSL:**

The Windows Subsystem for Linux (WSL) is a feature of the Windows operating system that enables you to run a Linux file system, along with Linux command-line tools and GUI apps, directly on Windows, alongside your traditional Windows desktop and apps. See the about page for more details.

- Install it by entering this command in an **administrator** PowerShell or Windows Command Prompt and then restarting your machine
    ```powershell
    wsl --install
    ```
    ***Note:** The above command only works if WSL is not installed at all, if you run `wsl --install` and see the WSL help text, please try running `wsl --list --online` to see a list of available distros and run `wsl --install -d <DistroName>` to install a distro.

----------

### **Step 2: Install Docker Desktop:**

Follow these steps to install docker:

- Download [Docker Desktop for Windows](https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe)

- Follow the usual installation instructions to install Docker Desktop. If you are running a supported system, Docker Desktop prompts you to enable WSL 2 during installation. Read the information displayed on the screen and enable WSL 2 to continue.
- Start Docker Desktop from the Windows Start menu.
- From the Docker menu, select **Settings > General**.
![](/After/BBBank_API/assets/images/2.jpg)

- Select the Use **WSL 2 based engine** check box.

- If you have installed Docker Desktop on a system that supports WSL 2, this option will be enabled by default.

- Click **Apply & Restart**.

That’s it! Now *docker commands* will work from Windows using the new WSL 2 engine.

------------  

### **Step 3: Configure *Dockerfile***


**What is `dockerfile` ?**

Docker can build images automatically by reading the instructions from a Dockerfile. A Dockerfile is a text document that contains all the commands a user could call on the command line to assemble an image.

- Here we will create two stages of docker file, one for building the image and second is to host the image

- First we will create a new file named `dockerfile`. 
- Paste the below given code in this file 
    ```docker
        #Stage 1: 
        FROM node:latest as node 
        WORKDIR /app
        COPY . . 
        RUN npm install
        RUN npm run build --prod

        #Stage: 2 
        FROM nginx:alpine
        COPY --from=node /app/dist/bbbank-ui /usr/share/nginx/html
    ```
    - Here it will use *node* image **From** where it will take all the decencies
    - Our **Working Directory** will be default docker */app* folder and it will **Copy** all the contents of default folder
    - Then it will **RUN** `npm install` command to install all node package modules.
    - Then it will **RUN** `npm build --prod` command and is used to build a package. This command will create a *dist* folder in this directory. All the contents of thi folder is all we need to host out web app. 

    - In *Stage 2* we will use engineX image to host our app
    - We will **copy** all the contents from */app/dist/bbbank-ui* this location to this location */usr/share/nginx/html* (default location of nginx to host an app)

    --------------

    ### **Step 4: Create our API's *Docker Image***

#### **What is Docker Image?**
A Docker image is a read-only template that contains a set of instructions for creating a container that can run on the Docker platform. It provides a convenient way to package up applications and preconfigured server environments, which you can use for your own private use or share publicly with other Docker users.

- To create docker image use this command 
    ```powershell
    docker build -t bbbank_api_image -f Dockerfile .
    ```
    *Note: there is a period ' . ' with a space after Dockerfile, it is not by accident and it is supposed to be like this

- To check the current working images run this command

    ```powershell
        docker images
    ```

    ![](/After/BBBank_API/assets/images/11.jpg)

    You can see our currently created image named *bbbank_api_image* up and running. You can also see the running images from **Docker Desktop** we downloaded 
    ![](/After/BBBank_API/assets/images/12.jpg)

    ----------

### **Step 5: Running a container from existing image**

#### **What are containers?**

Containers are packages of software that contain all of the necessary elements to run in any environment. In this way, containers virtualize the operating system and run anywhere, from a private data center to the public cloud or even on a developer’s personal laptop.

- To run a container use this command 
    ```powershell
    docker run -it -p 8001:80 --name {container-name} {image-name}
    ```
    - The `docker run` command **creates** a container from a given image and **starts** the container using a given command
    - `-p` it will map TCP port 80 in the container to port 8001 on the Docker host. 

- Now our api running inside container you can check it by running this command
    ```powershell
    docker ps
    ```
    ![](/After/BBBank_API/assets/images/14.jpg)
 
 Here you can see that our container is running while using the image and port we have created. 
You can also see running containers from the docker desktop as well.
- Start your API container if you have already created or [created a new API inside docker container](https://github.com/PatternsTechGit/PT_DockerContainer_.Net6_API)
- Finally you can use this URL to see you UI is running inside container on port 8001. 
http://localhost:8001
![](/After/BBBank_API/assets/images/15.jpg)

*NOTE: Make sure you allow the CORS requests in your API to allow your UI call your API. [READ MORE ABOUT CORS ERROR IN STEP 5 OF THIS LAB](https://github.com/PatternsTechGit/PT_AngularCallingAPI)

*NOTE: If you still face CORS error try to removing Caches of you DOCKER FILE. 

--------

### **Step 6: Push/Pull Docker Images into DockerHub**

- Create an account at Docker Hub (https://hub.docker.com/) to be able to push and pull Docker Hub images.
- Use this command to login to you docker hub account you created 
    ```powershell
    docker login -u your_user_name 
    ```
- Password - The prompt will request our password for DockerHub
![](/After/BBBank_API/assets/images/7.jpg)

- Before pushing our image to docker hub repository we first need to tag it by using this command
    ```powershell
        docker tag bbbank_api_image rajaastahir/bbbank_api_image
    ```
    We need to tag the image to send it to our own repository that is created when we sign up on hub.docker.com. It creates a default repository with our Docker ID.
- Finally to push the image into remote repository of docker hub use this command
    ```powershell
    docker push {user-name}/repository-name
    ```
    ![](/After/BBBank_API/assets/images/8.jpg)

- You can also see your image in Docker Hub repositories in the account we created before.
![](/After/BBBank_API/assets/images/9.jpg)
- Similarly to **pull** from your Docker Hub repository you can use this command
```powershell
    docker pull {user-name}/{Image-name}
```
- You can also directly `push` and `pull` from docker desktop app
![](/After/BBBank_API/assets/images/10.jpg)

------