using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management;
using System.IO;

namespace SystemInfoCollector
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Hardware> hardwares = ShowSystemInfo();
            HardwareInfo.ItemsSource = hardwares;
        }


        private Hardware GetHardware(ManagementObjectCollection collection, string name, string result)
        {
            Hardware hardware = new Hardware { Name = name };
            foreach(ManagementObject queryObj in collection)
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
        private List<Hardware> GetComputerSystemHardWare()
        {
            ManagementObjectCollection ComputerSystem =
                new ManagementObjectSearcher("root\\CIMV2",
                "SELECT * FROM Win32_ComputerSystem").Get();
            Hardware domain = GetHardware(ComputerSystem, "Имя домена", "Domain");
            Hardware domainRole = GetHardware(ComputerSystem, "Доменная роль", "DomainRole");
            Hardware motherboard = GetHardware(ComputerSystem, "Материнская плата", "Model");
            Hardware computerName = GetHardware(ComputerSystem, "Имя компьютера", "Name");
            Hardware user = GetHardware(ComputerSystem, "Имя пользователя", "UserName");
            Hardware[] templist = new Hardware[] { domain, domainRole, motherboard, computerName, user};
            List<Hardware> outlist =  templist.ToList();
            return outlist;
        }
        private List<Hardware> ShowSystemInfo()
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
            hardwareList.Add(monitor);
            hardwareList.Add(keyboard);
            
            return hardwareList;
        }
    }
}
