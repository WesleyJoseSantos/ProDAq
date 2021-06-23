using DotNetCom.DataBase;
using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace DotNetCom.General.Tags
{
    public enum TagLinkStatus
    {
        Unknown,
        Good,
        Bad
    }

    [DesignTimeVisible(false)]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class TagLink : Component
    {
        [JsonProperty]
        [Category("General")]
        [DisplayName("Name")]
        [Description("Generic name of this link item. Optional.")]

        public string Name { get; set; }
        [JsonProperty]
        [Category("General")]
        [DisplayName("Tag Name")]
        [Description("Name used to link this item to an existent tag.")]
        [TypeConverter(typeof(TagNameTypeConverter))]
        public string TagName { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Link Id")]
        [Description("Id used to link this item to an external communication variable element.")]
        public string Id { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Status")]
        [Description("Current status of this link.")]
        public TagLinkStatus Status { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Value")]
        [Description("Current linked item Value.")]
        public object Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    ValueChanged?.Invoke(this, null);
                }
            }
        }

        [Browsable(false)]
        [TypeConverter(typeof(TagNameTypeConverter))]
        public Tag Tag { get => Data.TagsDataBase.GetTag(TagName); }

        public event EventHandler ValueChanged;

        private object _value;
        public TagLink()
        {
            InitializeComponent();
            Initialize();
        }

        public TagLink(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ValueChanged += TagLink_ValueChanged;
        }

        private void TagLink_ValueChanged(object sender, EventArgs e)
        {
            if (Tag != null) Tag.Value = Value;
        }
    }
}
