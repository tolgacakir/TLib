 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Plc.Concrete.Sharp7
{
    public class DataMap
    {
        public string PropertyName { get; set; }
        public int Address { get; set; }
        public int ByteSize { get; set; }
    }
}
