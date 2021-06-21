using DotNetCom.General.NamedObject;
using System.ComponentModel;
using System.Drawing.Design;

namespace DotNetCom.General.Tags
{
    public interface ITagReaderContainer
    {
        TagCollection TagsCollection { get; set; }
    }
}
