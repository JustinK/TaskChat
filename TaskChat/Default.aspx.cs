using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TaskChat
{
    public partial class Default : System.Web.UI.Page
    {
        private const string TrelloBoardId = "522fc20a17a1fd7f7b001a44";
        private const string TrelloAppKey = "967d1862f7fd182536edb139eeee8197";
        private const string TrelloToken = "46e66cd37ff14c745e6fc6cc8c337ce15d2cb29657bc44f78c863c5bf0d0c783";

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Proxy server to interact with Trello API. Prevents browser security issues with cross domain requests
        [WebMethod]
        public static object TrelloApiProxy(string id, string newData, string postType)
        {
            
            if (postType.Equals("updateLabel"))
            {
                string uri = "https://api.trello.com/1/cards/" + id + "/name";
                string parameters = "key=" + TrelloAppKey + "&token=" + TrelloToken + "&value=" + newData;

                return HttpPut(uri, parameters);
            }

            if (postType.Equals("updateDescription"))
            {
                string uri = "https://api.trello.com/1/cards/" + id + "/desc";
                string parameters = "key=" + TrelloAppKey + "&token=" + TrelloToken + "&value=" + newData;

                return HttpPut(uri, parameters);
            }

            if (postType.Equals("getBoards"))
            {
                string uri = "https://api.trello.com/1/members/" + id + "/boards?fields=name&key=" + TrelloAppKey + "&token=" + TrelloToken;
                return HttpGet(uri);
            }
            return "Method not found.";
        }

        private static string HttpPut(string uri, string parameters)
        {
            WebRequest request = WebRequest.Create(uri);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "PUT";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(parameters);
            request.ContentLength = bytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            WebResponse resp = request.GetResponse();
            if (resp == null) return null;
            var sr = new StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static object HttpGet(string uri)
        {
            WebRequest req = WebRequest.Create(uri);
            WebResponse resp = req.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return sr.ReadToEnd().Trim();
        }
    }
}