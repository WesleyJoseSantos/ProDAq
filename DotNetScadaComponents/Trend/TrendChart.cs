using DotNetCom.General.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        public void Init()
        {
            //Series.Clear();
            //ChartAreas.Clear();
            //ChartArea chartArea1 = new ChartArea();
            //CustomLabel customLabelX = new CustomLabel();
            //CustomLabel customLabelY = new CustomLabel();
            //Legend legend1 = new Legend();
            //AllowDrop = true;

            //chartArea1.AxisX.CustomLabels.Add(customLabelX);
            //chartArea1.AxisX.MajorGrid.LineColor = Color.LightGray;
            //chartArea1.AxisY.CustomLabels.Add(customLabelY);
            //chartArea1.AxisY.Enabled = AxisEnabled.False;
            //chartArea1.Name = "ChartArea1";
            //ChartAreas.Add(chartArea1);

            //legend1.BackColor = Color.Transparent;
            //legend1.DockedToChartArea = "ChartArea1";
            //legend1.Docking = Docking.Left;
            //legend1.Name = "Legend1";
            //Legends.Add(legend1);


        }

        public void AddData(string name, object value, string time)
        {
            var trendIdx = 0;
            var series = GetSeries(name, value);
            if (value == null) return;
            if(value is bool)
            {
                trendIdx = AddBooleanData(name, (bool)value, time);
            }
            else
            {
                trendIdx = series.Points.AddXY(time, value);
                //var chartArea = ChartAreas.FindByName(series.ChartArea);
                //var digArea = GetDigitalChartArea();
                //chartArea.RecalculateAxesScale();
                //digArea.AxisX.Minimum = chartArea.AxisX.Minimum;
                //digArea.AxisX.Maximum = chartArea.AxisX.Maximum;
                //digArea.AxisY.Minimum = chartArea.AxisY.Minimum;
                //digArea.AxisY.Maximum = chartArea.AxisY.Maximum;
            }
            if(trendIdx > Settings.Axis.X.Size)
            {
                series.Points.RemoveAt(0);
                //var chartArea = ChartAreas.FindByName(series.ChartArea);
                //chartArea.RecalculateAxesScale();
                //foreach (var chart in ChartAreas)
                //{
                //    chart.AxisX.Minimum = trendIdx - Settings.Axis.X.Size;
                //}
                //foreach (var series in Series)
                //{
                //    var chartArea = ChartAreas.FindByName(series.ChartArea);
                //    chartArea.RecalculateAxesScale();
                //}
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
            var newSeries = Series.Add(name);
            ConfigureSeries(ref newSeries, value);
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
                //series.CustomProperties = "PointWidth=" + 2;
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

            //foreach (var axis in chart.Axes)
            //{
            //    axis.MajorTickMark.Enabled = false;
            //    axis.MajorGrid.Enabled = false;
            //    axis.LabelStyle.Enabled = false;
            //}

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

        private void InitializeComponent()
        {

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
