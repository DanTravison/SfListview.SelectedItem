namespace SampleApp.ObjectModel;

/// <summary>
/// Provides a <see cref="Microsoft.Maui.Graphics.Color"/> for a <see cref="ColorPalette"/>.
/// </summary>
public sealed class PaletteColor : IEquatable<PaletteColor>
{
    #region Fields

    static uint _hashSeed;
    readonly int _hashCode = (int)Interlocked.Increment(ref _hashSeed);

    #endregion Fields

    /// <summary>
    /// Initializes a new instance of this class.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> for this instance.</param>
    public PaletteColor(Color color)
    {
        Color = color;
    }

    /// <summary>
    /// Gets the <see cref="Color"/>.
    /// </summary>
    public Color Color
    {
        get;
    }

    #region Equals

    /// <summary>
    /// Determines if the specified <paramref name="other"/> is equal to this instance.
    /// </summary>
    /// <param name="other">The <see cref="PaletteColor"/> to compare.</param>
    /// <returns>true if the specified <paramref name="other"/> is equal to this instance; 
    /// otherwise, false.</returns>
    public bool Equals(PaletteColor other)
    {
        return ReferenceEquals(this, other);
    }

    /// <summary>
    /// Determines if the specified object is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare.</param>
    /// <returns>
    /// true if <paramref name="obj"/> is equal to this instance; otherwise, false.
    /// </returns>
    public override bool Equals(object obj)
    {
        return ReferenceEquals(this, obj);
    }

    /// <summary>
    /// Determines if the specified <paramref name="color"/> is equal to this instance's <see cref="Color"/>.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> to compare.</param>
    /// <returns>
    /// true if <paramref name="color"/> is equal to this instance's <see cref="Color"/>; otherwise, false.
    /// </returns>
    public bool Equals(Color color)
    {
        return ColorComparer.Comparer.Equals(Color, color);
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>
    /// A hash code for the current object.
    /// </returns>
    public override int GetHashCode()
    {
        return _hashCode;
    }

    #endregion Equals

    /// <summary>
    /// Gets the string representation of the <see cref="Color"/>.
    /// </summary>
    /// <returns>The string representation of the <see cref="Color"/>.</returns>
    public override string ToString()
    {
        return Color.ToArgbHex();
    }
}
