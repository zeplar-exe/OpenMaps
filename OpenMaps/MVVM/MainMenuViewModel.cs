using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Media;
using OpenMaps.Controls.Drawing;
using OpenMaps.Tools;
using OpenMaps.Tools.Brushes;

namespace OpenMaps.MVVM;

public class MainMenuViewModel
{
    public ObservableProperty<int> CanvasWidth { get; }
    public ObservableProperty<int> CanvasHeight { get; }
    
    public ObservableProperty<Tool> SelectedTool { get; }
    public ObservableProperty<IPixelDisplay> PixelDisplay { get; }

    public ObservableCollection<RecentFileInfo> RecentFiles { get; }
    
    public ObservableCollection<ToolObject> Tools { get; }

    public MainMenuViewModel()
    {
        SelectedTool = new ObservableProperty<Tool>();
        PixelDisplay = new ObservableProperty<IPixelDisplay>();
        CanvasWidth = new ObservableProperty<int>();
        CanvasHeight = new ObservableProperty<int>();
        
        RecentFiles = new ObservableCollection<RecentFileInfo>();
        
        Tools = new ObservableCollection<ToolObject>();
    }
}

public readonly record struct RecentFileInfo(string ShortName, string FullPath, ImageSource Image);