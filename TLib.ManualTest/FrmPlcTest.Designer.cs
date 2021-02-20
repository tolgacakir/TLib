
namespace TLib.ManualTest
{
    partial class FrmPlcTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtPlcRun = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPcResponse = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtPlcRun
            // 
            this.txtPlcRun.Location = new System.Drawing.Point(149, 45);
            this.txtPlcRun.Name = "txtPlcRun";
            this.txtPlcRun.Size = new System.Drawing.Size(100, 20);
            this.txtPlcRun.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "PlcRun";
            // 
            // txtPcResponse
            // 
            this.txtPcResponse.Location = new System.Drawing.Point(149, 110);
            this.txtPcResponse.Name = "txtPcResponse";
            this.txtPcResponse.Size = new System.Drawing.Size(100, 20);
            this.txtPcResponse.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PcResponse";
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(255, 44);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(112, 23);
            this.btnGet.TabIndex = 2;
            this.btnGet.Text = "Get from PLC";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(255, 109);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(112, 23);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "Set to PLC";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmPlcTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 201);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPcResponse);
            this.Controls.Add(this.txtPlcRun);
            this.Name = "FrmPlcTest";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPlcRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPcResponse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Timer timer1;
    }
}

