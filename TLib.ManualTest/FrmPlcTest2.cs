using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using S7;
using S7.Net;
using S7.Net.Types;

namespace TLib.ManualTest
{
    public partial class FrmPlcTest2 : Form
    {
        private readonly Plc _plc;
        public FrmPlcTest2()
        {
            InitializeComponent();
            _plc = new Plc(CpuType.S71500, "192.168.1.201", 0, 1);
             
        }

        private async void FrmPlcTest2_Load(object sender, EventArgs e)
        {
            await _plc.OpenAsync();
            if (_plc.IsConnected)
            {
                var var0 = new DataItem
                {
                    DB = 1,
                    BitAdr = 0,
                    Count = 1,
                    StartByteAdr = 0,
                    Value = new object(),
                    DataType = DataType.DataBlock,
                    VarType = VarType.Bit,
                };
                #region ReadMultipleVars
                //var var1 = new DataItem
                //{
                //    DB = 1,
                //    BitAdr = 0,
                //    Count = 1,
                //    StartByteAdr = 1,
                //    Value = new object(),
                //    DataType = DataType.DataBlock,
                //    VarType = VarType.Byte,
                //};
                //var var2 = new DataItem
                //{
                //    DB = 1,
                //    BitAdr = 0,
                //    Count = 10,
                //    StartByteAdr = 2,
                //    Value = new object(),
                //    DataType = DataType.DataBlock,
                //    VarType = VarType.String,
                //};

                //var dataItemsRead = new List<DataItem>
                //{
                //    var0,
                //    var1,
                //    var2
                //};

                //var readed = await _plc.ReadMultipleVarsAsync(dataItemsRead);
                //MessageBox.Show(readed[2].Value.ToString());
                #endregion

                var db = new TestDataBlock
                {
                    Var0 = true,
                    Var1 = 5,
                    Var2 = new byte[] { 1,2,3,4,5,6,7,8,9,10,11 },
                };
                await _plc.ReadClassAsync(db, 1);
                //var str = Encoding.UTF8.GetString(db.Var2, 0, db.Var2.Length);
                //MessageBox.Show(str);
            }
        }
    }

    public class TestDataBlock
    {
        public bool Var0 { get; set; }
        public byte Var1 { get; set; }
        public byte[] Var2 { get; set; } = new byte[11];

        public string Var2String => Encoding.ASCII.GetString(Var2);

        public TestDataBlock()
        {

        }
    }
}
