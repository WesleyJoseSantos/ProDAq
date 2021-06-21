using DotNetCom.General.Controls;
using DotNetCom.General.Tags;
using Newtonsoft.Json;
using OPCAutomation;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DotNetCom.OpcDa
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OpcClient : Component, ITagWriterConteiner
    {
        private int updateRate;
        private int itHdl;
        private OPCGroup opcGroup;

        [JsonProperty]
        [Category("OPC")]
        [DisplayName("Server")]
        [Description("OPC Server to be connected with this client.")]
        [Browsable(true)]
        public OpcServerConnection Server
        {
            get => server;
            set
            {
                if (server != null)
                {
                    server.ServerStarted -= Server_ServerStarted;
                    server.RemoveClient(this);
                }
                server = value;
                if (server != null)
                {
                    server.ServerStarted += Server_ServerStarted;
                    server.AddClient(this);
                }
            }
        }

        [JsonProperty]
        [Category("OPC")]
        [DisplayName("Group Name")]
        [Description("Group name used to connecting to the OPC server.")]
        public string GroupName { get; set; }

        [JsonProperty]
        [Category("OPC")]
        [DisplayName("Update Time")]
        [Description("Determine how fast data is retrieved from the OPC server.")]
        public int UpdateTime
        {
            get
            {
                return updateRate;
            }
            set
            {
                updateRate = value;
                if (opcGroup != null)
                {
                    try
                    {
                        opcGroup.UpdateRate = value;
                    }
                    catch (Exception ex)
                    {
                        Console?.Errors?.AppendText(ex.Message);
                    }
                }
            }
        }

        [JsonProperty]
        [Category("OPC")]
        [DisplayName("OPC Items")]
        [Description("OPC Items of this client.")]
        [Editor(typeof(OPCItemCollectionEditor), typeof(UITypeEditor))]
        public TagLink[] TagLinks { get; set; }

        [Browsable(false)]
        public ComponentConsole Console { get; set; }

        public OpcClient()
        {
            InitializeComponent();
            Console = new ComponentConsole();
        }

        public void AddItems()
        {
            if (Server.ServerCurrentState != ServerState.Running)
            {
                Server.Connect();
            }
            var browser = new OpcBrowser();
            browser.OPCBrowser = Server.CreateBrowser();
            if (browser.ShowDialog() == DialogResult.OK)
            {
                TagLinks = browser.SelectedItems;
            }
            UpdateGroup();
        }

        private void InitializeGroup()
        {
            //if (OPCItems != null && opcGroup == null)
            //{
            var groupName = GroupName != "" ? GroupName : Guid.NewGuid().ToString("n").Substring(0, 8);
            opcGroup = Server.OPCGroups.Add(GroupName);
            itHdl = 1;

            opcGroup.DataChange += OpcGroup_DataChange;
            opcGroup.UpdateRate = UpdateTime;
            opcGroup.IsSubscribed = true;
            opcGroup.OPCItems.DefaultIsActive = true;
            //}
        }

        private void UpdateGroup()
        {
            if (TagLinks != null)
            {
                for (int i = itHdl - 1; i < TagLinks.Length; i++)
                {
                    try
                    {
                        opcGroup.OPCItems.AddItem(TagLinks[i].Id, itHdl);
                        TagLinks[i].Status = TagLinkStatus.Good;
                        itHdl++;
                    }
                    catch (Exception ex)
                    {
                        TagLinks[i].Status = TagLinkStatus.Bad;
                        Console?.Errors?.AppendText(TagLinks[i].Id + "is Bad. ");
                        Console?.Errors?.AppendText(ex.Message + '\n');
                    }
                }
            }
        }

        private void OpcGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            for (int i = 1; i <= NumItems; i++)
            {
                var itHdl = Convert.ToInt32(ClientHandles.GetValue(i));
                if (TagLinks != null)
                {
                    var value = ItemValues.GetValue(i);
                    TagLinks[itHdl - 1].Value = value;
                }
            }
        }

        private void Server_ServerStarted(object sender, EventArgs e)
        {
            InitializeGroup();
            UpdateGroup();
            Console?.Log?.AppendText("OPC Server Started!.\n");
        }

        private IContainer components = null;
        private OpcServerConnection server;

        public OpcClient(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
        }
    }
}
