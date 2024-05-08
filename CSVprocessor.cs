using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper; 

namespace PDFdownloader
{
    public class CSVprocessor
    {
        public List<Link> ReadCSV(string file){ //check with List<Link> downloadedLinks
            IEnumerable<Link> links;
            List<Link> readLinks = new List<Link>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                links = csv.GetRecords<Link>();
                readLinks = links.ToList();
            }

            return readLinks;
        }

        public void WriteCSV(List<Link> logLinks){ //check with List<Link> downloadedLinks

            using (var writer = new StreamWriter("log.csv"))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(logLinks);
            }
        }
    }
}