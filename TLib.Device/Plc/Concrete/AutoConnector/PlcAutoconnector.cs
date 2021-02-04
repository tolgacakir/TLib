using System.Timers;
using TLib.Device.Plc.Abstract;

namespace TLib.Device.Plc.Concrete.AutoConnector
{
    //TODO: sadece plc için değil, tüm device'lar için kullanılabilir.
    public class PlcAutoConnector
    {
        private readonly IPlcClient _plcClient;
        private readonly Timer _timer;

        public PlcAutoConnector(IPlcClient plcClient, double interval = 3000.0)
        {
            _plcClient = plcClient;
            _timer = new Timer(interval);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //TODO: asenkron erişimde sorun çıkıyor. bu yüzden timer durdurulup tekrar başlatılıyor. farklı şekilde çözülebilir.
            _timer.Stop();

            if (! _plcClient.ConnectionStatus)
            {
                _plcClient.Connect();
            }

            _timer.Start();
        }
    }
}