using DotNetCom.General.Tags;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DotNetScadaComponents.Trend
{
    public partial class Trend : UserControl, ITagReaderContainer
    {
        public TrendChartSettings Settings
        {
            get => trendChart.Settings;
            set => trendChart.Settings = value;
        }

        public int TimeBase
        {
            get => timer.Interval;
            set => timer.Interval = value;
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Tags Collection")]
        [Description("Collection of tags linked to this control.")]
        public TagCollection TagsCollection { get; set; }

        private Timer timer;

        public Trend()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TagsCollection?.Tags != null)
            {
                foreach (var tag in TagsCollection.Tags)
                {
                    var time = DateTime.Now.ToString("HH:mm:ss.fff");
                    if(tag.Value != null)
                    {
                        trendChart.AddData(tag.Name, tag.Value, time);
                    }
                }
            }
        }

        public void Start()
        {
            timer.Start();
        }

        private void UpdateSeries()
        {
            //if (tags != null)
            //{
            //    var list = tags.ToList();
            //    foreach (var item in value)
            //    {
            //        list.Remove(item);
            //    }
            //    trendChart.Remove(list.ToArray());
            //}
        }
    }
}
