using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TLib.ManualTest.DataAccessLayer.Abstract;
using TLib.ManualTest.DataAccessLayer.Concrete.Json;
using TLib.ManualTest.DataAccessLayer.Concrete.S7;
using TLib.ManualTest.Entities;

namespace TLib.ManualTest
{
    public partial class FrmPlcTest : Form
    {
        private readonly IDummyPlcClient _dummyPlcClient;
        public FrmPlcTest()
        {
            InitializeComponent();

            _dummyPlcClient = new JsonDummyPlcClient();
            //_dummyPlcClient = new S7DummyPlcClient(0, "0.0.0.0"); for S7 PLC
            _dummyPlcClient.Connect();

        }

        private void btnGet_Click(object sender, EventArgs e)
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
    }
}
