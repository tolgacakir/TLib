using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Test.TestEntities
{
    public class PcResponse
    {
        public byte OrderId { get; set; }
        public byte CellId { get; set; }
        public string Barcode { get; set; }
        public byte Response { get; set; }
    }
}
