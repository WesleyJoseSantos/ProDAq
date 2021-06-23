using DotNetCom.General.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DotNetScadaComponents.Trend
{
    class TrendChart : Chart
    {
        public TrendChartSettings Settings { get; set; } = new TrendChartSettings();

        private Dictionary<string, Series> digSeries = new Dictionary<string, Series>();
        private Dictionary<string, Series> analogSeries = new Dictionary<string, Series>();
        private List<Series> digSeriesList = new List<Series>();
        private int cont = 0;

        public TrendChart()
        {
            
        }

        public void AddData(string name, object value, string time)
        {
            var trendIdx = 0;
            var series = GetSeries(name, value);
            if (value == null) return;
            if(value is bool)
            {
                SafeAction(delegate
                {
                    trendIdx = AddBooleanData(name, (bool)value, time);
                });
            }
            else
            {
                SafeAction(delegate
                {
                    trendIdx = series.Points.AddXY(time, value);
                });
            }
            if(trendIdx > Settings.Axis.X.Size)
            {
                SafeAction(delegate
                {
                    series.Points.RemoveAt(0);
                });
            }
        }

        public int AddBooleanData(string name, bool value, string time)
        {
            var series = GetSeries(name, value);
            var idx = digSeriesList.IndexOf(series) + 1;
            var offSet = Settings.Axis.Y.DigitalSignals.SizeOffSet;
            Console.WriteLine(value);
            cont = series.Points.AddXY(time, idx - 1, idx - offSet);
            series.Points[cont].Color = series.Color;
            series.Points[cont].Color = value ? series.Color : Color.Transparent;
            return cont;
        }

        public Series GetSeries(string name, object value)
        {
            if (digSeries.ContainsKey(name))
            {
                return digSeries[name];
            }
            if (analogSeries.ContainsKey(name))
            {
                return analogSeries[name];
            }
            Series newSeries = null;
            SafeAction(delegate
            {
                newSeries = Series.Add(name);
                ConfigureSeries(ref newSeries, value);
            });
            return newSeries;
        }

        public ChartArea GetDigitalChartArea()
        {
            var chartArea = ChartAreas.FindByName("DigitalChartArea");
            if(chartArea == null)
            {
                var newChartArea = new ChartArea("DigitalChartArea");
                newChartArea.Name = "DigitalChartArea";
                newChartArea.Position = new ElementPosition(0, 0, 100, 100);
                newChartArea.AxisY.Enabled = AxisEnabled.False;
                newChartArea.AxisX.Maximum = Settings.Axis.X.Size;
                newChartArea.AxisY.Maximum = Settings.Axis.Y.DigitalSignals.Scale;
                ChartAreas.Add(newChartArea);
                return newChartArea;
            }
            return chartArea;
        }

        public void ConfigureSeries(ref Series series, object value)
        {
            if(value is bool)
            {
                digSeries.Add(series.Name, series);
                digSeriesList.Add(series);
                series.ChartType = SeriesChartType.Range;
                series.ChartArea = GetDigitalChartArea().Name;
            }
            else
            {
                var newChart = new ChartArea(series.Name);
                ConfigureNewChart(ref newChart);

                analogSeries.Add(series.Name, series);
                series.ChartType = SeriesChartType.Line;
                series.ChartArea = newChart.Name;
            }
        }

        public void ConfigureNewChart(ref ChartArea chart)
        {
            chart.AlignmentOrientation = AreaAlignmentOrientations.All;
            chart.AlignWithChartArea = GetDigitalChartArea().Name;
            chart.AxisX.IsMarginVisible = false;
            chart.AxisY.Enabled = AxisEnabled.False;
            chart.AxisX.Enabled = AxisEnabled.False;
            chart.AxisX.Maximum = Settings.Axis.X.Size;

            chart.BackColor = Color.Transparent;
            ChartAreas.Add(chart);
        }

        public void Remove(Tag[] tags) 
        {
            foreach (var tag in tags)
            {
                if (digSeries.ContainsKey(tag.Name))
                {
                    Series.Remove(digSeries[tag.Name]);
                    digSeries.Remove(tag.Name);
                }
                if (analogSeries.ContainsKey(tag.Name))
                {
                    Series.Remove(analogSeries[tag.Name]);
                    analogSeries.Remove(tag.Name);
                }
                var chartArea = ChartAreas.FindByName(tag.Name);
                if (chartArea != null) ChartAreas.Remove(chartArea);
            }
        }

        private void SafeAction(Action action)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate
                {
                    action();
                });
            }
            else
            {
                action();
            }
        }
    }

    [DesignTimeVisible(false)]
    public class TrendChartSettings : Component
    {
        public TrendChartAxisSettings Axis { get; set; } = new TrendChartAxisSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisSettings : Component
    {
        public TrendChartAxisXSettings X { get; set; } = new TrendChartAxisXSettings();
        public TrendChartAxisYSettings Y { get; set; } = new TrendChartAxisYSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisXSettings : Component
    {
        public int Size { get; set; } = 1000;
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisYSettings : Component
    {
        public TrendChartAxisYDigitalSettings DigitalSignals { get; set; } = new TrendChartAxisYDigitalSettings();
    }

    [DesignTimeVisible(false)]
    public class TrendChartAxisYDigitalSettings : Component
    {
        public int Scale { get; set; } = 25;
        public float SizeOffSet { get; set; } = 0.3F;
    }
}
