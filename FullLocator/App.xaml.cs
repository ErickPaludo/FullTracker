using FullLocator.Models.Armazenamento;
using FullLocator.Views.Menu;

namespace FullLocator
{
    public partial class App : Application
    {
        public App(IDataService dataservice)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ViewMenu(dataservice));
        }
    }
}
