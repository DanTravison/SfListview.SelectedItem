using System.Collections;
using System.ComponentModel;

namespace SampleApp.ObjectModel;

public class ColorPalette : ObservableObject, IEnumerable<PaletteColor>
{
    /// <summary>
    /// Provides a the default colors for the <see cref="ColorPalette"/>.
    /// </summary>
    static public readonly Color[] DefaultColors =
    [
        Colors.Black,
        Colors.White,
        Colors.Red,
        Colors.Green,
        Colors.Blue,
        Colors.Yellow,
        Colors.Violet,
        Colors.Magenta,
        Colors.DarkSlateGray,
        Colors.LightSlateGray,
        Colors.DarkTurquoise,
        Colors.DodgerBlue,
        Colors.PaleGoldenrod
    ];

    PaletteColor _selectedItem;
    List<PaletteColor> _colors = new();

    #region Constructors

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    public ColorPalette()
        : this(DefaultColors)
    {
    }

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="colors">An <see cref="IEnumerable{Color}"/> to use to populate the collection.</param>
    public ColorPalette(IEnumerable<Color> colors)
    {
        foreach (Color color in colors)
        {
            _colors.Add(new PaletteColor(color));
        }
        if (_colors.Count > 0)
        {
            _selectedItem = _colors[0];
        }
    }

    #endregion Constructors

    #region Properties

    /// <summary>
    /// Gets or sets the selected <see cref="Color"/>.
    /// </summary>
    public PaletteColor SelectedItem
    {
        get => _selectedItem;
        set
        {
            // NOTE: Since a ColorPalette can have multiple PaletteColor entries with the same color value,
            // comparison uses the object reference, not the PaletteColor.Color value.
            if (!object.ReferenceEquals(value, _selectedItem))
            {
                _selectedItem = value;
                OnPropertyChanged(SelectedItemChangedEventArgs);
            }
        }
    }

    /// <summary>
    /// Gets the <see cref="PaletteColor"/> at the specified <paramref name="index"/>.
    /// </summary>
    /// <param name="index">The zero-based index of the <see cref="PaletteColor"/> to get.</param>
    /// <returns>The <see cref="PaletteColor"/> at the specified <paramref name="index"/>.</returns>
    public PaletteColor this[int index]
    {
        get => _colors[index];
    }

    #endregion Properties

    /// <summary>
    /// Provides <see cref="PropertyChangedEventArgs"/> passed to the <see cref="ObservableObject.PropertyChanged"/> event when <see cref="SelectedItem"/> changes.
    /// </summary>
    static public readonly PropertyChangedEventArgs SelectedItemChangedEventArgs = new PropertyChangedEventArgs(nameof(SelectedItem));

    public IEnumerator<PaletteColor> GetEnumerator()
    {
        return _colors.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _colors.GetEnumerator();
    }
}
