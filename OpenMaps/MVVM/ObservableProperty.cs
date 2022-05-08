using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace OpenMaps.MVVM;

public class ObservableProperty<T> : INotifyPropertyChanged
{
    private T? b_value;
    public T? Value
    {
        get => b_value;
        set
        {
            if (b_value?.Equals(value) ?? value == null)
                return;

            b_value = value;
            OnPropertyChanged(nameof(Value));
            
            ValueChanged?.Invoke(this, value);
        }
    }

    public event EventHandler<T?>? ValueChanged;

    public ObservableProperty(T? value = default)
    {
        b_value = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}