using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OpenMaps.Common;
using OpenMaps.Controls;
using OpenMaps.Controls.Drawing;
using OpenMaps.MVVM;
using OpenMaps.Tools;
using OpenMaps.Tools.Brushes;
using OpenMaps.Tools.Metrics;

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
            
            ViewModel.CanvasWidth.Value = 1000;
            ViewModel.CanvasHeight.Value = 1000;
            ViewModel.PixelDisplay.Value = display;

            var circularBrush = new CircularStrokeBrush
            {
                Radius = { Value = 2 }, 
                Color = new ColorPixel(Colors.Black)
            };
            var rectangularBrush = new RectangularStokeBrush
            {
                Width = { Value = 5 },
                Height = { Value = 5 },
                StrokeWidth = { Value = 1 },
                Color = new ColorPixel(Colors.Black),
                StrokeColor = new ColorPixel(Colors.Black)
            };
            
            ViewModel.Tools.Add(new ToolObject(circularBrush));
            ViewModel.Tools.Add(new ToolObject(rectangularBrush));
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

        private void ToolButton_Clicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var toolObject = (ToolObject)button.DataContext;

            ViewModel.SelectedTool.Value = toolObject.Tool;
        }
    }
}