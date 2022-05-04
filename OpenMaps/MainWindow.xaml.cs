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
using OpenMaps.MVVM;

namespace OpenMaps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainMenuViewModel ViewModel { get; }
        
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = (MainMenuViewModel)DataContext;
            
            ViewModel.RecentFiles.Add(new RecentFileInfo("Test", @"C:\Test", new BitmapImage()));
        }
    }
}