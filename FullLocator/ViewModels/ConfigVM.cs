using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FullLocator.Models;
using FullLocator.Models.Armazenamento;
using FullLocator.Views;
using FullLocator.Views.Menu;
using Microsoft.Maui.Animations;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FullLocator.ViewModels
{
    public partial class ConfigVM : ObservableObject
    {
        private readonly IDataService _dataService;
        [ObservableProperty]
        private List<DataConfig> _data;

        [ObservableProperty]
        private DataConfig dados;
        public ObservableCollection<DataConfig> Info;

        private readonly INavigation _navigation;

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand DisplayCommand { get; }



        [RelayCommand]
        private async void Salvar()
        {
            if (!string.IsNullOrEmpty(Dados.time.ToString()) && !string.IsNullOrEmpty(Dados.http_api) && !string.IsNullOrEmpty(Dados.location_precision) && !string.IsNullOrEmpty(Dados.placa.ToString()))
            {
                if (Dados.time > 0)
                {
                    if (Dados.placa.ToString().Length == 7)
                    {
                        if (Data.Count >= 1)
                        {
                            UpdateCommand.Execute(null);
                        }
                        else
                        {
                            AddCommand.Execute(null);
                        }
                        await App.Current.MainPage.DisplayAlert("Sucesso!", "Configuração salva com sucesso!", "OK");
                        NavigationPage navpage = (NavigationPage)App.Current.MainPage;
                        await navpage.PopToRootAsync();
                        await navpage.PushAsync(new ViewMenu(_dataService));
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Erro", "Placa incompleta!", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "O tempo mínimo deve ser de 1 minuto!", "OK");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Existem campos nulos", "OK");
            }
        }


        public ConfigVM()
        {

        }

        public ConfigVM(IDataService service )
        {
            Dados = new DataConfig();
            Info = new ObservableCollection<DataConfig>();
            AddCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await service.AddData(Dados);
                await Refresh(service);
            });

            UpdateCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await service.UpdateData(Dados);
                await Refresh(service);
            });

            DeleteCommand = new Command(async () =>
            {
                await service.InitilizeAsync();

                var resposta = await App.Current.MainPage.DisplayAlert("Alerta", "Confirma exclusão ?", "Sim", "Não");
                if (resposta)
                    await service.DeleteData(Dados);

                await Refresh(service);
            });

            DisplayCommand = new Command(async () =>
            {
                await service.InitilizeAsync();
                await Refresh(service);

                if (Data.Count == 0)
                {
                    Dados = new DataConfig
                    {
                        location_precision = "Média",
                        time = 1
                    };
                }
                else
                {
                    var obj = Data[0];
                    Dados = new DataConfig
                    {
                        id = obj.id,
                        location_precision = obj.location_precision,
                        http_api = obj.http_api,
                        time = obj.time,
                        placa = obj.placa
                    };
                }
                
            });

            DisplayCommand.Execute(null);
        }

        private async Task Refresh(IDataService service)
        {
            Data = await service.GetDataConfigAsync();
        }
    }
}
