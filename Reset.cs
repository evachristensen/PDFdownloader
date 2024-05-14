using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDFdownloader
{
    public class Reset
    {
        public bool IsLogEmpty(List<Log> log, List<Link> readList)
        {
            if (log.Count() == readList.Count())
            {
                return false;
            }
            else
            {
                Console.WriteLine("Log is empty. Resetting log...");
                return true;
            }


        }
        public bool PromptReset()
        {
            Console.WriteLine("Would you like to reset the file download log? Y/N");
            string? reset = ReadUserInput();

            switch (reset)
            {
                case "y":
                    Console.WriteLine("Resetting log...");
                    return true;
                case "n":
                    return false;
            }
            return false;
        }

        public List<Log> ResetLog(List<Link> linkList)
        {
            List<Log> resetList = new();
            
            foreach (Link l in linkList){
                Log i = new Log{id = l.id, downloadStatus = ""};
                resetList.Add(i);
            }
            Console.WriteLine("Log reset.");
            return resetList;
        }

        public string? ReadUserInput()
        {
            try
            {
                string? userInput = "";
                while (userInput == "")
                {
                    userInput = Console.ReadLine();
                }
                return userInput.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}