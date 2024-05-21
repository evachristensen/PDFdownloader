using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Http;

namespace PDFdownloader
{
    public class Downloader
    {
        public async Task<List<Log>> DownloadPDFs(List<Link> downloadLinks, List<Log> logLinks)
        {
            List<Log> newLogLinks = logLinks;
            List<Task> taskList = new List<Task>();
            //loop through list
            foreach (Link l in downloadLinks)
            {
                int myID = l.id;
                if (logLinks[myID - 1].downloadStatus == null)
                {
                    Log i = new Log { id = myID, downloadStatus = "" };
                    logLinks[myID - 1] = i;
                }

                string downloadStatus = logLinks[myID - 1].downloadStatus;

                if (logLinks[myID - 1].downloadStatus == "")
                {
                    Console.WriteLine("getting file " + logLinks[myID - 1].id);
                    using (WebClient client = new()) //using webclient even though it may give me issues. I might try using Ihhtpfactory
                    {
                        client.Proxy = null;
                        bool canDownloadPDFB = true;

                        taskList.Add(tryDownload(client, l, myID, newLogLinks, "B", canDownloadPDFB));
                        if (!canDownloadPDFB)
                        {
                            taskList.Add(tryDownload(client, l, myID, newLogLinks, "C", canDownloadPDFB));
                        }

                    }

                }

            }
            await Task.WhenAll(taskList);

            foreach (Log a in newLogLinks)
            {
                Console.WriteLine("id: " + a.id + ", download status " + a.downloadStatus);
            }
            return newLogLinks;
        }

        public async Task tryDownload(WebClient client, Link l, int myID, List<Log> newLogLinks, string column, bool canDownloadPDFB)
        {
            try
            {
                string? url = column switch
                {
                    "B" => l.pdf_url_1,
                    "C" => l.pdf_url_2,
                    _ => ""
                };
                
                Uri uri = new Uri(url);

                var result = await client.DownloadDataTaskAsync(uri);
                var contentType = client.ResponseHeaders["Content-Type"];

                if (contentType == "application/pdf")
                {
                    await client.DownloadFileTaskAsync(uri, "./pdfs/" + myID + column + ".pdf");
                    newLogLinks[myID - 1].downloadStatus = column;
                    Console.WriteLine("Attempt " + column + " downloaded pdf " + myID);
                    canDownloadPDFB = true;
                }
                else
                {
                    newLogLinks[myID - 1].downloadStatus = "";
                    canDownloadPDFB = false;
                    throw new ArgumentException("Filetype must be PDF");
                }
            }
            catch (Exception f)
            {
                canDownloadPDFB = false;
                Console.WriteLine("Attempt " + column + " failed to download pdf " + myID);
                Console.WriteLine(f.Message);
                newLogLinks[myID - 1].downloadStatus = "";
            }
        }
    }
}