using DotNetCom.General.Tags;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNetCom.Text
{
    public enum SplitMode
    {
        String,
        Json
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class SerialText : Serial, IText
    {

        [JsonProperty]
        [Category("Split Options")]
        [DisplayName("Split mode")]
        [Description("Serial port name that should be used.")]
        public SplitMode SplitMode { get; set; }

        [JsonProperty]
        [Category("Split Options")]
        [DisplayName("Split string")]
        [Description("String used to split non-json received content.")]
        public string SplitString { get; set; }

        [JsonProperty]
        [Category("Data")]
        [DisplayName("Items to Receive")]
        [Description("Items to receive on serial port. " +
            "In Json split mode, Id will be the json path. " +
            "In Strin split mode, Id is not necessary.")]
        [Editor(typeof(TextInterfaceEditor), typeof(UITypeEditor))]
        public TagLink[] TagLinks { get; set; }

        [Browsable(false)]
        public string ReceivedData { get; set; }

        public event EventHandler DataAvailable;

        public SerialText()
        {
            InitializeComponent();
            Init();
        }

        public SerialText(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        public void Begin()
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void End()
        {
            try
            {
                serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Init()
        {
            serialPort.DataReceived += SerialPort_DataReceived;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceivedData = serialPort.ReadLine();
            switch (SplitMode)
            {
                case SplitMode.String:

                    break;
                case SplitMode.Json:
                    JsonParseStringToTags(ReceivedData);
                    break;
                default:
                    break;
            }
            DataAvailable?.Invoke(this, null);
        }

        private void JsonParseStringToTags(string jsonStr)
        {
            if (TagLinks == null) return;
            JObject json;
            try
            {
                json = JObject.Parse(jsonStr);
            }
            catch (Exception)
            {
                return;
            }
            foreach (var tag in TagLinks)
            {
                try
                {
                    var value = (JValue)json.SelectToken(tag.Id);
                    if (value != null)
                    {
                        tag.Value = value.Value;
                    }
                    else
                    {
                        tag.Status = TagLinkStatus.Bad;
                    }
                }
                catch (Exception)
                {
                    tag.Status = TagLinkStatus.Bad;
                }
            }
        }
    }
}
