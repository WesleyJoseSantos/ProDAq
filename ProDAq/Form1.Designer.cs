
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
            DotNetCom.General.Tags.TagCollection tagCollection1 = new DotNetCom.General.Tags.TagCollection();
            this.btConnect = new System.Windows.Forms.Button();
            this.btDisconnect = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.btTrendStart = new System.Windows.Forms.Button();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.serialText1 = new DotNetCom.Text.SerialText(this.components);
            this.tagLink1 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink2 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink3 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tagLink4 = new DotNetCom.General.Tags.TagLink(this.components);
            this.tag1 = new DotNetCom.General.Tags.Tag();
            this.tag2 = new DotNetCom.General.Tags.Tag();
            this.tag3 = new DotNetCom.General.Tags.Tag();
            this.tag4 = new DotNetCom.General.Tags.Tag();
            this.trend = new DotNetScadaComponents.Trend.Trend();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btConnect
            // 
            this.btConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btConnect.Location = new System.Drawing.Point(232, 455);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(75, 23);
            this.btConnect.TabIndex = 1;
            this.btConnect.Text = "connect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // btDisconnect
            // 
            this.btDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btDisconnect.Location = new System.Drawing.Point(232, 483);
            this.btDisconnect.Name = "btDisconnect";
            this.btDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btDisconnect.TabIndex = 1;
            this.btDisconnect.Text = "disconnect";
            this.btDisconnect.UseVisualStyleBackColor = true;
            this.btDisconnect.Click += new System.EventHandler(this.btDisconnect_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btDisconnect);
            this.splitContainer1.Panel1.Controls.Add(this.btConnect);
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.btTrendStart);
            this.splitContainer1.Panel2.Controls.Add(this.trend);
            this.splitContainer1.Size = new System.Drawing.Size(761, 511);
            this.splitContainer1.SplitterDistance = 313;
            this.splitContainer1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(83, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "stop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btTrendStart
            // 
            this.btTrendStart.Location = new System.Drawing.Point(2, 4);
            this.btTrendStart.Name = "btTrendStart";
            this.btTrendStart.Size = new System.Drawing.Size(75, 23);
            this.btTrendStart.TabIndex = 2;
            this.btTrendStart.Text = "start";
            this.btTrendStart.UseVisualStyleBackColor = true;
            this.btTrendStart.Click += new System.EventHandler(this.btTrendStart_Click);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.SelectedObject = this.serialText1;
            this.propertyGrid.Size = new System.Drawing.Size(313, 511);
            this.propertyGrid.TabIndex = 0;
            // 
            // serialText1
            // 
            this.serialText1.BaudRate = 115200;
            this.serialText1.DataBits = 8;
            this.serialText1.Parity = System.IO.Ports.Parity.None;
            this.serialText1.Port = "COM19";
            this.serialText1.ReceivedData = "{ \"waves\":{ \"senoid\":0.496615, \"triangle\":0.700000, \"square\":1, \"bit\":true } }\r";
            this.serialText1.SplitMode = DotNetCom.Text.SplitMode.Json;
            this.serialText1.SplitString = null;
            this.serialText1.StopBits = System.IO.Ports.StopBits.One;
            this.serialText1.TagLinks = new DotNetCom.General.Tags.TagLink[] {
        this.tagLink1,
        this.tagLink2,
        this.tagLink3,
        this.tagLink4};
            this.serialText1.Terminator = "\n";
            // 
            // tagLink1
            // 
            this.tagLink1.Id = "waves.senoid";
            this.tagLink1.Name = "senoid";
            this.tagLink1.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink1.TagName = "WAVES_SENOID";
            this.tagLink1.Value = null;
            // 
            // tagLink2
            // 
            this.tagLink2.Id = "waves.triangle";
            this.tagLink2.Name = "triangle";
            this.tagLink2.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink2.TagName = "WAVES_TRIANGLE";
            this.tagLink2.Value = null;
            // 
            // tagLink3
            // 
            this.tagLink3.Id = "waves.square";
            this.tagLink3.Name = "square";
            this.tagLink3.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink3.TagName = "WAVES_SQUARE";
            this.tagLink3.Value = null;
            // 
            // tagLink4
            // 
            this.tagLink4.Id = "waves.bit";
            this.tagLink4.Name = "bit";
            this.tagLink4.Status = DotNetCom.General.Tags.TagLinkStatus.Good;
            this.tagLink4.TagName = "WAVES_BIT";
            this.tagLink4.Value = null;
            // 
            // tag1
            // 
            this.tag1.Description = null;
            this.tag1.Name = "WAVES_SENOID";
            this.tag1.Value = null;
            // 
            // tag2
            // 
            this.tag2.Description = null;
            this.tag2.Name = "WAVES_TRIANGLE";
            this.tag2.Value = null;
            // 
            // tag3
            // 
            this.tag3.Description = null;
            this.tag3.Name = "WAVES_SQUARE";
            this.tag3.Value = null;
            // 
            // tag4
            // 
            this.tag4.Description = null;
            this.tag4.Name = "WAVES_BIT";
            this.tag4.Value = null;
            // 
            // trend
            // 
            this.trend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trend.Location = new System.Drawing.Point(0, 0);
            this.trend.Name = "trend";
            this.trend.Size = new System.Drawing.Size(444, 511);
            this.trend.TabIndex = 0;
            tagCollection1.Names = new string[] {
        "WAVES_SENOID",
        "WAVES_TRIANGLE",
        "WAVES_SQUARE",
        "WAVES_BIT"};
            this.trend.TagsCollection = tagCollection1;
            this.trend.TimeBase = 50;
            this.trend.UpdateOnDataChanged = false;
            this.trend.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trend_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 511);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.Button btDisconnect;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DotNetScadaComponents.Trend.Trend trend;
        private System.Windows.Forms.Button btTrendStart;
        private System.Windows.Forms.Button button1;
        private DotNetCom.Text.SerialText serialText1;
        private DotNetCom.General.Tags.TagLink tagLink1;
        private DotNetCom.General.Tags.TagLink tagLink2;
        private DotNetCom.General.Tags.TagLink tagLink3;
        private DotNetCom.General.Tags.TagLink tagLink4;
        private DotNetCom.General.Tags.Tag tag1;
        private DotNetCom.General.Tags.Tag tag2;
        private DotNetCom.General.Tags.Tag tag3;
        private DotNetCom.General.Tags.Tag tag4;
    }
}