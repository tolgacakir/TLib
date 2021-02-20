using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7.Rx;
using Sharp7.Rx.Enums;

namespace TLib.ManualTest
{
    public partial class FrmPlcTest3 : Form
    {
        private readonly Sharp7.Rx.Sharp7Plc _sharp7Plc;
        public FrmPlcTest3()
        {
            InitializeComponent();

            _sharp7Plc = new Sharp7Plc("192.168.1.201", 0, 0);
            _setText = SetLabel;
        }

        private async void FrmPlcTest3_Load(object sender, EventArgs e)
        {
            var result = await _sharp7Plc.InitializeAsync();


            var c = SynchronizationContext.Current;

            var data = await _sharp7Plc.GetValue<byte>("DB1.DBB1");
            var notification = _sharp7Plc.CreateNotification<byte>("DB1.DBB1", TransmissionMode.OnChange, new TimeSpan(0, 0, 1));
            var notification2 = _sharp7Plc.CreateNotification<bool>("DB1.DBX0.0", TransmissionMode.OnChange, new TimeSpan(0, 0, 1));


            await _sharp7Plc.SetValue<byte>("DB1.DBB1",99);
            

            //var observer = new S7Data(label1);
            //notification.SubscribeSafe(observer);
            //notification2.SubscribeSafe(observer);
            //_sharp7Plc.ConnectionState.SubscribeSafe(observer);


            Action<byte> act = v => label1.Invoke(_setText, v.ToString());
            notification.Subscribe(act);
            //notification2.Subscribe(v => label1.Text = v.ToString());
            //_sharp7Plc.ConnectionState.Subscribe(v => label1.Text = v.ToString());
        }

        private void SetLabel(string value)
        {
            label1.Text = value;
        }

        public delegate void SetText(string value);

        public SetText _setText;
    }

    public class S7Data : IObserver<byte>, IObserver<bool>, IObserver<ConnectionState>
    {
        private Label _label1;

        public bool Var0 { get; set; }
        public byte Var1 { get; set; }


        public S7Data(Label label1)
        {
            _label1 = label1;
        }

        private void SetLabel(bool value)
        {
            _label1.Text = value.ToString();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(byte value)
        {
            Var1 = value;
        }

        public void OnNext(bool value)
        {
            Var0 = value;

            _label1.Invoke(_setText, value);
            
        }

        public void OnNext(ConnectionState value)
        {
            MessageBox.Show(value.ToString());
        }

        public delegate void SetText(string value);

        public SetText _setText;
    }
}
