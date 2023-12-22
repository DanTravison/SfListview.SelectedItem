namespace SampleApp.ObjectModel;

using Syncfusion.Maui.ListView;
using System.ComponentModel;

internal class MainViewModel : ObservableObject
{
    PaletteColor _selection;
    PaletteColor _selectedItem;
    public MainViewModel()
    {
        Palette =  new();
        Reset = new(OnReset);
    }

    /// <summary>
    /// Gets the <see cref="ColorPalette"/> to display in the <see cref="SfListView"/>
    /// </summary>
    public ColorPalette Palette
    {
        get;
    }

    /// <summary>
    /// Gets or sets the <see cref="PaletteColor"/> selected in the <see cref="SfListView"/>.
    /// </summary>
    /// <remarks>
    /// This property is used to capture the Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs.AddedItems[0] value
    ///  when SfListView.SelectionChanged is raised.
    /// See MainPage.SfListView_SelectionChanged
    /// </remarks>
    public PaletteColor Selection
    {
        get => _selection;
        set => SetProperty(ref _selection, value, ReferenceEqualityComparer.Instance, SelectionChangedEventArgs);
    }

    /// <summary>
    /// Gets or sets the <see cref="PaletteColor"/> selected in the <see cref="SfListView"/>.
    /// </summary>
    /// <remarks>
    /// This property is used to capture the SfListView.SelectedItem value when SfListView.SelectionChanged is raised.
    /// See MainPage.SfListView_SelectionChanged
    /// </remarks>
    public PaletteColor SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value, ReferenceEqualityComparer.Instance, SelectionChangedEventArgs);
    }

    public Command Reset
    {
        get;
    }

    void OnReset(object parameter)
    {
        Palette.SelectedItem = Palette[0];
    }

    static readonly PropertyChangedEventArgs SelectionChangedEventArgs = new(nameof(Selection));

    static readonly PropertyChangedEventArgs SelectedItemChangedEventArgs = new(nameof(SelectedItem));
}
