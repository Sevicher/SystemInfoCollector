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

        private List<Hardware> ShowSystemInfo()
        {
            List<Hardware> hardwareList = new List<Hardware>();
            Hardware osVersion = new Hardware
            {
                Name = "Операционная система (номер версии)",
                Result = Environment.OSVersion.ToString()
            };
            hardwareList.Add(osVersion);
            Hardware procArch = new Hardware
            {
                Name = "Разрядность процессора",
                Result = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToString()
            };
            hardwareList.Add(procArch);
            Hardware procModel = new Hardware
            {
                Name = "Модель процессора",
                Result = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER").ToString()
            };
            hardwareList.Add(procModel);
            Hardware procCount = new Hardware
            {
                Name = "Число процессоров",
                Result = Environment.ProcessorCount.ToString()
            };
            hardwareList.Add(procCount);
            Hardware systemDirectory = new Hardware
            {
                Name = "Операционная система (номер версии)",
                Result = Environment.OSVersion.ToString()
            };
            hardwareList.Add(systemDirectory);
            Hardware userName = new Hardware
            {
                Name = "Имя пользователя",
                Result = Environment.UserName.ToString()
            };
            hardwareList.Add(userName);
            Hardware userDomainName = new Hardware
            {
                Name = "Доменное имя пользователя",
                Result = Environment.UserDomainName.ToString()
            };
            hardwareList.Add(userDomainName);
            //// Локальные диски
            //Console.WriteLine("Локальные диски: ");
            //foreach (DriveInfo dI in DriveInfo.GetDrives())
            //{
            //    Console.Write(
            //          "\t Диск: {0}\n\t" +
            //          " Формат диска: {1}\n\t " +
            //          "Размер диска (ГБ): {2}\n\t Доступное свободное место (ГБ): {3}\n",
            //          dI.Name, dI.DriveFormat, (double)dI.TotalSize / 1024 / 1024 / 1024, (double)dI.AvailableFreeSpace / 1024 / 1024 / 1024);
            //    Console.WriteLine();
            //}
            return hardwareList;
        }
    }
}
