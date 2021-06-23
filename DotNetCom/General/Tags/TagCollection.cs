using DotNetCom.DataBase;
using DotNetCom.General.NamedObject;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace DotNetCom.General.Tags
{
    [JsonObject(MemberSerialization.OptIn)]
    [Editor(typeof(TagsSelectorEditor), typeof(UITypeEditor))]
    public class TagCollection
    {
        [JsonProperty]
        [Category("Database")]
        [DisplayName("Tags")]
        [Description("Tags collection.")]
        public string[] Names { get; set; }

        [Browsable(false)]
        public Tag[] Tags { get => Data.TagsDataBase.GetTags(Names); }

        public event EventHandler TagChanged;

        private void Item_ValueChanged(object sender, EventArgs e)
        {
            TagChanged?.Invoke(sender, e);
        }

        public void EnableEvents()
        {
            var tags = Tags;
            DisableEvents();
            foreach (var item in tags)
            {
                item.ValueChanged += Item_ValueChanged;
            }
        }

        public void DisableEvents()
        {
            var tags = Tags;
            foreach (var item in tags)
            {
                item.ValueChanged -= Item_ValueChanged;
            }
        }
    }
}
