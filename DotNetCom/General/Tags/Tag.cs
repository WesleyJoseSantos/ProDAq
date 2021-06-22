using Newtonsoft.Json;
using System;
using System.ComponentModel;
using DotNetCom.General.NamedObject;
using DotNetCom.DataBase;
using System.Text.RegularExpressions;

namespace DotNetCom.General.Tags
{
    [DesignTimeVisible(false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class Tag : Component, INamedObject
    {
        private object _value;
        private string name;

        [JsonProperty]
        [Category("General")]
        [DisplayName("Description")]
        [Description("Description of this item. Optional.")]
        public string Description { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Name")]
        [Description("Tag Name.")]
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Data.TagsDataBase.Add(this);
            }
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Value")]
        [Description("Tag Value.")]
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

        public event EventHandler ValueChanged;

        public Tag()
        {

        }

        public Tag(string name)
        {
            Name = name;
        }

        ~Tag()
        {
            if (Name != null && Name != "") Data.TagsDataBase.Remove(this);
        }

        public static string GetTagName(string itName, bool toUpperCase)
        {
            string tagName = "";
            tagName = itName.Replace(" ", "_");
            tagName = tagName.Replace(".", "_").Trim();
            tagName = tagName.Replace("\n", "_").Trim();
            tagName = Regex.Replace(tagName, @"[^0-9a-zA-Zz._]", string.Empty);
            if (toUpperCase)
            {
                tagName = tagName.ToUpper();
            }
            return tagName;
        }
    }

    public class TagNameTypeConverter : StringConverter
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
            var list = Data.TagsDataBase.Tags.Keys;
            return new StandardValuesCollection(list);
        }
    }
}
