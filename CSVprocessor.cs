using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper; 

namespace PDFdownloader
{
    public class CSVprocessor
    {
        public List<Link> ReadLinkCSV(string file){ //check with List<Link> downloadedLinks
            IEnumerable<Link> links;
            List<Link> readLinks = new List<Link>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                Console.WriteLine("Reading links...");
                links = csv.GetRecords<Link>();
                readLinks = links.ToList();
            }

            return readLinks;
        }

        public List<Log> ReadLogCSV(string file){ //check with List<Link> downloadedLinks
            IEnumerable<Log> logs;
            List<Log> logList = new List<Log>();

            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                Console.WriteLine("Reading Log...");
                logs = csv.GetRecords<Log>();
                logList = logs.ToList();
            }

            return logList;
        }

        public void WriteCSV(string file, List<Log> logLinks){ //check with List<Link> downloadedLinks

            using (var writer = new StreamWriter(file))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                Console.WriteLine("Writing to Log...");
                csv.WriteRecords(logLinks);
            }
        }
    }
}