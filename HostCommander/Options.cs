using System.Text;
using CommandLine;

namespace HostCommander
{
    class Options
    {
        
        [HelpOption]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("HostCommander v0.1");
            usage.AppendLine("Usage:  HostCommander -m mode -h host -i ip");
            return usage.ToString();
        }

        [Option('m', "mode", Required = true, HelpText = "Add or Remove")]
        public string Mode { get; set; }

        [Option('h', "host", Required = false, HelpText = "Host File")]
        public string Host { get; set; }

        [Option('i', "ip", Required = false, HelpText = "Host File")]
        public string IpAddress { get; set; }
    }
}
