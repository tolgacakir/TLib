using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TLib.Device.Printer.Abstract;

namespace TLib.Device.Printer.Concrete
{
    public class ZplPrinter : IPrinter
    {
        public string Name { get; }

        public bool Connected { get; private set; }
        public string IpAddress { get; }
        public int Port { get; }

        private readonly IPEndPoint _ipEndPoint;

        public ZplPrinter(string name, string ipAddress, int port)
        {
            Name = name;
            IpAddress = ipAddress;
            Port = port;
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), Convert.ToInt32(port));

        }

        public bool Print(string data)
        {
            throw new NotImplementedException();
        }
    }
}
