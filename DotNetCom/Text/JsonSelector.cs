using DotNetCom.General.Controls;
using DotNetCom.General.Tags;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DotNetCom.Text
{
    public partial class JsonSelector : Form
    {
        private IText textInterface;

        public TagLink[] SelectedItems { get; set; }

        private List<string> itIds = new List<string>();

        private string jsonData = ""; 

        public IText TextInterface
        {
            get => textInterface;
            set
            {
                textInterface = value;
                textInterface.DataAvailable += TextInterface_DataAvailable;
            }
        }

        public JsonSelector()
        {
            InitializeComponent();
        }

        private void JsonSelector_Load(object sender, EventArgs e)
        {
            if (SelectedItems != null)
            {
                foreach (var it in SelectedItems)
                {
                    var itNode = itemsToAdd.Items.Add(it.Name);
                    itNode.Tag = it;
                    itIds.Add(it.Id);
                }
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            browserList.Nodes.Clear();
            JObject json = JObject.Parse(textInterface.ReceivedData);
            JsonTreeViewLoader.AddObjectNodes(json, "Received Data", browserList.Nodes);
        }

        private void browserList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Nodes.Count == 0)
            {
                try
                {
                    var selected = NodeToJsonItem(e.Node, jsonData);
                    itemProperty.SelectedObject = selected;
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    Log(ex.StackTrace);
                }
            }
        }

        private void browserList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var status = e.Node.Checked;
            foreach (TreeNode item in e.Node.Nodes)
            {
                item.Checked = status;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browserList.AfterCheck -= browserList_AfterCheck;
            foreach (TreeNode node in browserList.Nodes)
            {
                CheckAllNodes(node, true);
            }
            browserList.AfterCheck += browserList_AfterCheck;
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            browserList.AfterCheck -= browserList_AfterCheck;
            foreach (TreeNode node in browserList.Nodes)
            {
                CheckAllNodes(node, false);
            }
            browserList.AfterCheck += browserList_AfterCheck;
        }

        private void CheckAllNodes(TreeNode node, bool status)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = status;
                CheckAllNodes(item, status);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in browserList.Nodes)
            {
                AddAllCheckedNodes(item);
            }
        }

        private void AddAllCheckedNodes(TreeNode node)
        {
            if (node.Checked != false && node.Nodes.Count == 0)
            {
                var jsIt = NodeToJsonItem(node, jsonData);
                if (!itIds.Contains(jsIt.Path))
                {
                    itIds.Add(jsIt.Path);

                    var tag = new Tag();
                    var tagLink = JsonItemToTagLink(jsIt);
                    var list = SelectedItems?.ToList() ?? new List<TagLink>();
                    var itNode = itemsToAdd.Items.Add(tagLink.Name);
                    var upper = cbUpperCase.Checked;
                                        
                    tag.Name = cbFullId.Checked
                        ? General.Tags.Tag.GetTagName(jsIt.Path, upper)
                        : General.Tags.Tag.GetTagName(tagLink.Name, upper);
                    tagLink.TagName = tag.Name;
                    list.Add(tagLink);
                    itNode.Tag = tagLink;
                    SelectedItems = list.ToArray();
                }
            }
            foreach (TreeNode item in node.Nodes)
            {
                AddAllCheckedNodes(item);
            }
        }

        private void TextInterface_DataAvailable(object sender, EventArgs e)
        {
            string receivedData = textInterface.ReceivedData;
            Log(receivedData);
            try
            {
                if (browserList.Nodes.Count == 0)
                {
                    JObject json = JObject.Parse(receivedData);
                    jsonData = receivedData;
                    browserList.Invoke((MethodInvoker)delegate {
                        JsonTreeViewLoader.AddObjectNodes(json, "Received Data", browserList.Nodes);
                    });
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
            }

        }

        private void Log(string msg)
        {
            if (rtbReceivedData.InvokeRequired)
            {
                rtbReceivedData.Invoke((MethodInvoker)delegate
                {
                    rtbReceivedData.AppendText(msg + '\n');
                });
            }
            else
            {
                try
                {
                    rtbReceivedData.AppendText(msg + '\n');
                }
                catch (Exception)
                {

                }
            }
        }

        private JsonItem NodeToJsonItem(TreeNode node, string jsonData)
        {
            var path = node.FullPath.Replace('\\', '.').Split(':')[0];
            bool valid;
            object value;
            path = path.Replace("Received Data.", "");
            node.Tag = path;
            try
            {
                JObject json = JObject.Parse(jsonData);
                value = json.SelectToken(path);
                valid = true;
            }
            catch (Exception ex)
            {
                value = ex.Message;
                valid = false;
                throw;
            }
            var selected = new JsonItem(path, value, valid);
            return selected;
        }

        private TagLink JsonItemToTagLink(JsonItem jsonItem)
        {
            var tagLink = new TagLink()
            {
                Id = jsonItem.Path,
                Name = jsonItem.Path.Split('.').ToList().Last(),
                Value = jsonItem.Value,
                Status = jsonItem.Valid ? TagLinkStatus.Good : TagLinkStatus.Bad
            };
            return tagLink;
        }

        private void itemsToAdd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsToAdd.SelectedItems?.Count > 0)
            {
                itemProperty.SelectedObject = itemsToAdd.SelectedItems[0].Tag;
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (SelectedItems == null)
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
                    itIds.Remove(tag.Id);
                    SelectedItems = list.ToArray();
                    itemsToAdd.Items.Remove(item);
                }

            }
        }

        private void JsonSelector_FormClosing(object sender, FormClosingEventArgs e)
        {
            textInterface.DataAvailable -= TextInterface_DataAvailable;
            textInterface.End();
        }
    }

    public class JsonItem : Component
    {
        public string Path { get; set; }
        
        public object Value { get; set; }

        public bool Valid { get; set; }

        public JsonItem(string path, object value, bool valid)
        {
            Path = path;
            Value = value;
            Valid = valid;
        }
    }

    public class TextInterfaceEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            IText textInterface = context.Instance as IText;
            textInterface.Begin();

            using (JsonSelector form = new JsonSelector())
            {
                form.TextInterface = textInterface;
                
                if (svc.ShowDialog(form) == DialogResult.OK)
                {
                    value = form.SelectedItems;
                    foreach (var item in form.SelectedItems)
                    {
                        textInterface.Container?.Add(item);
                        textInterface.Container?.Add(item.Tag);
                    }
                }
            }

            textInterface.End();
            return value;
        }

        public static DialogResult ShowEditor(ref IText textInterface)
        {
            var form = new JsonSelector();
            var result = form.ShowDialog();

            return result;
        }
    }
}
