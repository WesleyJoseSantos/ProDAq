using DotNetCom.General.Controls;
using Newtonsoft.Json;
using OPCAutomation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace DotNetCom.OpcDa
{
    public enum ServerState
    {
        Running = 1,
        Failed,
        Noconfig,
        Suspended,
        Test,
        Disconnected
    }

    public enum OPCServerConnectionAction
    {
        None,
        Connect,
        Disconnect
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class OpcServerConnection : Component, OPCServer
    {
        private OPCServer server;

        private OPCServer GetServer
        {
            get
            {
                if(server == null)
                {
                    try
                    {
                        server = new OPCServer();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                }
                return server;
            }
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Server Name")]
        [Description("OPC server name.")]
        [TypeConverter(typeof(ServerNameTypeConverter))]
        public string ServerName { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Computer Name")]
        [Description("Computer Name where opc server is running.")]
        public string ServerNode { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Server State")]
        [Description("Current State of OPC server.")]
        public ServerState ServerCurrentState => (ServerState)GetServer.ServerState;

        [JsonProperty]
        [Category("General")]
        [DisplayName("Action")]
        [Description("Action to Execute.")]
        public OPCServerConnectionAction Action
        {
            get => OPCServerConnectionAction.None;
            set
            {
                switch (value)
                {
                    case OPCServerConnectionAction.None:

                        break;
                    case OPCServerConnectionAction.Connect:
                        Connect();
                        break;
                    case OPCServerConnectionAction.Disconnect:
                        Disconnect();
                        break;
                    default:
                        break;
                }
            }
        }

        [Category("General")]
        [DisplayName("Connected Clients")]
        [Description("Local instances of opc clients connected to this opc server connection instance.")]
        public OpcClient[] ConnectedClients => connectedClients;

        [Browsable(false)]
        public ComponentConsole Console { get; set; }

        [Browsable(false)]
        public string ClientName { get => GetServer.ClientName; set => GetServer.ClientName = value; }

        [Browsable(false)]
        public DateTime StartTime => GetServer.StartTime;

        [Browsable(false)]
        public DateTime CurrentTime => GetServer.CurrentTime;

        [Browsable(false)]
        public DateTime LastUpdateTime => GetServer.LastUpdateTime;

        [Browsable(false)]
        public short MajorVersion => GetServer.MajorVersion;

        [Browsable(false)]
        public short MinorVersion => GetServer.MinorVersion;

        [Browsable(false)]
        public short BuildNumber => GetServer.BuildNumber;

        [Browsable(false)]
        public string VendorInfo => GetServer.VendorInfo;

        [Browsable(false)]
        public int ServerState => GetServer.ServerState;

        [Browsable(false)]
        public int LocaleID { get => GetServer.LocaleID; set => GetServer.LocaleID = value; }

        [Browsable(false)]
        public int Bandwidth => GetServer.Bandwidth;

        [Browsable(false)]
        public OPCGroups OPCGroups => GetServer.OPCGroups;

        [Browsable(false)]
        public dynamic PublicGroupNames => GetServer.PublicGroupNames;

        public event DIOPCServerEvent_ServerShutDownEventHandler ServerShutDown;

        public event EventHandler ServerStarted;

        public OpcServerConnection()
        {
            GetServer.ServerShutDown += ThisServer_ServerShutDown;
            InitializeComponent();
            Console = new ComponentConsole();
        }

        public dynamic GetOPCServers(object Node)
        {
            var anServer = new OPCServer();
            object servers = anServer.GetOPCServers();
            var arrServers = (Array)servers;
            var serverList = new List<string>();

            for (int i = 1; i <= arrServers.Length; i++)
            {
                object server = arrServers.GetValue(i);
                var newServer = new OpcServerConnection();
                serverList.Add(server.ToString());
            }

            return serverList;
        }

        public void Connect()
        {
            try
            {
                GetServer.Connect(ServerName, ServerNode);
                if (ServerCurrentState == OpcDa.ServerState.Running)
                {
                    ServerStarted?.Invoke(this, null);
                }
                Console?.Log?.AppendText("Server status: " + ServerCurrentState + '\n');
            }
            catch (Exception ex)
            {
                Console?.Errors?.AppendText(ex.Message + '\n');
            }
        }

        public void Connect(string ProgID, object Node)
        {
            ServerName = ProgID;
            ServerNode = Node.ToString();
            GetServer.Connect(ProgID, Node);
            Console?.Log?.AppendText("Server status: " + ServerCurrentState + '\n');
        }

        public void Disconnect()
        {
            GetServer.Disconnect();
            Console?.Log?.AppendText("Server status: " + ServerCurrentState + '\n');
            ServerShutDown?.Invoke("Disconnect order");
        }

        public void AddClient(OpcClient client)
        {
            var list = connectedClients?.ToList() ?? new List<OpcClient>();
            list.Add(client);
            connectedClients = list.ToArray();
            Console?.Log?.AppendText("Server client added: " + client.GroupName + '\n');
        }

        public void RemoveClient(OpcClient client)
        {
            var list = connectedClients?.ToList() ?? new List<OpcClient>();
            list.Remove(client);
            connectedClients = list.ToArray();
            Console?.Log?.AppendText("Server client removed: " + client.GroupName + '\n');
        }

        public OPCBrowser CreateBrowser()
        {
            return GetServer.CreateBrowser();
        }

        public string GetErrorString(int ErrorCode)
        {
            return GetServer.GetErrorString(ErrorCode);
        }

        public dynamic QueryAvailableLocaleIDs()
        {
            return GetServer.QueryAvailableLocaleIDs();
        }

        public void QueryAvailableProperties(string ItemID, out int Count, out Array PropertyIDs, out Array Descriptions, out Array DataTypes)
        {
            GetServer.QueryAvailableProperties(ItemID, out Count, out PropertyIDs, out Descriptions, out DataTypes);
        }

        public void GetItemProperties(string ItemID, int Count, ref Array PropertyIDs, out Array PropertyValues, out Array Errors)
        {
            GetServer.GetItemProperties(ItemID, Count, ref PropertyIDs, out PropertyValues, out Errors);
        }

        public void LookupItemIDs(string ItemID, int Count, ref Array PropertyIDs, out Array NewItemIDs, out Array Errors)
        {
            GetServer.LookupItemIDs(ItemID, Count, ref PropertyIDs, out NewItemIDs, out Errors);
        }

        private void ThisServer_ServerShutDown(string Reason)
        {
            ServerShutDown?.Invoke(Reason);
            Console?.Errors?.AppendText("Server ShutDown! Reason: " + Reason);
        }

        private IContainer components = null;
        private OpcClient[] connectedClients;

        public OpcServerConnection(IContainer container)
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

    class ServerNameTypeConverter : StringConverter
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
            var server = context.Instance as OpcServerConnection;
            var list = server.GetOPCServers("");
            return new StandardValuesCollection(list);
        }
    }
}
