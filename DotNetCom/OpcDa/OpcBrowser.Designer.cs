
namespace DotNetCom.OpcDa
{
    partial class OpcBrowser
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
            this.browserList = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbFullId = new System.Windows.Forms.CheckBox();
            this.cbUpperCase = new System.Windows.Forms.CheckBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.browserItems = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btOk = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.itemsToAdd = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemProperty = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // browserList
            // 
            this.browserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browserList.Location = new System.Drawing.Point(12, 43);
            this.browserList.Name = "browserList";
            this.browserList.Size = new System.Drawing.Size(369, 253);
            this.browserList.TabIndex = 0;
            this.browserList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.browserList_AfterSelect);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbFullId);
            this.splitContainer1.Panel1.Controls.Add(this.cbUpperCase);
            this.splitContainer1.Panel1.Controls.Add(this.btAdd);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.browserItems);
            this.splitContainer1.Panel1.Controls.Add(this.browserList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btOk);
            this.splitContainer1.Panel2.Controls.Add(this.btCancel);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.itemsToAdd);
            this.splitContainer1.Panel2.Controls.Add(this.itemProperty);
            this.splitContainer1.Size = new System.Drawing.Size(844, 556);
            this.splitContainer1.SplitterDistance = 394;
            this.splitContainer1.TabIndex = 2;
            // 
            // cbFullId
            // 
            this.cbFullId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbFullId.AutoSize = true;
            this.cbFullId.Location = new System.Drawing.Point(14, 531);
            this.cbFullId.Name = "cbFullId";
            this.cbFullId.Size = new System.Drawing.Size(143, 17);
            this.cbFullId.TabIndex = 6;
            this.cbFullId.Text = "Use full Id to Tag Names";
            this.cbFullId.UseVisualStyleBackColor = true;
            // 
            // cbUpperCase
            // 
            this.cbUpperCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbUpperCase.AutoSize = true;
            this.cbUpperCase.Checked = true;
            this.cbUpperCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbUpperCase.Location = new System.Drawing.Point(14, 510);
            this.cbUpperCase.Name = "cbUpperCase";
            this.cbUpperCase.Size = new System.Drawing.Size(158, 17);
            this.cbUpperCase.TabIndex = 5;
            this.cbUpperCase.Text = "UPPER_CASE Tag Names ";
            this.cbUpperCase.UseVisualStyleBackColor = true;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btAdd.Location = new System.Drawing.Point(306, 517);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(75, 23);
            this.btAdd.TabIndex = 4;
            this.btAdd.Text = "Add";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Available Items";
            // 
            // browserItems
            // 
            this.browserItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browserItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.browserItems.HideSelection = false;
            this.browserItems.Location = new System.Drawing.Point(12, 302);
            this.browserItems.Name = "browserItems";
            this.browserItems.Size = new System.Drawing.Size(369, 200);
            this.browserItems.TabIndex = 2;
            this.browserItems.UseCompatibleStateImageBehavior = false;
            this.browserItems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Items";
            this.columnHeader1.Width = 326;
            // 
            // btOk
            // 
            this.btOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOk.Location = new System.Drawing.Point(277, 517);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(75, 23);
            this.btOk.TabIndex = 6;
            this.btOk.Text = "OK";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.Location = new System.Drawing.Point(358, 517);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 5;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Selected Items";
            // 
            // itemsToAdd
            // 
            this.itemsToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsToAdd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.itemsToAdd.HideSelection = false;
            this.itemsToAdd.Location = new System.Drawing.Point(13, 43);
            this.itemsToAdd.Name = "itemsToAdd";
            this.itemsToAdd.Size = new System.Drawing.Size(419, 253);
            this.itemsToAdd.TabIndex = 3;
            this.itemsToAdd.UseCompatibleStateImageBehavior = false;
            this.itemsToAdd.View = System.Windows.Forms.View.Details;
            this.itemsToAdd.SelectedIndexChanged += new System.EventHandler(this.itemsToAdd_SelectedIndexChanged);
            this.itemsToAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.itemsToAdd_KeyDown);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Name";
            this.columnHeader2.Width = 326;
            // 
            // itemProperty
            // 
            this.itemProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemProperty.Location = new System.Drawing.Point(13, 302);
            this.itemProperty.Name = "itemProperty";
            this.itemProperty.Size = new System.Drawing.Size(419, 200);
            this.itemProperty.TabIndex = 0;
            // 
            // OpcBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 556);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OpcBrowser";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpcDaBrowser";
            this.Load += new System.EventHandler(this.OpcDaBrowser_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView browserList;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView browserItems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView itemsToAdd;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.PropertyGrid itemProperty;
        private System.Windows.Forms.CheckBox cbUpperCase;
        private System.Windows.Forms.CheckBox cbFullId;
    }
}