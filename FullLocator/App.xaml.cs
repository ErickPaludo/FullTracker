using FullLocator.Views.Menu;

namespace FullLocator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ViewMenu();
        }
    }
}
