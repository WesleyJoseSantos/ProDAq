using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DotNetCom.DataBase;
using DotNetCom.General.Tags;

namespace DotNetCom.General.NamedObject
{
    public partial class ObjectSelector : Form
    {
        public INamedObject[] SelectedObjects { get; set; }

        public INamedObject[] AvailableObjects { get; set; }
        
        public ObjectSelector()
        {
            InitializeComponent();
        }

        private void ObjectSelector_Load(object sender, EventArgs e)
        {
            if(AvailableObjects != null)
            {
                foreach (var item in AvailableObjects)
                {
                    if (SelectedObjects.Contains(item)) continue;
                    var it = listAvailableObjects.Items.Add(item.Name);
                    it.Tag = item;
                }
            }

            if(SelectedObjects != null)
            {
                foreach (var item in SelectedObjects)
                {
                    var it = listSelectedObjects.Items.Add(item.Name);
                    it.Tag = item;
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            if (listSelectedObjects.SelectedIndices == null) return;
            foreach (ListViewItem item in listAvailableObjects.SelectedItems)
            {
                var it = (Tag)item.Tag;
                var list = SelectedObjects?.ToList() ?? new List<INamedObject>();
                list.Add(it);

                var itNode = listSelectedObjects.Items.Add(it.Name);
                itNode.Tag = it;
                SelectedObjects = list.ToArray();

                listAvailableObjects.Items.Remove(item);
            }
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listSelectedObjects_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (listSelectedObjects.SelectedIndices == null) return;
                foreach (ListViewItem item in listSelectedObjects.SelectedItems)
                {
                    var tag = item.Tag as Tag;
                    var list = SelectedObjects.ToList();
                    list.Remove(tag);
                    SelectedObjects = list.ToArray();
                    listSelectedObjects.Items.Remove(item);
                }
                
            }
        }

        private void listAvailableObjects_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (listAvailableObjects.SelectedIndices == null) return;
                if (MessageBox.Show("Delete object(s) from application database?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in listAvailableObjects.SelectedItems)
                    {
                        var tag = item.Tag as Tag;
                        var list = AvailableObjects.ToList();
                        list.Remove(tag);
                        AvailableObjects = list.ToArray();
                        listAvailableObjects.Items.Remove(item);
                        Data.TagsDataBase.Remove(tag);
                        tag.Dispose();
                    }

                }

            }
        }
    }

    public class TagsSelectorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            try
            {
                using (ObjectSelector form = new ObjectSelector())
                {
                    var obj = context.Instance;
                    form.AvailableObjects = Data.TagsDataBase.Tags.Values.ToArray();
                    form.SelectedObjects = ((ITagReaderContainer)obj).TagsCollection?.Tags ?? new Tag[0];
                    form.Text = "Tags Selector";

                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        var tags = new List<string>();
                        foreach (var item in form.SelectedObjects)
                        {
                            tags.Add(((Tag)item).Name);
                        }
                        var tagsCollection = new TagCollection();
                        tagsCollection.Names = tags.ToArray();
                        value = tagsCollection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + ex.StackTrace); ;
                throw;
            }
            
            return value;
        }
    }

    class TagsGroupsSelectorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            using (ObjectSelector form = new ObjectSelector())
            {
                var obj = context.Instance as ITagGroupsConteiner;
                form.AvailableObjects = Data.TagsDataBase.TagsGroups.Values.ToArray();
                form.SelectedObjects = obj.TagsGroups ?? new TagGroup[0];
                form.Text = "Tags Groups Selector";

                if (svc.ShowDialog(form) == DialogResult.OK)
                {
                    var tags = new List<TagGroup>();
                    foreach (var item in form.SelectedObjects)
                    {
                        tags.Add(item as TagGroup);
                    }
                    value = tags.ToArray();
                }
            }
            return value;
        }
    }
}
