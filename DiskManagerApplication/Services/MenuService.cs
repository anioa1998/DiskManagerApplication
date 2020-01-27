using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication
{
    public class MenuService
    {

        [DllImport(@"DiskInfoCppLibrary.dll", EntryPoint = "disk_info", CallingConvention = CallingConvention.StdCall)]
        public static extern int ShowDiskInfo();

        public ManagementScope scope { get; set; }

        public MenuService(ManagementScope scope)
        {
            this.scope = scope;
        }

        public void MainMenu()
        {
            int mainMenuSelect;
            bool switchExit = true;
            
            do
            {
                Console.WriteLine("Witaj w menu głównym DiskManagerApplication. Wybierz opcję:");

                MainMenuOptions();
                mainMenuSelect = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switchExit = true;

                switch (mainMenuSelect)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            switchExit = DiskPartitionInformationMenu();
                            break;
                        }
                    case 2:
                        {
                            int result = ShowDiskInfo();
                            Console.WriteLine(result); // dotąd nic nie edytuj
                            Console.ReadLine();
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wybrana opcja nie istnieje. Wybierz ponownie.\n");
                            switchExit = false;
                            break;
                        }
                }

            } while (!switchExit);
        }

        private bool DiskPartitionInformationMenu()
        {
            DiskPartitionInformation diskPartitionInformation = new DiskPartitionInformation(scope);
            int diskInformationSelect;

            bool escapeFromMenu  = true;
            bool switchExit = true;
            do
            {
            DiskPartitionInformationMenuOptions();

            diskInformationSelect = Convert.ToInt32(Console.ReadLine());
  
                switchExit = true;
                switch (diskInformationSelect)
                {
                    case 0:
                        {
                            break;
                        }
                    case 1:
                        {
                            escapeFromMenu = diskPartitionInformation.ShowDiskInformation();
                            break;
                        }
                    case 2:
                        {
                            escapeFromMenu = diskPartitionInformation.ShowPartitionInformation();
                            break;
                        }
                    case 3:
                        {
                            escapeFromMenu = diskPartitionInformation.ShowLogicalDiskInformation();
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Wybrana opcja nie istnieje. Wybierz ponownie.\n");
                            switchExit = false;
                            break;
                        }
                }
            } while (!switchExit);

            Console.Clear();
            return escapeFromMenu;
        }

        private void MainMenuOptions()
        {
            Console.WriteLine("[1] - Parametry dysków HDD, SSD, USB i ich partycji");
            Console.WriteLine("[2] - Parametry dysków HDD, SSD, USB z wykorzystaniem C++ [NOWE]");
            Console.WriteLine("[3] - Dane S.M.A.R.T. *Obsługuje prawidłowo jedynie dyski HDD");
            Console.WriteLine("[4] - Formatowanie wybranej partycji");
            Console.WriteLine("[0] - Wyjście z programu");
            Console.Write("Wybieram: ");
        }
       
        private void DiskPartitionInformationMenuOptions()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Parametry dysków i partycji");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("[1] - Wyświetl parametry dysków"); //Automatyczne generowanie raportu .txt
            Console.WriteLine("[2] - Wyświetl parametry partycji");
            Console.WriteLine("[3] - Wyświetl parametry dysków logicznych");
            Console.WriteLine("[0] - Wyjście z programu");
            Console.Write("Wybieram: ");
        }

        public void ExitMessage()
        {
            Console.WriteLine("Dziękujemy za skorzystanie z naszego programu\n");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Utworzyli: Sebastian Franc, Anna Piechowska");
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("Wciśnij dowolny klawisz, aby zakończyć");
            Console.ReadKey();
        }
    }
}
