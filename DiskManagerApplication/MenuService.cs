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
            Console.WriteLine("[1] - Parametry dysków");
            Console.WriteLine("[2] - Dane S.M.A.R.T. *Obsługuje prawidłowo jedynie dyski HDD");
            Console.WriteLine("[3] - Formatowanie wybranej partycji");
            Console.Write("Wybieram: ");
            mainMenuSelect = Convert.ToInt32(Console.ReadLine());

        }
    }
}
