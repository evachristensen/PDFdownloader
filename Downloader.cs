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
        public List<Link> DownloadPDFs(List<Link> downloadLinks, List<Link> logLinks)
        {
            List<Link> newLogLinks = logLinks;
            //loop through list
            foreach (Link l in downloadLinks)
            {
                int id = l.id;
                if (logLinks[id].downloadStatus == "")
                {
                    //try to download 1
                    using (WebClient client = new())
                    {
                        int attempt = 1;
                        try
                        {
                            client.DownloadFile(l.pdf_url_1, "./pdfs/" + id + "B.pdf");
                            newLogLinks[id].downloadStatus = "B";
                            Console.WriteLine("Attempt " + attempt + " downloaded pdf " + id);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Attempt " + attempt + " failed to download pdf " + id);
                            Console.WriteLine(e.Message);
                            newLogLinks[id].downloadStatus = "";
                            attempt = 2;

                            try
                            {
                                client.DownloadFile(l.pdf_url_2, "./pdfs/" + id + "C.pdf");
                                newLogLinks[id].downloadStatus = "C";
                                Console.WriteLine("Attempt " + attempt + " downloaded pdf " + id);
                            }
                            catch (Exception f)
                            {
                                Console.WriteLine("Attempt " + attempt + " failed to download pdf " + id);
                                Console.WriteLine(f.Message);
                                newLogLinks[id].downloadStatus = "";

                            }
                        }
                    }

                }

                //try to download 2
                //if download is successful, then update logLinks

            }
            return newLogLinks;
        }
    }
}