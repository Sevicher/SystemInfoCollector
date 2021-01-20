using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SystemInfoCollector
{
    public class HardwareCollector
    {
        private static Hardware GetHardware(ManagementObjectCollection collection, string name, string result)
        {
            Hardware hardware = new Hardware { Name = name };
            foreach (ManagementObject queryObj in collection)
            {
                try
                {
                    hardware.Result = queryObj[result].ToString();
                }
                catch
                {
                    hardware.Result = "Не удалось получить данные";
                }
            }
            return hardware;
        }
        private static List<Hardware> GetComputerSystemHardWare()
        {
            ManagementObjectCollection ComputerSystem =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_ComputerSystem").Get();
            Hardware domain = GetHardware(ComputerSystem, "Имя домена", "Domain");
            Hardware domainRole = GetHardware(ComputerSystem, "Доменная роль", "DomainRole");
            Hardware motherboard = GetHardware(ComputerSystem, "Материнская плата", "Model");
            Hardware computerName = GetHardware(ComputerSystem, "Имя компьютера", "Name");
            Hardware user = GetHardware(ComputerSystem, "Имя пользователя", "UserName");
            Hardware[] templist = new Hardware[] { domain, domainRole, motherboard, computerName, user };
            List<Hardware> outlist = templist.ToList();
            return outlist;
        }
        private static List<Hardware> GetComputerSystemHardWare(ManagementScope scope)
        {
            ObjectQuery computerSystemQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ManagementObjectCollection ComputerSystem =
                new ManagementObjectSearcher(scope, computerSystemQuery).Get();
            Hardware domain = GetHardware(ComputerSystem, "Имя домена", "Domain");
            Hardware domainRole = GetHardware(ComputerSystem, "Доменная роль", "DomainRole");
            Hardware motherboard = GetHardware(ComputerSystem, "Материнская плата", "Model");
            Hardware computerName = GetHardware(ComputerSystem, "Имя компьютера", "Name");
            Hardware user = GetHardware(ComputerSystem, "Имя пользователя", "UserName");
            Hardware[] templist = new Hardware[] { domain, domainRole, motherboard, computerName, user };
            List<Hardware> outlist = templist.ToList();
            return outlist;
        }
        public static List<Hardware> ShowSystemInfo()
        {
            List<Hardware> hardwareList = new List<Hardware>();
            hardwareList.AddRange(GetComputerSystemHardWare());
            ManagementObjectCollection desktopMonitorCollection =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_DesktopMonitor").Get();
            Hardware monitor = GetHardware(desktopMonitorCollection, "Основной монитор", "Name");
            ManagementObjectCollection keyboardCollection = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Keyboard").Get();
            Hardware keyboard = GetHardware(keyboardCollection, "Клавиатура", "Name");
            ManagementObjectCollection processorCollection = new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Processor").Get();
            Hardware processor = GetHardware(processorCollection, "Процессор", "Name");
            ManagementObjectCollection videoControllerCollection = new ManagementObjectSearcher("root\\CIMV2",
    "SELECT * FROM Win32_VideoController").Get();
            Hardware videoController = GetHardware(videoControllerCollection, "Видеокарта", "Name");
            hardwareList.Add(videoController);
            hardwareList.Add(monitor);
            hardwareList.Add(processor);
            hardwareList.Add(keyboard);

            return hardwareList;
        }

        public static List<Hardware> ShowSystemInfo(ManagementScope scope)
        {
            List<Hardware> hardwareList = new List<Hardware>();
            hardwareList.AddRange(GetComputerSystemHardWare(scope));
            ObjectQuery monitorQuery = new ObjectQuery("SELECT * FROM Win32_DesktopMonitor");
            ManagementObjectCollection desktopMonitorCollection =
                new ManagementObjectSearcher(scope, monitorQuery).Get();
            Hardware monitor = GetHardware(desktopMonitorCollection, "Основной монитор", "Name");
            ObjectQuery keyboardQuery = new ObjectQuery("SELECT * FROM Win32_Keyboard");
            ManagementObjectCollection keyboardCollection = new ManagementObjectSearcher(scope, keyboardQuery).Get();
            Hardware keyboard = GetHardware(keyboardCollection, "Клавиатура", "Name");
            ObjectQuery processorQuery = new ObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectCollection processorCollection = new ManagementObjectSearcher(scope, processorQuery).Get();
            Hardware processor = GetHardware(processorCollection, "Процессор", "Name");
            ObjectQuery videoControllerQuery = new ObjectQuery("SELECT * FROM Win32_VideoController");
            ManagementObjectCollection videoControllerCollection = new ManagementObjectSearcher(scope, videoControllerQuery).Get();
            Hardware videoController = GetHardware(videoControllerCollection, "Видеокарта", "Name");
            hardwareList.Add(videoController);
            hardwareList.Add(monitor);
            hardwareList.Add(processor);
            hardwareList.Add(keyboard);
            return hardwareList;
        }

    }
}
