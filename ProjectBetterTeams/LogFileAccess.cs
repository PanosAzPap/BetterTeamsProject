using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectBetterTeams
{
    public class LogFileAccess
    {

        public void LogUser(string UserName)
        {
            bool AccessNotGranted;
            do
            {
                AccessNotGranted = false;
                try
                {
                    DateTime dateTime = DateTime.Now;
                    string[] Entry = new string[] {
                        $"{UserName},{dateTime}"
                    };
                    System.IO.File.AppendAllLines("Logs.txt", Entry);
                }
                catch (Exception)
                {
                    File.Create("Logs.txt");
                    AccessNotGranted = true;
                }
            } while (AccessNotGranted);
        }

    }
}
