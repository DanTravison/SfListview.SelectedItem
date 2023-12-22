using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SampleApp.ObjectModel;

/// <summary>
/// Provides an abstract base class for classes supporting INotifyPropertyChanged
/// </summary>
public abstract class ObservableObject : IObservableObject
{
    #region Fields

    bool _hasChanges;
    readonly WeakEventManager _propertyChanged = new();

    #endregion Fields

    #region Constructors

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    protected ObservableObject()
        : this(true)
    {
    }

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="supportsHasChanges">true if the object supports <see cref="HasChanges"/>; otherwise, false.</param>
    protected ObservableObject(bool supportsHasChanges)
    {
        SupportsHasChanges = supportsHasChanges;
    }

    #endregion Constructors

    #region HasChanges

    /// <summary>
    /// Gets or sets the value indicating if the object has changes.
    /// </summary>
    /// <value>true if the object has changes; otherwise, false.</value>
    /// <remarks>
    /// The derived class or external callers use this property to indicate 
    /// this instance needs to be saved to an underlying store.
    /// The property is not updated by the base class. 
    /// </remarks>
    [JsonIgnore]
    public virtual bool HasChanges
    {
        get => _hasChanges;
        set
        {
            if (SupportsHasChanges && value != _hasChanges)
            {
                _hasChanges = value;
                if (value == false)
                {
                    ResetHasChanges();
                }
                OnPropertyChanged(HasChangesChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Sets <see cref="HasChanges"/> to false without raising a <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <remarks>
    /// This method is generally called when this instance is reset to its default state or 
    /// after this instance is saved or restored.
    /// </remarks>
    public void ResetHasChanges()
    {
        if (SupportsHasChanges)
        {
            _hasChanges = false;
            OnResetHasChanges();
        }
    }

    /// <summary>
    /// Overridden in the derived class to perform additional work when <see cref="ResetHasChanges"/>
    /// is called.
    /// </summary>
    protected virtual void OnResetHasChanges()
    {
        // do nothing.
    }

    /// <summary>
    /// Gets the value indicating if <see cref="HasChanges"/> is supported.
    /// </summary>
    /// <remarks>
    /// If false, setting <see cref="HasChanges"/> does nothing and <see cref="PropertyChanged"/> event 
    /// is not raised for <see cref="HasChanges"/>.
    /// </remarks>
    public bool SupportsHasChanges
    {
        get;
    }

    #endregion HasChanges

    #region INotifyPropertyChanged

    /// <summary>
    /// Occurs when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Raises the <see cref="PropertyChanged"/> event with a cached <see cref="PropertyChangedEventArgs"/>.
    /// </summary>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> for the event.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e);
        PropertyChanged?.Invoke(this, e);
    }

    #endregion INotifyPropertyChanged

    #region SetProperty

    /// <summary>
    /// Provides a <see cref="INotifyPropertyChanged"/> event with a cached <see cref="PropertyChangedEventArgs"/>.
    /// </summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change occurred.</param>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> identifying the event.</param>
    /// <returns>true if the property was set; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="e"/> is a null reference.</exception>
    protected bool SetProperty<T>(ref T field, T newValue, PropertyChangedEventArgs e)
    {
        return SetProperty(ref field, newValue, null, e);
    }

    /// <summary>
    /// Provides a <see cref="INotifyPropertyChanged"/> event with a cached <see cref="PropertyChangedEventArgs"/>.
    /// </summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change occurred.</param>
    /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> instance to use to compare the input values.</param>
    /// <param name="e">The <see cref="PropertyChangedEventArgs"/> identifying the event.</param>
    /// <returns>true if the property was set; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="e"/> is a null reference.</exception>
    protected bool SetProperty<T>(ref T field, T newValue, IEqualityComparer<T> comparer, PropertyChangedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(e, nameof(e));
        bool areEqual = false;
        if (comparer != null)
        {
            areEqual = comparer.Equals(field, newValue);
        }
        else if (field != null)
        {
            areEqual = field.Equals(newValue);
        }
        else if (newValue == null)
        {
            areEqual = true;
        }
        if (areEqual)
        {
            return false;
        }

        field = newValue;
        OnPropertyChanged(e);
        HasChanges = true;
        return true;
    }

    #endregion SetProperty

    #region Cached PropertyChangedEventArgs

    /// <summary>
    /// Defines the <see cref="PropertyChangedEventArgs"/> passed to <see cref="INotifyPropertyChanged.PropertyChanged"/> when <see cref="HasChanges"/> is changed.
    /// </summary>
    static public readonly PropertyChangedEventArgs HasChangesChangedEventArgs = new(nameof(HasChanges));

    #endregion Cached PropertyChangedEventArgs

}
