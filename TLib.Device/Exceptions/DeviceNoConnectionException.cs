using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.Plc.Exceptions
{
    public class DeviceNoConnectionException : Exception
    {
        public DeviceNoConnectionException(string message="The device connection is NOT OK") : base(message)
        {

        }
    }
}
