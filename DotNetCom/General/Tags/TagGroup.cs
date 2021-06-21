using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Design;
using DotNetCom.General.Tags;
using DotNetCom.General.NamedObject;
using DotNetCom.DataBase;

namespace DotNetCom.General.Tags
{
    [JsonObject(MemberSerialization.OptIn)]
    public partial class TagGroup : Component, INamedObject, ITagReaderContainer
    {
        private string name;
        private bool added = false;

        [JsonProperty]
        [Category("General")]
        [DisplayName("Name")]
        [Description("Tags Group Name.")]
        public string Name
        {
            get => name;
            set
            {
                if (name == null && value != null && value != "" && !added)
                {
                    name = value;
                    Data.TagsDataBase.Add(this);
                    added = true;
                    return;
                }
                name = value;
            }
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Tags Collection")]
        [Description("Collection of tags linked to this control.")]
        public TagCollection TagsCollection { get; set; }

        public TagGroup()
        {
            InitializeComponent();
        }

        public TagGroup(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        ~TagGroup()
        {
            Data.TagsDataBase.Remove(this);
        }

    }
}
