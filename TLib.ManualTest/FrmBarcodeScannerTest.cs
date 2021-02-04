using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLib.Device.BarcodeScanner.Abstract;
using TLib.Device.BarcodeScanner.Concrete.Json;

namespace TLib.ManualTest
{
    public partial class FrmBarcodeScannerTest : Form
    {
        private readonly IBarcodeScanner _barcodeScanner;
        public FrmBarcodeScannerTest()
        {
            InitializeComponent();
            _barcodeScanner = new JsonBarcodeScanner();
            _barcodeScanner.BarcodeScanned += _barcodeScanner_BarcodeScanned;
            _barcodeScanner.BarcodeCouldNotScanned += _barcodeScanner_BarcodeCouldNotScanned;
            _barcodeScanner.Connect();
        }

        private void _barcodeScanner_BarcodeCouldNotScanned(object sender, BarcodeEventArgs e)
        {
            MessageBox.Show("read error!");
        }

        private void _barcodeScanner_BarcodeScanned(object sender, BarcodeEventArgs e)
        {
            MessageBox.Show("Barcode: " + e.Barcode);
        }
    }
}