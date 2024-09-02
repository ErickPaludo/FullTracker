using FullLocator.Models.Armazenamento;
using FullLocator.ViewModels;

namespace FullLocator.Views.Menu;

public partial class ViewMenu : FlyoutPage
{
	private readonly IDataService _sqliteService;
	public ViewMenu(IDataService sqliteService)
	{
		InitializeComponent();
		_sqliteService = sqliteService;
		BindingContext = new MenuVM(_sqliteService);
	}
}