
namespace DotNetScadaComponents.Trend
{
    partial class Trend
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.trendChartSettings1 = new DotNetScadaComponents.Trend.TrendChartSettings();
            this.trendChartAxisSettings1 = new DotNetScadaComponents.Trend.TrendChartAxisSettings();
            this.trendChartAxisXSettings1 = new DotNetScadaComponents.Trend.TrendChartAxisXSettings();
            this.trendChartAxisYSettings1 = new DotNetScadaComponents.Trend.TrendChartAxisYSettings();
            this.trendChartAxisYDigitalSettings1 = new DotNetScadaComponents.Trend.TrendChartAxisYDigitalSettings();
            this.trendChart = new DotNetScadaComponents.Trend.TrendChart();
            ((System.ComponentModel.ISupportInitialize)(this.trendChart)).BeginInit();
            this.SuspendLayout();
            // 
            // trendChartSettings1
            // 
            this.trendChartSettings1.Axis = this.trendChartAxisSettings1;
            // 
            // trendChartAxisSettings1
            // 
            this.trendChartAxisSettings1.X = this.trendChartAxisXSettings1;
            this.trendChartAxisSettings1.Y = this.trendChartAxisYSettings1;
            // 
            // trendChartAxisXSettings1
            // 
            this.trendChartAxisXSettings1.Size = 100;
            // 
            // trendChartAxisYSettings1
            // 
            this.trendChartAxisYSettings1.DigitalSignals = this.trendChartAxisYDigitalSettings1;
            // 
            // trendChartAxisYDigitalSettings1
            // 
            this.trendChartAxisYDigitalSettings1.Scale = 25;
            this.trendChartAxisYDigitalSettings1.SizeOffSet = 0.3F;
            // 
            // trendChart
            // 
            this.trendChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.trendChart.Legends.Add(legend1);
            this.trendChart.Location = new System.Drawing.Point(0, 0);
            this.trendChart.Name = "trendChart";
            this.trendChart.Settings = this.trendChartSettings1;
            this.trendChart.Size = new System.Drawing.Size(441, 418);
            this.trendChart.TabIndex = 0;
            this.trendChart.Text = "trendChart1";
            // 
            // Trend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trendChart);
            this.Name = "Trend";
            this.Size = new System.Drawing.Size(441, 418);
            ((System.ComponentModel.ISupportInitialize)(this.trendChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TrendChart trendChart;
        private TrendChartSettings trendChartSettings1;
        private TrendChartAxisSettings trendChartAxisSettings1;
        private TrendChartAxisXSettings trendChartAxisXSettings1;
        private TrendChartAxisYSettings trendChartAxisYSettings1;
        private TrendChartAxisYDigitalSettings trendChartAxisYDigitalSettings1;
    }
}
