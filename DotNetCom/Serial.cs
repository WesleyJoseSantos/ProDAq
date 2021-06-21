using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace DotNetCom
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Serial : Component
    {
        protected SerialPort serialPort = new SerialPort();

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Port")]
        [Description("Serial port name that should be used.")]
        [TypeConverter(typeof(PortNameTypeConverter))]
        public string Port { get => serialPort.PortName; set => serialPort.PortName = value; }

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Baud rate")]
        [Description("Baud rate of COM Port.")]
        [TypeConverter(typeof(BaudRateTypeConverter))]
        public int BaudRate { get => serialPort.BaudRate; set => serialPort.BaudRate = value; }

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Data bits")]
        [Description("The number of data bits on the serial port.")]
        [TypeConverter(typeof(DataBitsTypeConverter))]
        public int DataBits { get => serialPort.DataBits; set => serialPort.DataBits = value; }

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Stop bits")]
        [Description("The number of stop bits on the serial port.")]
        public StopBits StopBits { get => serialPort.StopBits; set => serialPort.StopBits = value; }

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Parity")]
        [Description("The parity mode on the serial port.")]
        public Parity Parity { get => serialPort.Parity; set => serialPort.Parity = value; }

        [JsonProperty]
        [Category("Serial")]
        [DisplayName("Terminator")]
        [Description("String used to detect end of line.")]
        public string Terminator { get => serialPort.NewLine; set => serialPort.NewLine = value; }

        public Serial()
        {
            InitializeComponent();
        }

        public Serial(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }

    class PortNameTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var list = SerialPort.GetPortNames();
            return new StandardValuesCollection(list);
        }
    }

    class BaudRateTypeConverter : Int32Converter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] list = { 110, 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400, 57600, 74880, 115200, 128000, 256000 };
            return new StandardValuesCollection(list);
        }
    }

    class DataBitsTypeConverter : Int32Converter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            int[] list = { 5, 6, 7, 8 };
            return new StandardValuesCollection(list);
        }
    }
}
