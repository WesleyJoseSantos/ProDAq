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
                    var selected = NodeToJsonItem(e.Node);
                    itemProperty.SelectedObject = selected;
                }
                catch (Exception ex)
                {
                    rtbReceivedData.AppendText(ex.Message + '\n');
                    rtbReceivedData.AppendText(ex.StackTrace + '\n');
                }
            }
        }

        private void browserList_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count != 0 && e.Node.Checked == true)
            {
                e.Node.Checked = false;
                foreach (TreeNode item in e.Node.Nodes)
                {
                    item.Checked = !item.Checked;
                }
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in browserList.Nodes)
            {
                item.Checked = true;
            }
        }

        private void deselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in browserList.Nodes)
            {
                item.Checked = false;
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            foreach (TreeNode item in browserList.Nodes)
            {
                var jsIt = NodeToJsonItem(item);
                if (itIds.Contains(jsIt.Path)) continue;
                itIds.Add(jsIt.Path);

                var tagLink = JsonItemToTagLink(jsIt);
                var list = SelectedItems?.ToList() ?? new List<TagLink>();
                var itNode = itemsToAdd.Items.Add(tagLink.Name);

                list.Add(tagLink);
                itNode.Tag = tagLink;
                SelectedItems = list.ToArray();
            }
        }

        private void TextInterface_DataAvailable(object sender, EventArgs e)
        {
            rtbReceivedData.AppendText(textInterface.ReceivedData + '\n');
            try
            {
                if (browserList.Nodes.Count == 0)
                {
                    JObject json = JObject.Parse(textInterface.ReceivedData);
                    browserList.Invoke((MethodInvoker)delegate {
                        JsonTreeViewLoader.AddObjectNodes(json, "Received Data", browserList.Nodes);
                    });
                }
            }
            catch (Exception ex)
            {
                rtbReceivedData.AppendText(ex.Message + '\n');
                rtbReceivedData.AppendText(ex.StackTrace + '\n');
            }
        }

        private JsonItem NodeToJsonItem(TreeNode node)
        {
            JObject json = JObject.Parse(textInterface.ReceivedData);
            var path = node.FullPath.Replace('\\', '.').Split(':')[0];
            path = path.Replace("Received Data.", "");
            node.Tag = path;
            var value = json.SelectToken(path);
            var selected = new JsonItem(path, value);
            return selected;
        }

        private TagLink JsonItemToTagLink(JsonItem jsonItem)
        {
            var tagLink = new TagLink()
            {
                Id = jsonItem.Path,
                Name = jsonItem.Path.Split('.').ToList().Last()
            };
            return tagLink;
        }
    }

    public class JsonItem : Component
    {
        public string Path { get; set; }
        
        public object Value { get; set; }

        public JsonItem(string path, object value)
        {
            Path = path;
            Value = value;
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
