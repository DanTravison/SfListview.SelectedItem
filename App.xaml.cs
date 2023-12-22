namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Views.MainPage main = new();
            MainPage = new NavigationPage(main);
        }
    }
}
