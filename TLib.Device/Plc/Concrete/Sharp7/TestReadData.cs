using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Plc.Abstract;

namespace TLib.Device.Plc.Concrete.Sharp7
{
    public class TestReadData : IPlcData
    {
        public byte PlcRun { get; set; }
        public string ProductNumber { get; set; }
    }
}
