using PDFdownloader;
//instantiating classes
CSVprocessor cSVprocessor1 = new CSVprocessor();
Downloader downloader = new Downloader();
Reset reset = new Reset();

CSVprocessor cSVprocessor = new CSVprocessor();
List<Link> readLinks = new List<Link>();
List<Log> logLinks = new List<Log>();

//Reading the files
readLinks = cSVprocessor.ReadLinkCSV("GRI_2017_2020.csv");
logLinks = cSVprocessor.ReadLogCSV("log.csv");

//ask if the user wants to reset the log and checking if log is empty
bool willReset = reset.PromptReset();
if (willReset || reset.IsLogEmpty(logLinks, readLinks))
{
    logLinks = reset.ResetLog(readLinks);
}

Download();

cSVprocessor1.WriteCSV("log.csv", logLinks);

void Download()
{
    Console.WriteLine("Would you like to download? Y/N");
    string? dl = reset.ReadUserInput();

    switch (dl)
    {
        case "y":
            Console.WriteLine("downloading...");
            logLinks = downloader.DownloadPDFs(readLinks, logLinks);
            break;
        case "n":
            Console.WriteLine("nope");
            break;
    }
}

