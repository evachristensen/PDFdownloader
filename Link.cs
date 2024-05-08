using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDFdownloader
{
    public class Link
    {
        public int id { get; set; } //id
        public string? pdf_url_1 { get; set; } //pdf link
        public string? pdf_url_2 { get; set; } //reserve link
        public string downloadStatus = ""; //"B", "C" or ""
    }
}