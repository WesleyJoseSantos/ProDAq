using DotNetCom.DataBase;
using DotNetCom.General.NamedObject;
using Newtonsoft.Json;
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

    }
}
