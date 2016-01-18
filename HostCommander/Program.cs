using System;
using System.Collections.Generic;

namespace HostCommander
{
    class Program
    {
        protected const string HostFilePath = "C:\\Windows\\System32\\drivers\\etc\\hosts";

        static void Main(string[] args)
        {
            Options options = new Options();
            CommandLine.Parser.Default.ParseArguments(args, options);
            HostFile hostFile = new HostFile(HostFilePath);

            try
            {
                switch (options.Mode)
                {
                    case "list":
                        hostFile.List();
                        break;
                    case "add":
                        hostFile.Add(options.IpAddress, options.Host);
                        Console.WriteLine(options.Host + " is now pointing at " + options.IpAddress);
                        break;
                    case "remove":
                        hostFile.Remove(options.Host);
                        Console.WriteLine(options.Host + " has been removed");
                        break;
                    case "clean":
                        hostFile.Clean();
                        Console.WriteLine("Host file has been succesfully cleaned");
                        break;
                    default:
                        Console.WriteLine("Unrecognised mode.  Consult usage guide.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Occured: " + e.Message);
            }

        }
    }
}
