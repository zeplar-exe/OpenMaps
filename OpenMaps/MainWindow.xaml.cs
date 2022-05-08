using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenMaps.Common;
using OpenMaps.Controls;
using OpenMaps.Controls.Drawing;
using OpenMaps.Controls.Drawing.Brushes;
using OpenMaps.MVVM;

namespace OpenMaps
{
    public partial class MainWindow : Window
    {
        public MainMenuViewModel ViewModel { get; }
        
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = (MainMenuViewModel)DataContext;
            
            ViewModel.RecentFiles.Add(new RecentFileInfo("Test", @"C:\Test", new BitmapImage()));

            var dpi = VisualTreeHelper.GetDpi(this);

            var displaySize = new IntVector(500, 500);
            var display = new BitmapDisplay(displaySize, dpi);
            
            display.Lock();
            
            display.SetRect(
                new Int32Rect(0, 0, displaySize.X, displaySize.Y), 
                new ColorPixel(Colors.White));
            
            display.Unlock();
            
            ViewModel.CanvasWidth.Value = 3000;
            ViewModel.CanvasHeight.Value = 3000;
            ViewModel.PixelDisplay.Value = display;
            ViewModel.DrawBrush.Value = new CircleBrush(new ColorPixel(Colors.Black), 5);
        }
        
        private void Zoom_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var zoomControl = (ZoomControl)sender;
            
            if (e.Delta > 0)
            {
                zoomControl.ZoomIn();
            }
            else
            {
                zoomControl.ZoomOut();
            }
        }
    }
}