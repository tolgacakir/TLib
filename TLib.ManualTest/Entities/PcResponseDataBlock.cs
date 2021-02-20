using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.ManualTest.Entities
{
    public class PcResponseDataBlock
    {
        public short Order { get; set; }
        public byte BoxId { get; set; }
        public string Barcode { get; set; } = "";
        public byte Response { get; set; }
    }
}
