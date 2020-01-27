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
            int returnCode;
            bool switchExit = true;
            int fsCode;
            string driveLetter;
            string fsName = "FAT32";
            string label;
            string confirm;

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
                            int result = ShowDiskInfo(); // dotąd nic nie edytuj
                            do
                            {
                                returnCode = ReturnMenu();
                            } while (returnCode != 0 && returnCode != 1);
                            if (returnCode == 0)
                                switchExit = true;
                            else
                                switchExit = false;
                            Console.Clear();
                                break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            DiskFormat diskFormat = new DiskFormat();

                            driveLetter = FormatingMenuOptions();

                            do
                            {

                                fsCode = ChooseFileSystemMenuOptions();

                            } while (fsCode != 1 && fsCode != 2);

                            if (fsCode == 1)
                                fsName = "FAT32";
                            else if (fsCode == 2)
                                fsName = "NTFS";

                            label = TypeDriveNameMenu();

                            do
                            {
                                confirm = ConfirmFormatingMenu(driveLetter, fsName, label);
                            } while (confirm != "t" && confirm != "n");

                            if (confirm == "t")
                            {
                                diskFormat.FormatDrive(driveLetter, fsName, label);                               
                            }
                            else if (confirm == "n")
                            {
                                Console.WriteLine("Anulowano operację ! \n");
                            }

                            do
                            {
                                returnCode = ReturnMenu();
                            } while (returnCode != 0 && returnCode != 1);
                            if (returnCode == 0)
                                switchExit = true;
                            else
                                switchExit = false;
                            Console.Clear();
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
        private int ReturnMenu()
        {
            Console.WriteLine("[0] - Wyjście z programu");
            Console.WriteLine("[1] - Wróć do menu głównego");
            Console.Write("Wybieram: ");
            return Convert.ToInt32(Console.ReadLine());
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

        private string FormatingMenuOptions()
        {
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("!****** Formatowanie wybranego dysku *******!\n");
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("[KROK 1/3] Podaj literę dysku który chcesz sformatować ( w formacie E: )");
            return Convert.ToString(Console.ReadLine());
        }

        private int ChooseFileSystemMenuOptions()
        {
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("[KROK 2/3] Wybierz system plików ");
            Console.WriteLine("[1] - FAT32");
            Console.WriteLine("[2] - NTFS \n");
            return Convert.ToInt32(Console.ReadLine());
        }

        private string TypeDriveNameMenu()
        {
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("[KROK 3/3] Podaj nową nazwę utworzonego dysku\n");
            return Convert.ToString(Console.ReadLine());
        }

        private string ConfirmFormatingMenu(string letter, string filesystem, string name)
        {
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^UWAGA^^^^^^^^^^^^^^^^^^^^\n");
            Console.WriteLine("OSTRZEŻENIE! OPERACJA USUNIE WSZELKIE DANE Z DYSKU");
            Console.WriteLine("---------------------------------------------\n");
            Console.WriteLine("Wprowadzone dane : \n");

            Console.WriteLine("[Litera formatowanego dysku] : " + letter);
            Console.WriteLine("[Wybrany system plików] :" + filesystem);
            Console.WriteLine("[Nowa nazwa dysku] : " + name + "\n");


            Console.WriteLine("---------------------------------------------\n");

            Console.WriteLine("Czy na pewno chcesz dokonać formatowania dysku (t/n) ? \n");

            return Convert.ToString(Console.ReadLine());
        }

    }
}
