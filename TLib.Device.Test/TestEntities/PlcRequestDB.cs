using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Test.TestEntities
{
    public class PlcRequestDB
    {
        public byte PlcRun { get; set; }
        public PlcRequest[] PlcRequests { get; set; }
    }

    public struct PlcRequest
    {
        public byte OrderId { get; set; }
        public byte CellId { get; set; }
        public string Barcode { get; set; }
    }
}
