using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Plc.Exceptions
{
    public class PlcNoConnectionException : Exception
    {
        public PlcNoConnectionException(string message="Plc connection is not OK") : base(message)
        {

        }
    }
}
