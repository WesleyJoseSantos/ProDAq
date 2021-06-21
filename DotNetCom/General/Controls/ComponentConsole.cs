using System.Windows.Forms;

namespace DotNetCom.General.Controls
{
    public class ComponentConsole
    {
        public TextBoxBase Log { get; set; }
        public TextBoxBase Errors { get; set; }

        public ComponentConsole() { }

        public ComponentConsole(TextBoxBase log, TextBoxBase errors)
        {
            Log = log;
            Errors = errors;
        }

    }
}
