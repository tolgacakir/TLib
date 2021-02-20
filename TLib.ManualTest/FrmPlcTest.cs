using Serilog;
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
using TLib.ManualTest.DataAccessLayer.Abstract;
using TLib.ManualTest.DataAccessLayer.Concrete.Json;
using TLib.ManualTest.DataAccessLayer.Concrete.S7;
using TLib.ManualTest.Entities;

namespace TLib.ManualTest
{
    public partial class FrmPlcTest : Form
    {
        private readonly IDummyPlcClient _dummyPlcClient;
        private readonly ILogger _logger;
        private readonly object _lock;

        public FrmPlcTest()
        {
            InitializeComponent();

            _logger = LoggerFactory.GetSerilogFileLogger();
            //_dummyPlcClient = new JsonDummyPlcClient();
            _dummyPlcClient = new S7DummyPlcClient(1, "192.168.1.201",true);
            var connected = _dummyPlcClient.Connect();
            _lock = new object();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            lock (_lock)
            {
                var result = _dummyPlcClient.GetPlcRequest(out PlcRequestDataBlock plcRequest);
                if (result)
                {
                    txtPlcRun.Text = plcRequest.PlcRun.ToString();
                }
                else
                {
                    MessageBox.Show("Unsuccessfull");
                }
            }
            
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            var pcResponse = byte.Parse(txtPcResponse.Text);
            var pcResponseDataBlock = new PcResponseDataBlock
            {
                Response = pcResponse
            };

            var result = _dummyPlcClient.SetPcResponse(in pcResponseDataBlock);
            if (!result)
            {
                MessageBox.Show("Unsuccessfull");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (_lock)
            {
                if (_dummyPlcClient.Connected)
                {
                    BackColor = Color.LightGreen;
                }
                else
                {
                    BackColor = Color.IndianRed;
                }

                try
                {
                    var result = _dummyPlcClient.GetPlcRequest(out PlcRequestDataBlock plcRequest);
                    if (result)
                    {
                        txtPlcRun.Text = plcRequest.PlcRun.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Unsuccessfull");
                    }

                    var response = new PcResponseDataBlock
                    {
                        Order = (short)plcRequest.PlcRequests[2].Order,
                        BoxId = plcRequest.PlcRequests[2].BoxId,
                        Barcode = plcRequest.PlcRequests[2].Barcode,
                        Response = 5,
                    };
                    _dummyPlcClient.SetPcResponse(in response);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                }
            }
            
            
        }
    }
}
