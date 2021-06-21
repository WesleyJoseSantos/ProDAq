using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DotNetCom.General.Tags
{
    public partial class TagExplorer : Component
    {
        public Dictionary<string, Tag> AvailableTags { get => DataBase.Data.TagsDataBase.Tags; }

        public TagExplorer()
        {
            InitializeComponent();
        }

        public TagExplorer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
