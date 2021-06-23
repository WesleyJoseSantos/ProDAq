using DotNetCom.General.Tags;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
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

        public bool UpdateOnDataChanged
        {
            get => updateOnDataChanged;
            set
            {
                updateOnDataChanged = value;
                timer.Enabled = !updateOnDataChanged;
                if (updateOnDataChanged)
                {
                    TagsCollection.EnableEvents();
                    if (TagsCollection != null) TagsCollection.TagChanged -= TagsCollection_TagChanged;
                    if (TagsCollection != null) TagsCollection.TagChanged += TagsCollection_TagChanged;
                }
            }
        }

        public int TimeBase
        {
            get
            {
                if(timer != null)
                {
                    return timer.Interval;
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if(timer != null)
                {
                    timer.Interval = value;
                }

            }
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Tags Collection")]
        [Description("Collection of tags linked to this control.")]
        public TagCollection TagsCollection { get; set; }

        public new event MouseEventHandler MouseClick
        {
            add
            {
                base.MouseClick += value;
                foreach (Control control in Controls)
                {
                    control.MouseClick += value;
                }
            }
            remove
            {
                base.MouseClick -= value;
                foreach (Control control in Controls)
                {
                    control.MouseClick -= value;
                }
            }
        }

        private ThreadingTimer timer;
        private bool updateOnDataChanged;

        public Trend()
        {
            InitializeComponent();
            timer = new ThreadingTimer(UpdateTrend);
        }

        void UpdateTrend()
        {
            if (TagsCollection?.Tags != null)
            {
                foreach (var tag in TagsCollection.Tags)
                {
                    var time = DateTime.Now.ToString("HH:mm:ss.fff");
                    if (tag.Value != null)
                    {
                        trendChart.AddData(tag.Name, tag.Value, time);
                    }
                }
            }
        }

        private void TagsCollection_TagChanged(object sender, EventArgs e)
        {
            if (!updateOnDataChanged) if (TagsCollection != null)
            {
                TagsCollection.TagChanged -= TagsCollection_TagChanged;
                TagsCollection.DisableEvents();
            }
            var tag = sender as Tag;
            var time = DateTime.Now.ToString("HH:mm:ss.fff");
            if (tag.Value != null)
            {
                trendChart.AddData(tag.Name, tag.Value, time);
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
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

    public class ThreadingTimer
    {
        public bool Enabled 
        { 
            get => watch.IsRunning; 
            set
            {
                if (value)
                {
                    watch.Start();
                }
                else
                {
                    watch.Stop();
                }
            }
        }

        public int Interval { get; set; }

        private Stopwatch watch = new Stopwatch();
        public ThreadingTimer(Action action)
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                while (true)
                {
                    if (Enabled)
                    {
                        if(watch.ElapsedMilliseconds >= Interval)
                        {
                            watch.Restart();
                            action();
                        }
                    }
                }
            });
        }

        public void Start()
        {
            watch.Start();
        }

        public void Stop()
        {
            watch.Stop();
        }
    }
}
