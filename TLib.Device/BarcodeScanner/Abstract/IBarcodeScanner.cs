using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLib.Device.BarcodeScanner.Abstract
{
    public interface IBarcodeScanner
    {
        string Barcode { get; }
        bool Connect();
        void Disconnect();
        bool ConnectionStatus { get; }

        event BarcodeScannedEventHandler BarcodeScanned;
        event BarcodeCouldNotScannedEventHandler BarcodeCouldNotScanned;
    }

    public delegate void BarcodeScannedEventHandler(object sender, BarcodeEventArgs e);
    public delegate void BarcodeCouldNotScannedEventHandler(object sender, BarcodeEventArgs e);


    public class BarcodeEventArgs : EventArgs
    {
        public string Barcode { get; }
        public string ScannerId { get; set; }
        public BarcodeEventArgs(string barcode, string scannerId)
        {
            Barcode = barcode;
            ScannerId = scannerId;
        }
    }
}
