using System.ComponentModel;

namespace SampleApp.ObjectModel;

/// <summary>
/// Notifies clients of property value changes and tracks has changes.
/// </summary>
public interface IObservableObject : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the value indicating if the object has changes.
    /// </summary>
    /// <value>true if the object has changes; otherwise, false.</value>
    bool HasChanges
    {
        get;
        set;
    }

    /// <summary>
    /// Sets <see cref="HasChanges"/> to false without raising a <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
    /// </summary>
    void ResetHasChanges();
}
