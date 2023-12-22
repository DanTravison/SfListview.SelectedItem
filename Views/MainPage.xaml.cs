using SampleApp.ObjectModel;

namespace SampleApp.Views;

public partial class MainPage : ContentPage
{
    MainViewModel _model;
    public MainPage()
    {
        BindingContext = _model = new MainViewModel();
        InitializeComponent();
    }

    private void SfListView_SelectionChanged(object sender, Syncfusion.Maui.ListView.ItemSelectionChangedEventArgs e)
    {
        _model.Selection = e.AddedItems[0] as PaletteColor;
        _model.SelectedItem = Colors.SelectedItem as PaletteColor;
    }
}
