using System.IO;
using System.Net;

namespace Atika
{
    public partial class MainActivity
    {
        class Internet
        {
            // [ Server Connections ]

            // Connect to WAN Server
            public string Connect(string serverLocationLink, string tempServerLocation)
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile(serverLocationLink, tempServerLocation);

                return File.ReadAllText(tempServerLocation);
            }
        }
    }
}
