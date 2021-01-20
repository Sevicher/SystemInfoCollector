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
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices;

namespace SystemInfoCollector
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Workstation> _workStations;
        public MainWindow()
        {
            InitializeComponent();
            List<Hardware> hardwares = HardwareCollector.ShowSystemInfo();
            HardwareInfo.ItemsSource = hardwares;
            List<Workstation> workstations = Connector.ConnectAndCollectInfo();
            WorkStations.ItemsSource = workstations.Select(w => w.Name);
            _workStations = workstations;
        }

        private void WorkStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HardwareInfo.ItemsSource = _workStations[WorkStations.SelectedIndex].Hardwares;
        }
    }
}
