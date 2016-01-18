using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel.Registration.Proxy;

namespace HostCommander
{
    class HostFile
    {
        protected string Line;
        protected bool TopCommentParsed = false;
        protected string HostFilePath;

        public HostFile(string hostFilePath)
        {
            this.HostFilePath = hostFilePath;
        }

        public void Add(string ip, string host)
        {
            StreamWriter hostFile = OpenForWriting();
            hostFile.WriteLine(ip + " " + host);
            hostFile.Close();
        }

        public void Remove(string host)
        {
            string[] hostFile = File.ReadAllLines(HostFilePath);
            List<string> hostFileList = new List<string>(hostFile);
            hostFileList.RemoveAt(hostFileList.FindIndex(s => s.Contains(host)));
            File.WriteAllLines(HostFilePath, hostFileList);
        }

        public void List()
        {
            List<string> hostList = GetHostList();
            foreach (var s in hostList)
            {
                Console.WriteLine(s);
            }
        }

        public void Clean()
        {
            string[] hostFile = File.ReadAllLines(HostFilePath);
            List<string> hostFileList = new List<string>(hostFile);
            hostFileList.RemoveAll(v => v == " ");
            File.WriteAllLines(HostFilePath, hostFileList);
        }

        protected List<string> GetHostList()
        {
            List<string> hostList = new List<string>();
            StreamReader hostFile = OpenForReading();
            while ((Line = hostFile.ReadLine()) != null)
            {
                // Let's ignore the comments at the top.
                if (!TopCommentParsed)
                {
                    if (Line.StartsWith("#"))
                    {
                        TopCommentParsed = true;
                    }

                    continue;
                }

                // Ignore blank lines
                if (Line.IsNullOrEmpty())
                {
                    continue;
                }

                hostList.Add(Line);
            }


            return hostList;
        }

        protected StreamReader OpenForReading()
        {
            return new StreamReader(HostFilePath);
        }

        protected StreamWriter OpenForWriting()
        {
            return File.AppendText(HostFilePath);
        }


    }
}
