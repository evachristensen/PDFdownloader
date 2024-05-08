using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

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
                    //try to download 2
                    //if download is successful, then update logLinks
                }
            }          

            return newLogLinks;
        }
    }
}