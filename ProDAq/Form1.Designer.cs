
namespace ProDAq
{
    partial class Form1
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
            this.serialText1 = new DotNetCom.Text.SerialText(this.components);
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // serialText1
            // 
            this.serialText1.BaudRate = 115200;
            this.serialText1.DataBits = 8;
            this.serialText1.Parity = System.IO.Ports.Parity.None;
            this.serialText1.Port = "COM10";
            this.serialText1.ReceivedData = null;
            this.serialText1.SplitMode = DotNetCom.Text.SplitMode.Json;
            this.serialText1.SplitString = null;
            this.serialText1.StopBits = System.IO.Ports.StopBits.One;
            this.serialText1.TagLinks = null;
            this.serialText1.Terminator = "\n";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.SelectedObject = this.serialText1;
            this.propertyGrid1.Size = new System.Drawing.Size(800, 450);
            this.propertyGrid1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private DotNetCom.Text.SerialText serialText1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
    }
}