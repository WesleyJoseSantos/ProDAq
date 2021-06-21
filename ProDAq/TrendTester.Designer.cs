
namespace ProDAq
{
    partial class TrendTester
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
            DotNetCom.General.Tags.TagCollection tagCollection1 = new DotNetCom.General.Tags.TagCollection();
            DotNetCom.General.Controls.ComponentConsole componentConsole1 = new DotNetCom.General.Controls.ComponentConsole();
            DotNetCom.General.Controls.ComponentConsole componentConsole2 = new DotNetCom.General.Controls.ComponentConsole();
            this.trend1 = new DotNetScadaComponents.Trend.Trend();
            this.opcServerConnection1 = new DotNetCom.OpcDa.OpcServerConnection(this.components);
            this.opcClient1 = new DotNetCom.OpcDa.OpcClient(this.components);
            this.tagLink1 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink2 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink3 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink4 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink5 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tag1 = new DotNetCom.General.Tags.Tag();
            this.tag2 = new DotNetCom.General.Tags.Tag();
            this.tag3 = new DotNetCom.General.Tags.Tag();
            this.tag4 = new DotNetCom.General.Tags.Tag();
            this.tagExplorer1 = new DotNetCom.General.Tags.TagExplorer(this.components);
            this.tag5 = new DotNetCom.General.Tags.Tag();
            this.SuspendLayout();
            // 
            // trend1
            // 
            this.trend1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trend1.Location = new System.Drawing.Point(0, 0);
            this.trend1.Name = "trend1";
            this.trend1.Size = new System.Drawing.Size(624, 450);
            this.trend1.TabIndex = 0;
            tagCollection1.Names = new string[] {
        "INT1",
        "INT2",
        "INT4",
        "BOOLEAN",
        "SQUARE_WAVES_BOOLEAN"};
            this.trend1.TagsCollection = tagCollection1;
            this.trend1.TimeBase = 100;
            // 
            // opcServerConnection1
            // 
            this.opcServerConnection1.Action = DotNetCom.OpcDa.OPCServerConnectionAction.None;
            this.opcServerConnection1.ClientName = null;
            componentConsole1.Errors = null;
            componentConsole1.Log = null;
            this.opcServerConnection1.Console = componentConsole1;
            this.opcServerConnection1.LocaleID = 2048;
            this.opcServerConnection1.ServerName = "Matrikon.OPC.Simulation.1";
            this.opcServerConnection1.ServerNode = null;
            // 
            // opcClient1
            // 
            componentConsole2.Errors = null;
            componentConsole2.Log = null;
            this.opcClient1.Console = componentConsole2;
            this.opcClient1.GroupName = null;
            this.opcClient1.Server = this.opcServerConnection1;
            this.opcClient1.TagLinks = new DotNetCom.General.Tags.TagLink[] {
        this.tagLink1,
        this.tagLink2,
        this.tagLink3,
        this.tagLink4,
        this.tagLink5};
            this.opcClient1.UpdateTime = 100;
            // 
            // tagLink1
            // 
            this.tagLink1.Id = "Triangle Waves.Int1";
            this.tagLink1.Name = "Int1";
            this.tagLink1.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink1.TagName = "INT1";
            this.tagLink1.Value = null;
            // 
            // tagLink2
            // 
            this.tagLink2.Id = "Triangle Waves.Int2";
            this.tagLink2.Name = "Int2";
            this.tagLink2.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink2.TagName = "INT2";
            this.tagLink2.Value = null;
            // 
            // tagLink3
            // 
            this.tagLink3.Id = "Triangle Waves.Int4";
            this.tagLink3.Name = "Int4";
            this.tagLink3.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink3.TagName = "INT4";
            this.tagLink3.Value = null;
            // 
            // tagLink4
            // 
            this.tagLink4.Id = "Random.Boolean";
            this.tagLink4.Name = "Boolean";
            this.tagLink4.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink4.TagName = "BOOLEAN";
            this.tagLink4.Value = null;
            // 
            // tagLink5
            // 
            this.tagLink5.Id = "Square Waves.Boolean";
            this.tagLink5.Name = "Boolean";
            this.tagLink5.Status = DotNetCom.General.Tags.TagLinkStatus.Unknown;
            this.tagLink5.TagName = "SQUARE_WAVES_BOOLEAN";
            this.tagLink5.Value = null;
            // 
            // tag1
            // 
            this.tag1.Description = null;
            this.tag1.Name = "INT1";
            this.tag1.Value = null;
            // 
            // tag2
            // 
            this.tag2.Description = null;
            this.tag2.Name = "INT2";
            this.tag2.Value = null;
            // 
            // tag3
            // 
            this.tag3.Description = null;
            this.tag3.Name = "INT4";
            this.tag3.Value = null;
            // 
            // tag4
            // 
            this.tag4.Description = null;
            this.tag4.Name = "BOOLEAN";
            this.tag4.Value = null;
            // 
            // tag5
            // 
            this.tag5.Description = null;
            this.tag5.Name = "SQUARE_WAVES_BOOLEAN";
            this.tag5.Value = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 450);
            this.Controls.Add(this.trend1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DotNetCom.OpcDa.OpcServerConnection opcServerConnection1;
        private DotNetCom.OpcDa.OpcClient opcClient1;
        private DotNetCom.General.Tags.TagLink tagLink1;
        private DotNetCom.General.Tags.TagLink tagLink2;
        private DotNetCom.General.Tags.TagLink tagLink3;
        private DotNetCom.General.Tags.TagLink tagLink4;
        private DotNetCom.General.Tags.Tag tag1;
        private DotNetCom.General.Tags.Tag tag2;
        private DotNetCom.General.Tags.Tag tag3;
        private DotNetCom.General.Tags.Tag tag4;
        private DotNetScadaComponents.Trend.Trend trend1;
        private DotNetCom.General.Tags.TagExplorer tagExplorer1;
        private DotNetCom.General.Tags.TagLink tagLink5;
        private DotNetCom.General.Tags.Tag tag5;
    }
}