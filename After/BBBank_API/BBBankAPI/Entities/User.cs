using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : BaseEntity // Inheriting from Base Entity class
    {
        // First name
        public string FirstName { get; set; }

        // Last name
        public string LastName { get; set; }

        // Email of the user
        public string Email { get; set; }

        // Profile picture or avatar
        public string ProfilePicUrl { get; set; }

        // Account attached to the user 
        public Account Account { get; set; }
    }
}