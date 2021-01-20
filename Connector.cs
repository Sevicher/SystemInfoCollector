using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoCollector
{
    public class Connector
    {
        public static List<Workstation> ConnectAndCollectInfo()
        {
            List<Workstation> workstations = new List<Workstation>();
            DirectoryContext directoryContext = new DirectoryContext(DirectoryContextType.Domain, "domain", "domain\\admin", "password");
            Domain domain = Domain.GetDomain(directoryContext);
            List<string> computerNames = new List<string>();
            DirectorySearcher mySearcher = new DirectorySearcher(domain.GetDirectoryEntry());
            mySearcher.Filter = ("(objectClass=computer)");
            mySearcher.SizeLimit = int.MaxValue;
            mySearcher.PageSize = int.MaxValue;
            foreach (SearchResult resEnt in mySearcher.FindAll())
            {
                string ComputerName = resEnt.GetDirectoryEntry().Name;
                if (ComputerName.StartsWith("CN="))
                    ComputerName = ComputerName.Remove(0, "CN=".Length);
                if (ComputerName.Contains("WS"))
                    computerNames.Add(ComputerName);
            }
            computerNames.Sort();
            ConnectionOptions connectionOptions = new ConnectionOptions()
            {
                Username = "domain\\admin",
                Password = "password",
                EnablePrivileges = true,
                Impersonation = ImpersonationLevel.Impersonate,
                Timeout = new TimeSpan(0, 0, 0, 1)
            };
            int i = 0;
            foreach (string computerName in computerNames)
            {
                ManagementScope scope = new ManagementScope($"\\\\{computerName}\\root\\cimv2", connectionOptions);
                try
                {
                    scope.Connect();
                }
                catch
                {
                    continue;
                }
                Workstation workstation = new Workstation();
                if (scope.IsConnected == true)
                {
                    workstation.Hardwares = HardwareCollector.ShowSystemInfo(scope);
                    workstation.Name = computerName;
                }
                else
                    continue;
                workstations.Add(workstation);
                i++;
            }
            return workstations;
        }
    }
}
