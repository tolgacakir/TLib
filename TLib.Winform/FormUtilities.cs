using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLib.Winform
{
    public class FormUtilities
    {
        public static void CheckAppRunning(Form form)
        {
            var ProcName = Process.GetCurrentProcess().ProcessName.Replace(".vshost", "");
            if (Process.GetProcessesByName(ProcName).Count() > 1)
            {
                MessageBox.Show("This application is already running.");
                form.Close();
            }
        }

        public static void Exit()
        {
            var result = MessageBox.Show("Are you sure want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
