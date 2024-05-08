using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDFdownloader
{
    public class Reset
    {
        public bool IsLogEmpty(List<Link> log, List<Link> readList)
        {
            if (log.Count() == readList.Count())
            {
                return false;
            }
            else
            {
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
                    Console.WriteLine("Not resetting log.");
                    return false;
            }
            return false;
        }

        public List<Link> ResetLog(List<Link> l)
        {
            Console.WriteLine("Log reset.");
            return l;
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