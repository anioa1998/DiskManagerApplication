using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.InteropServices;

namespace DiskManagerApplication
{    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConnectionService connectionService = new ConnectionService();

                ConnectionOptions options = new ConnectionOptions();
                options.Impersonation = connectionService.SetImpersonationLevel();

                ManagementScope scope = connectionService.GetCIMConnection(options);
                scope.Connect();

                MenuService menuService = new MenuService(scope);

                menuService.MainMenu();

                menuService.ExitMessage();
                    
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
    }

}
