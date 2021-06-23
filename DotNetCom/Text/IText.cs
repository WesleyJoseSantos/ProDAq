using DotNetCom.General.Tags;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        IContainer Container { get; }
        void Begin();
        void End();

        event EventHandler DataAvailable;
    }
}
