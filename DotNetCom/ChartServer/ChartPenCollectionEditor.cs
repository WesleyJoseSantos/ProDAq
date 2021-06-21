using DotNetCom.General.Tags;
using DotNetCom.OpcDa;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DotNetCom.ChartServer
{
    public partial class ChartPenCollectionEditor : UserControl
    {
        static Random randomGen = new Random();

        public ChartWebServer ChartWebServer { get; set; }

        public event EventHandler PropertyChanged;

        public ChartPenCollectionEditor()
        {
            InitializeComponent();
        }

        public void AddItem(TagLink item)
        {
            if (ChartWebServer.ContainsTag(item.Tag)) return;

            KnownColor[] names = GetKnownColors();
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            Color randomColor = Color.FromKnownColor(randomColorName);
            var list = ChartWebServer.GetDatasets().ToList();
            var pen = new ChartDataset()
            {
                label = item.TagName,
                borderColor = randomColor.Name
            };

            list.Add(pen);
            ChartWebServer.SetDataSets(list.ToArray());
            UpdateList();
        }

        private void listItems_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            propertyGrid1.SelectedObject = e.Item.Tag;
        }

        private void listItems_KeyDown(object sender, KeyEventArgs e)
        {
            var list = ChartWebServer.GetDatasets().ToList();
           if(e.KeyCode == Keys.Delete && listItems.SelectedItems != null)
            {

                foreach (ListViewItem item in listItems.SelectedItems)
                {
                    var pen = item.Tag as ChartDataset;
                    list.Remove(pen);
                }

                ChartWebServer.SetDataSets(list.ToArray());
                UpdateList();
            }
        }

        public void UpdateList()
        {
            listItems.Items.Clear();
            var datasets = ChartWebServer.GetDatasets();

            if (datasets == null) return;
            foreach (var item in datasets)
            {
                var it = listItems.Items.Add(item.label);
                it.Tag = item;
                it.SubItems.Add(item.borderColor);
            }
            ChartWebServer.SaveChartSettings();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            UpdateList();
            PropertyChanged?.Invoke(propertyGrid1.SelectedObject, null);
        }

        private KnownColor[] GetKnownColors()
        {
            KnownColor[] names = {
                KnownColor.AliceBlue, 
                KnownColor.Aqua, 
                KnownColor.Aquamarine, 
                KnownColor.Azure, 
                KnownColor.Beige, 
                KnownColor.Black, 
                KnownColor.BlanchedAlmond,
                KnownColor.Blue,
                KnownColor.BlueViolet,
                KnownColor.Brown,
                KnownColor.BurlyWood,
                KnownColor.CadetBlue,
                KnownColor.Chartreuse,
                KnownColor.Chocolate,
                KnownColor.Coral,
                KnownColor.CornflowerBlue,
                KnownColor.Crimson,
                KnownColor.Cyan,
                KnownColor.DarkBlue,
                KnownColor.DarkCyan,
                KnownColor.DarkGoldenrod,
                KnownColor.DarkGray,
                KnownColor.DarkGreen,
                KnownColor.DarkMagenta,
                KnownColor.DarkOliveGreen,
                KnownColor.DarkOrange,
                KnownColor.DarkOrchid,
                KnownColor.DarkRed,
                KnownColor.DarkSalmon,
                KnownColor.DarkSeaGreen,
                KnownColor.DarkSlateBlue,
                KnownColor.DarkSlateGray,
                KnownColor.DarkTurquoise,
                KnownColor.DarkViolet,
                KnownColor.DeepPink
            };
            return names;
        }
    }
}
