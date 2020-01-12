using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskManagerApplication
{
    public class MenuService
    {
        public ManagementScope scope { get; set; }

        public MenuService(ManagementScope scope)
        {
            this.scope = scope;
        }

        public void MainMenu()
        {
            int mainMenuSelect;
            Console.WriteLine("Witaj w menu głównym DiskManagerApplication. Wybierz opcję:");

            MainMenuOptions();

            Console.Write("Wybieram: ");
            mainMenuSelect = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            switch (mainMenuSelect)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        DiskPartitionInformationMenu();   
                        break;
                    }
                case 2:
                    {
                        
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                default:
                    {

                        break;
                    }
            }
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

        private void MainMenuOptions()
        {
            Console.WriteLine("[1] - Parametry dysków i partycji");
            Console.WriteLine("[2] - Dane S.M.A.R.T. *Obsługuje prawidłowo jedynie dyski HDD");
            Console.WriteLine("[3] - Formatowanie wybranej partycji");
            Console.WriteLine("[0] - Wyjście z programu");
        }
        private void DiskPartitionInformationMenu()
        {
            int diskInformationSelect;

            DiskPartitionInformationMenuOptions();
            Console.Write("Wybieram: ");

            diskInformationSelect = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }
        private void DiskPartitionInformationMenuOptions()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Parametry dysków i partycji");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("[1] - Wyświetl parametry dysków"); //Automatyczne generowanie raportu .txt
            Console.WriteLine("[2] - Wyświetl parametry partycji");
            Console.WriteLine("[0] - Wyjście z programu");
        }
    }
}
