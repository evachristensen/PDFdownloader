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
        public List<Log> DownloadPDFs(List<Link> downloadLinks, List<Log> logLinks)
        {
            List<Log> newLogLinks = logLinks;
            //loop through list
            foreach (Link l in downloadLinks)
            {
                int myID = l.id;
                if (logLinks[myID - 1].downloadStatus == null){
                    Log i = new Log{id = myID, downloadStatus = ""};
                    logLinks[myID - 1] = i;
                }
                
                string downloadStatus = logLinks[myID - 1].downloadStatus;
                
                if (logLinks[myID - 1].downloadStatus == "")
                {
                    Console.WriteLine("getting file " + logLinks[myID - 1].id);
                    using (WebClient client = new()) //using webclient even though it may give me issues. I might try using Ihhtpfactory
                    {
                        int attempt = 1;

                        try
                        {
                            var result = client.DownloadData(l.pdf_url_1);
                            string? contentType = client.ResponseHeaders["Content-Type"];

                            if (contentType == "application/pdf")
                            {
                                client.DownloadFile(l.pdf_url_1, "./pdfs/" + myID + "B.pdf");
                                newLogLinks[myID-1].downloadStatus = "B";
                                Console.WriteLine("Attempt " + attempt + " downloaded pdf " + myID);
                            }
                            else
                            {
                                Console.WriteLine("Attempt " + attempt + " failed to download pdf " + myID);
                                newLogLinks[myID-1].downloadStatus = "";
                                attempt = 2;
                                throw new ArgumentException("Filetype must be PDF");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Attempt " + attempt + " failed to download pdf " + myID);
                            Console.WriteLine(e.Message);
                            newLogLinks[myID-1].downloadStatus = "";
                            attempt = 2;
                        }

                        if (attempt == 2)
                        {
                            try
                            {
                                var result = client.DownloadData(l.pdf_url_2);
                                var contentType = client.ResponseHeaders["Content-Type"];

                                if (contentType == "application/pdf")
                                {
                                    client.DownloadFile(l.pdf_url_2, "./pdfs/" + myID + "C.pdf");
                                    newLogLinks[myID-1].downloadStatus = "C";
                                    Console.WriteLine("Attempt " + attempt + " downloaded pdf " + myID);
                                }
                                else
                                {
                                    Console.WriteLine("Attempt " + attempt + " failed to download pdf " + myID);
                                    newLogLinks[myID-1].downloadStatus = "";
                                    throw new ArgumentException("Filetype must be PDF");
                                }
                            }
                            catch (Exception f)
                            {
                                Console.WriteLine("Attempt " + attempt + " failed to download pdf " + myID);
                                Console.WriteLine(f.Message);
                                newLogLinks[myID-1].downloadStatus = "";
                            }
                        }
                    }

                }

            }
            foreach (Log a in newLogLinks){
                Console.WriteLine("id: " + a.id + ", download status " + a.downloadStatus);
            }
            return newLogLinks;
        }
    }
}