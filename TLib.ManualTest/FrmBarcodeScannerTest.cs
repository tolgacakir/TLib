using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLib.Core.Logging;
using TLib.Device.BarcodeScanner.Abstract;
using TLib.Device.BarcodeScanner.Concrete.Json;
using TLib.Device.BarcodeScanner.Concrete.SckBarcodeScanner;

namespace TLib.ManualTest
{
    public partial class FrmBarcodeScannerTest : Form
    {
        private readonly IBarcodeScanner _barcodeScanner;

        public FrmBarcodeScannerTest()
        {
            InitializeComponent();
            _barcodeScanner = new SckBarcodeScanner("", "192.168.1.123", 2112, this, LoggerFactory.GetSerilogFileLogger());
            _barcodeScanner.BarcodeScanned += _barcodeScanner_BarcodeScanned;
            _barcodeScanner.BarcodeCouldNotScanned += _barcodeScanner_BarcodeCouldNotScanned;
            _barcodeScanner.Connect();
        }

        private void _barcodeScanner_BarcodeCouldNotScanned(object sender, BarcodeEventArgs e)
        {
            listBox1.Items.Insert(0, "NoRead!");
        }

        private void _barcodeScanner_BarcodeScanned(object sender, BarcodeEventArgs e)
        {
            listBox1.Items.Insert(0, e.Barcode);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(_barcodeScanner.Connected)
            {
                BackColor = Color.LightGreen;
            }
            else
            {
                BackColor = Color.IndianRed;
            }
        }
    }
}