using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoCollector
{
    public class Workstation
    {
        public string Name { get; set; }
        public List<Hardware> Hardwares {get;set;}
    }
}
