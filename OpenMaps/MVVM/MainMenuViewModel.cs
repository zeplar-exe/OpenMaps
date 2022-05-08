using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using OpenMaps.Controls.Drawing;
using OpenMaps.Controls.Drawing.Brushes;

namespace OpenMaps.MVVM;

public class MainMenuViewModel
{
    public ObservableProperty<int> CanvasWidth { get; }
    public ObservableProperty<int> CanvasHeight { get; }
    
    public ObservableProperty<IPixelDisplay> PixelDisplay { get; }
    public ObservableProperty<IDrawBrush> DrawBrush { get; }

    public ObservableCollection<RecentFileInfo> RecentFiles { get; }

    public MainMenuViewModel()
    {
        PixelDisplay = new ObservableProperty<IPixelDisplay>();
        DrawBrush = new ObservableProperty<IDrawBrush>();
        CanvasWidth = new ObservableProperty<int>();
        CanvasHeight = new ObservableProperty<int>();
        
        RecentFiles = new ObservableCollection<RecentFileInfo>();
    }
}

public readonly record struct RecentFileInfo(string ShortName, string FullPath, ImageSource Image);