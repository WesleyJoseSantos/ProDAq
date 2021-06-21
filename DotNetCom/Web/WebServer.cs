using DotNetCom.DataBase;
using DotNetCom.General.Controls;
using DotNetCom.General.NamedObject;
using DotNetCom.General.Tags;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DotNetCom.Web
{
    public enum WebServerStatus
    {
        Stopped,
        Running
    }

    [JsonObject(MemberSerialization.OptIn)]
    public partial class WebServer : Component, ITagReaderContainer
    {
        private HttpListener listener;
        private string root;
        private string homePage;

        [JsonProperty]
        [Category("General")]
        [DisplayName("Url")]
        [Description("Url to access this web server via browser.")]
        public string Url { get; set; } = "http://localhost:8000/";

        [JsonProperty]
        [Category("General")]
        [DisplayName("Home Page")]
        [Description("Html file to be home page of web server")]
        [Editor(typeof(HomePageEditor), typeof(UITypeEditor))]
        public string HomePage
        {
            get => homePage;
            set
            {
                if (value == null) return;
                var fileInfo = new FileInfo(value);
                if (fileInfo.Exists)
                {
                    homePage = value;
                    root = fileInfo.DirectoryName;
                }

            }
        }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Server Root")]
        [Description("Directory where server will search by requested files/data.")]
        public string ServerRoot { get => root; }

        [Category("General")]
        [DisplayName("Status")]
        [Description("Current status of web server.")]
        public WebServerStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                StatusChanged?.Invoke(this, null);
            }
        }

        [Browsable(false)]
        public ComponentConsole Console { get; set; }

        [JsonProperty]
        [Category("General")]
        [DisplayName("Tags Collection")]
        [Description("Collection of tags linked to this control.")]
        public TagCollection TagsCollection { get; set; }

        [JsonProperty]
        [Category("Database")]
        [Browsable(false)]
        [DisplayName("Tags Groups")]
        [Description("Tags groups to be logged this device. " +
            "Only one tags group at time will be logged. " +
            "The clien must select the desired tag group.")]
        [Editor(typeof(TagsGroupsSelectorEditor), typeof(UITypeEditor))]
        public TagGroup[] TagsGroups { get; set; }

        [Browsable(false)]
        public string ReceivedData { get; set; }

        [Browsable(false)]
        public string DataToSend { get; set; }

        [Browsable(false)]
        public bool DataAvailable { get; set; }

        private WebServerStatus _status;

        public event EventHandler StatusChanged;

        public WebServer()
        {
            InitializeComponent();
            Initialize();
        }

        public WebServer(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Initialize();
        }

        public void Start()
        {
            listener = new HttpListener();
            listener.Start();
            Console?.Errors?.Invoke((MethodInvoker)delegate
            {
                Console?.Log?.AppendText($"Web Server started on {Url}.\n");
            });

            try
            {
                listener.Prefixes.Add(Url);
            }
            catch (Exception ex)
            {
                Console?.Errors?.Invoke((MethodInvoker)delegate
                {
                    Console?.Errors?.AppendText(ex.Message + '\n');
                });
                Stop();
                return;
            }
            Status = WebServerStatus.Running;
        }

        public void Stop()
        {
            Status = WebServerStatus.Stopped;
            listener.Stop();
            listener.Prefixes.Clear();
            Console?.Errors?.Invoke((MethodInvoker)delegate
            {
                Console?.Log?.AppendText("Web Server stopped.\n");
            });
        }

        private void Initialize()
        {
            Console = new ComponentConsole();

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                while (true)
                {
                    if (File.Exists(HomePage))
                    {
                        if (Status == WebServerStatus.Running)
                        {
                            try
                            {
                                HandleIncomingConnections();
                            }
                            catch (Exception ex)
                            {
                                Console?.Errors?.Invoke((MethodInvoker)delegate
                               {
                                   Console?.Errors?.AppendText(ex.Message + '\n');
                               });
                            }
                        }
                    }
                    else if (Status == WebServerStatus.Running)
                    {
                        Status = WebServerStatus.Stopped;
                        Console?.Errors?.Invoke((MethodInvoker)delegate
                        {
                            Console?.Errors?.AppendText("Home page file doesn't exists.\n");
                        });

                        listener.Close();
                        Console?.Log?.Invoke((MethodInvoker)delegate
                        {

                            Console?.Log?.AppendText("Web Server stopped.\n");
                        });
                    }
                }
            });
        }

        private string GetContentType(string url)
        {
            if (url.EndsWith(".json") || url.EndsWith(".jso"))
            {
                return "application/json";
            }
            else if (url.EndsWith(".css"))
            {
                return "text/css";
            }
            else if (url.EndsWith(".png"))
            {
                return "image/png";
            }
            else if (url.EndsWith(".ico"))
            {
                return "image/x-icon";
            }
            else if (url.EndsWith(".js"))
            {
                return "application/javascript";
            }
            else
            {
                return "application/html";
            }
        }

        private void HandleIncomingConnections()
        {
            HttpListenerContext ctx = listener.GetContext();
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;
            string contentType = "";
            byte[] data = new byte[0];

            //Console.Log.AppendText(req.Url.ToString() + '\n');
            //Console.Log.AppendText(req.HttpMethod + '\n');
            //Console.Log.AppendText(req.UserHostName + '\n');
            //Console.Log.AppendText(req.UserAgent + '\n');
            //Console.Log.AppendText("\n");

            if (req.RawUrl == "/")
            {
                data = File.ReadAllBytes(HomePage);
                contentType = "text/html";
            }
            ProcessRequest(req, ref contentType, ref data);

            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;

            resp.OutputStream.Write(data, 0, data.Length);
            resp.Close();
        }

        private void ProcessRequest(HttpListenerRequest req, ref string contentType, ref byte[] data)
        {
            if (req.HttpMethod == "GET")
            {
                if (req.QueryString.Get("data") != null)
                {
                    data = Encoding.UTF8.GetBytes(DataToSend);
                    contentType = "application/json";
                }
                else if (req.RawUrl == "/")
                {
                    //Keep Home Page
                }
                else
                {
                    string filePath = root + req.RawUrl.Replace('/', '\\');
                    contentType = GetContentType(req.RawUrl);
                    if (File.Exists(filePath))
                    {
                        data = File.ReadAllBytes(filePath);
                    }
                    else
                    {
                        data = Encoding.UTF8.GetBytes($"Cannot GET {req.RawUrl}");
                    }
                }
                if (req.QueryString.Get("tags") != null)
                {
                    string json = JsonConvert.SerializeObject(TagsCollection.Tags);
                    data = Encoding.UTF8.GetBytes(json);
                    contentType = "application/json";
                }
                if (req.QueryString.Get("values") != null)
                {
                    var values = new List<object>();

                    if (TagsCollection.Tags == null)
                    {
                        Console?.Errors?.Invoke((MethodInvoker)delegate
                        {
                            Console?.Errors?.AppendText("Values request error, tags is null.");
                        });
                        return;
                    }
                    foreach (var tag in TagsCollection.Tags)
                    {
                        values.Add(tag.Value);
                    }

                    string json = JsonConvert.SerializeObject(values);
                    data = Encoding.UTF8.GetBytes(json);
                    contentType = "application/json";
                }
            }
        }
    }

    class HomePageEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "Web Page File | *.html; *.htm";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    value = fileDialog.FileName;
                }
            }
            return value;
        }
    }
}
