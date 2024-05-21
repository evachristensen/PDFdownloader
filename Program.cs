using PDFdownloader;
//instantiating classes
CSVprocessor cSVprocessor1 = new CSVprocessor();
Downloader downloader = new Downloader();
Reset reset = new Reset();

CSVprocessor cSVprocessor = new CSVprocessor();
List<Link> readLinks = new();
List<Log> logLinks = new();

//choosing which file to read from
string? CSVname = "GRI_2017_2020.csv";
// if (reset.ReadUserInput() )
// {
//     string? CSVname = reset.ReadUserInput();
//     try
//     {
//         string? userInput = "";
//         while (userInput == "")
//         {
//             userInput = Console.ReadLine();
//         }
        
//     }
//     catch (Exception e)
//     {
//         Console.WriteLine(e.Message);
        
//     }
// }

//Reading the files
readLinks = cSVprocessor.ReadLinkCSV(CSVname);
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
            logLinks = downloader.DownloadPDFs(readLinks, logLinks).Result;
            break;
        case "n":
            Console.WriteLine("nope");
            break;
    }
}

