using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.ManualTest.Entities
{
    public class PlcRequestDataBlock
    {
        public byte PlcRun { get; set; }
        public PlcRequest[] PlcRequests { get; set; } = new PlcRequest[3];
    }

    public class PlcRequest
    {
        public int Order { get; set; }
        public byte BoxId { get; set; }
        public string Barcode { get; set; } = "";
    }
}
