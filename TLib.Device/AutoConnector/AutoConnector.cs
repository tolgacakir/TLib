using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TLib.Device.AutoConnector
{
    public class AutoConnector
    {
        private readonly IDevice _device;
        private readonly Timer _timer;

        public AutoConnector(IDevice device, double interval = 3000.0)
        {
            _device = device;
            _timer = new Timer(interval);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: asenkron erişimde sorun çıkıyor. bu yüzden timer durdurulup tekrar başlatılıyor. farklı şekilde çözülebilir.
            _timer.Stop();

            if (!_device.Connected)
            {
                _device.Connect();
            }

            _timer.Start();
        }
    }
}
