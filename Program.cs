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

//checking if log is empty, and if it is then we set it equal to the list to download
if (reset.IsLogEmpty(logLinks, readLinks)){ //can I combine this with ResetLog()?
    logLinks = readLinks;
}

//ask if the user wants to reset the log
bool willReset = reset.PromptReset();
if (willReset){
    logLinks = reset.ResetLog(readLinks);
}

foreach (Link l in logLinks)
{
    Console.WriteLine(l.id.ToString() + l.pdf_url_1);
}

logLinks = downloader.DownloadPDFs(readLinks, logLinks);