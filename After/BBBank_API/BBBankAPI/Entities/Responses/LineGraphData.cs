using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responses
{
    public class LineGraphData
    {
        public decimal TotalBalance { get; set; }
        public ICollection<string> Labels { get; set; }
        public ICollection<decimal> Figures { get; set; }

        public LineGraphData()
        {
            Labels = new List<string>();
            Figures = new List<decimal>();
        }
    }
}
