using PDFdownloader;
//instantiating classes
CSVprocessor cSVprocessor1 = new CSVprocessor();
Downloader downloader = new Downloader();
Reset reset = new Reset();

CSVprocessor cSVprocessor = new CSVprocessor();
List<Link> readLinks = new List<Link>();
List<Link> logLinks = new List<Link>();

//Reading the files
readLinks = cSVprocessor.ReadCSV("GRI_2017_2020.csv");
logLinks = cSVprocessor.ReadCSV("log.csv");

//ask if the user wants to reset the log and checking if log is empty
bool willReset = reset.PromptReset();
if (willReset || reset.IsLogEmpty(logLinks, readLinks)){
    logLinks = reset.ResetLog(readLinks);
}

foreach (Link l in logLinks)
{
    Console.WriteLine(l.id.ToString() + l.pdf_url_1);
}

logLinks = downloader.DownloadPDFs(readLinks, logLinks);