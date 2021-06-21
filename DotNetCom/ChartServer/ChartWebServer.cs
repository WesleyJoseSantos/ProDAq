using DotNetCom.DataBase;
using DotNetCom.General.Tags;
using DotNetCom.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DotNetCom.ChartServer
{
    public class ChartWebServer : WebServer
    {
        private ChartSettings chartSettings;

        [JsonProperty]
        [Category("Chart")]
        [DisplayName("Chart Settings")]
        [Description("Chart configuration.")]
        public ChartSettings ChartSettings
        {
            get
            {
                return chartSettings;
            }

            set
            {
                chartSettings = value;
            }
        }

        public ChartWebServer()
        {
            ChartSettings = new ChartSettings();
        }

        public void GenerateChartSettings()
        {
            var datasets = chartSettings?.data?.datasets?.ToList() ?? new List<ChartDataset>();
            if (TagsCollection.Tags == null) return;
            for (int i = 0; i < TagsCollection.Tags.Length; i++)
            {
                Tag tag = TagsCollection.Tags[i];
                if (datasets.Count > i)
                {
                    datasets[i].label = tag.Name;
                }
                else
                {
                    datasets.Add(new ChartDataset()
                    {
                        label = tag.Name,
                    });
                }
            }
            chartSettings.data.datasets = datasets.ToArray();
        }

        public void SaveChartSettings()
        {
            if(ServerRoot != null)
            {
                if (Directory.Exists(ServerRoot))
                {
                    string fileName = ServerRoot + "\\chart.json";
                    File.WriteAllText(fileName, chartSettings.ToJson());
                }
            }
        }

        public ChartDataset[] GetDatasets()
        {
            if(ChartSettings.data.datasets == null)
            {
                ChartSettings.data.datasets = new ChartDataset[0];
            }
            return ChartSettings.data.datasets;
        }

        public void SetDataSets(ChartDataset[] datasets)
        {
            var tags = new List<string>();
            ChartSettings.data.datasets = datasets;
            foreach (var item in datasets)
            {
                tags.Add(item.label);
            }
            TagsCollection.Names = tags.ToArray();
        }

        public bool ContainsTag(Tag tag)
        {
            foreach (var item in ChartSettings.data.datasets)
            {
                if(item.label == tag.Name)
                {
                    return true;
                }
            }
                return false;
        }
    }
}
