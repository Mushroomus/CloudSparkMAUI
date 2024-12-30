using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSparkMAUI.Models
{
    public class Image
    {
        public string? Name { get; set; }
        public string? Icon { get; set; }
        public string? IconUrl { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }
    }
}
