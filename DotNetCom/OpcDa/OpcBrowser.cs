using DotNetCom.General.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DotNetCom.OpcDa
{
    public partial class OpcBrowser : Form
    {
        public OPCAutomation.OPCBrowser OPCBrowser {get; set;}

        public TagLink[] SelectedItems { get; set; }

        private List<string> itIds = new List<string>();

        public OpcBrowser()
        {
            InitializeComponent();
        }

        private void OpcDaBrowser_Load(object sender, EventArgs e)
        {
            OPCBrowser.Filter = "";
            OPCBrowser.AccessRights = 1 | 2;
            OPCBrowser.ShowBranches();
            browserList.Nodes.Clear();
            for (int i = 1; i <= OPCBrowser.Count; i++)
            {
                browserList.Nodes.Add(OPCBrowser.Item(i));
            }

            if(SelectedItems != null)
            {
                foreach (var it in SelectedItems)
                {
                    var itNode = itemsToAdd.Items.Add(it.Name);
                    itNode.Tag = it;
                    itIds.Add(it.Id);
                }
            }
        }

        private void browserList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                Array path = e.Node.FullPath.Split('\\').ToArray();
                browserItems.Items.Clear();

                OPCBrowser.MoveTo(ref path);
                OPCBrowser.ShowBranches();

                if(e.Node.Nodes.Count == 0 && OPCBrowser.Count >= 1)
                {
                    for (int i = 1; i <= OPCBrowser.Count; i++)
                    {
                        e.Node.Nodes.Add(OPCBrowser.Item(i));
                    }
                }

                OPCBrowser.ShowLeafs();
                if (OPCBrowser.Count > 1)
                {
                    for (int i = 1; i <= OPCBrowser.Count; i++)
                    {
                        var it = browserItems.Items.Add(OPCBrowser.Item(i));
                        it.Tag = new TagLink()
                        {
                            Id = OPCBrowser.GetItemID(OPCBrowser.Item(i)),
                            Name = OPCBrowser.Item(i),
                        };
                    }
                }
            }
            catch (Exception)
            {
                OPCBrowser.MoveUp();
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (browserItems.SelectedIndices == null) return;
                foreach (ListViewItem item in browserItems.SelectedItems)
                {
                    var it = (TagLink)item.Tag;
                    if (itIds.Contains(it.Id)) continue;
                    itIds.Add(it.Id);
                    var list = SelectedItems?.ToList() ?? new List<TagLink>();
                    list.Add(it);
                    var itNode = itemsToAdd.Items.Add(it.Name);
                    itNode.Tag = it;
                    var tag = new Tag(GetTagName(it.Name));
                    it.TagName = tag.Name;
                    SelectedItems = list.ToArray();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
            }
        }

        string GetTagName(string itName)
        {
            string tagName = "";
            if (cbFullId.Checked)
            {
                string fullPath = OPCBrowser.CurrentPosition + '_' + itName;
                tagName = fullPath.Trim().Replace(" ", "_");
            }
            else
            {
                tagName = itName.Replace(" ", "_");
            }
            tagName = tagName.Replace(".", "_").Trim();
            tagName = tagName.Replace("\n", "_").Trim();
            tagName = Regex.Replace(tagName, @"[^0-9a-zA-Zz._]", string.Empty);
            if (cbUpperCase.Checked)
            {
                tagName = tagName.ToUpper();
            }
            return tagName;
        }

        //void RefreshSelectedItems()
        //{
        //    itemsToAdd.Items.Clear();
        //    foreach (var item in SelectedItems)
        //    {
        //        var it = itemsToAdd.Items.Add(item.Tag.Name);
        //        it.Tag = item;
        //    }
        //}

        private void itemsToAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsToAdd.SelectedItems?.Count > 0)
            {
                itemProperty.SelectedObject = itemsToAdd.SelectedItems[0].Tag;
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if(SelectedItems == null)
            {
                SelectedItems = new List<TagLink>().ToArray();
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void itemsToAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (itemsToAdd.SelectedIndices == null) return;
                foreach (ListViewItem item in itemsToAdd.SelectedItems)
                {
                    var tag = item.Tag as TagLink;
                    var list = SelectedItems.ToList();
                    list.Remove(tag);
                    SelectedItems = list.ToArray();
                    itemsToAdd.Items.Remove(item);
                }

            }
        }
    }

    public class OPCItemCollectionEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;  
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            OpcClient client = context.Instance as OpcClient;

            using(OpcBrowser form = new OpcBrowser())
            {
                client.Server.Connect();
                form.OPCBrowser = client.Server.CreateBrowser();
                form.SelectedItems = client.TagLinks;

                if(svc.ShowDialog(form) == DialogResult.OK)
                {       
                    value = form.SelectedItems;
                    foreach (var item in form.SelectedItems)
                    {
                        client.Container?.Add(item);
                        client.Container?.Add(item.Tag);
                    }
                }
            }
            client.Server.Disconnect();
            return value;
        }

        public static DialogResult ShowEditor(ref OpcClient client)
        {
            var form = new OpcBrowser();
            client.Server.Connect();
            form.OPCBrowser = client.Server.CreateBrowser();
            form.SelectedItems = client.TagLinks;

            var result = form.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (var item in form.SelectedItems)
                {
                    client.Container?.Add(item);
                    client.Container?.Add(item.Tag);
                }
                client.TagLinks = form.SelectedItems;
            }

            client.Server.Disconnect();
            return result;
        }
    }
}
