using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Test.TestEntities
{
    public class PlcRequestDataBlock
    {
        public byte PlcRun { get; set; }
        public PlcRequest[] PlcRequests { get; set; } = new PlcRequest[] { new PlcRequest() };
    }

    public class PlcRequest
    {
        public byte Order { get; set; }
        public byte BoxId { get; set; }
        public string Barcode { get; set; } = "";
    }
}
