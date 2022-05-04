using System.Collections.ObjectModel;
using System.Windows.Media;

namespace OpenMaps.MVVM;

public class MainMenuViewModel
{
    public ObservableCollection<RecentFileInfo> RecentFiles { get; }

    public MainMenuViewModel()
    {
        RecentFiles = new ObservableCollection<RecentFileInfo>();
    }
}

public readonly record struct RecentFileInfo(string ShortName, string FullPath, ImageSource Image);