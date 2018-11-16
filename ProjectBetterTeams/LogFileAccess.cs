using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

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
                    Console.WriteLine("Creating Log File...");
                    File.Create("Logs.txt");
                    AccessNotGranted = true;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
            } while (AccessNotGranted);
        }

        public void DeleteLogs()
        {
            try
            {
                Console.WriteLine("Deleting Logs...");
                System.IO.File.Delete("Logs.txt");
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                return;
            }
            Console.WriteLine("Done!");
        }
    }
}
