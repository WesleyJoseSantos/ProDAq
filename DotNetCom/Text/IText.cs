using DotNetCom.General.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetCom.Text
{
    public interface IText
    {
        SplitMode SplitMode { get; set; }
        string SplitString { get; set; }
        TagLink[] TagLinks { get; set; }
        string ReceivedData { get; set; }
        void Begin();
        void End();
        event EventHandler DataAvailable;
    }
}
