using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication
{
    public class DiskFormat
    {
        public bool FormatDrive(string driveLetter, string fileSystem, string label, bool quickFormat = true,
                                   int clusterSize = 8192, bool enableCompression = false)
        {
            
            if (driveLetter.Length != 2 || driveLetter[1] != ':' || !char.IsLetter(driveLetter[0]))
            {
                Console.WriteLine("Wprowadzono nieprawidłową literę dysku ! \n");
                return false;
            }

            var files = Directory.GetFiles(driveLetter);
            var directories = Directory.GetDirectories(driveLetter);

            foreach (var item in files)
            {
                try
                {
                    File.Delete(item);
                }
                catch (UnauthorizedAccessException) { }
                catch (IOException) { }
            }

            foreach (var item in directories)
            {
                try
                {
                    Directory.Delete(item);
                }
                catch (UnauthorizedAccessException) { }
                catch (IOException) { }
            }

            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"select * from Win32_Volume WHERE DriveLetter = '" + driveLetter + "'");
            foreach (ManagementObject vi in searcher.Get())
            {
                try
                {
                    var completed = false;
                    var watcher = new ManagementOperationObserver();

                    watcher.Completed += (sender, args) =>
                    {
                        Console.WriteLine("---------------------------------------------\n");
                        Console.WriteLine(" FORMATOWANIE DYSKU UKOŃCZONE !");
                        Console.WriteLine("---------------------------------------------\n");
                        Console.WriteLine("Status błędów : " + args.Status);
                        Console.WriteLine("\n");

                        completed = true;
                    };
                    watcher.Progress += (sender, args) =>
                    {
                        Console.WriteLine("Formatowanie w toku ... " + args.Current);
                    };

                    vi.InvokeMethod(watcher, "Format", new object[] { fileSystem, quickFormat, clusterSize, label, enableCompression });

                    while (!completed) { System.Threading.Thread.Sleep(1000); }

                }
                catch
                {

                }
            }
            return true;
        }
    }
}
