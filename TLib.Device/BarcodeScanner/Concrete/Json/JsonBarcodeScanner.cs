﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TLib.Device.BarcodeScanner.Abstract;
using TLib.Device.Plc.Exceptions;

namespace TLib.Device.BarcodeScanner.Concrete.Json
{
    public class JsonBarcodeScanner : IBarcodeScanner
    {
        public string Name { get; }
        public bool Connected { get; private set; }

        private string _barcode;
        public string Barcode 
        {
            get => _barcode;

            private set
            {
                if (!string.IsNullOrEmpty(value) && _barcode != value && value != _couldNotScannedCode)
                {
                    _barcode = value;
                    BarcodeScanned?.Invoke(this, new BarcodeEventArgs(value, Name.ToString()));
                }
                else if (_barcode != value && value == _couldNotScannedCode)
                {
                    _barcode = "";
                    WriteToFile(_barcode);
                    BarcodeCouldNotScanned?.Invoke(this, new BarcodeEventArgs(value, Name.ToString()));
                }
                else
                {
                    //TODO: invalid barcode scanned case. belki fluentValidation kullanılabilir.
                }
            }
        }

        private readonly string _readFilePath;
        private readonly Timer _timer;
        private readonly string _couldNotScannedCode;

        public event BarcodeScannedEventHandler BarcodeScanned;
        public event BarcodeCouldNotScannedEventHandler BarcodeCouldNotScanned;

        public JsonBarcodeScanner(ISynchronizeInvoke sync, string name = "1", int interval = 500, string couldNotScannedCode="NoRead")
        {
            Name = name;
            _couldNotScannedCode = couldNotScannedCode;
            _timer = new Timer(interval);
            _timer.SynchronizingObject = sync;
            _timer.Elapsed += _timer_Elapsed;

            var folder = "JsonBarcodeScanner";
            _readFilePath = $"{folder}\\{name}_BarcodeScanner.txt";


            CreateRoot(folder);
        }

        private void CreateRoot(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            if (!File.Exists(_readFilePath))
            {
                WriteToFile("");
            }
        }

        private async void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //_timer.Stop();
            Barcode = await ScanBarcodeAsync();
            //_timer.Start();
        }

        private Task<string> ScanBarcodeAsync()
        {
            return Task.Run(() =>
            {
                if (Connected)
                {
                    return File.ReadAllText(_readFilePath);
                }
                else
                {
                    throw new DeviceNoConnectionException($"The scanner connection is NOT OK. Scanner Name: {Name}");
                }
            });
        }

        private void WriteToFile(string barcode)
        {
            File.WriteAllText(_readFilePath, barcode);
        }

        public bool Connect()
        {
            Connected = true;
            _timer.Start();
            return true;
        }

        public void Disconnect()
        {
            Connected = false;
            _timer.Stop();
        }

    }
}