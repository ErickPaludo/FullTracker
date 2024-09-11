using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FullLocator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using ProjetoMuai;
using FullLocator.Models.Armazenamento;
using static System.Net.WebRequestMethods;
using FullLocator.Views;

namespace FullLocator.ViewModels
{
    public partial class HomeVM : ObservableObject
    {
        CancellationTokenSource cancellationTokenSource;

        private readonly INavigation _navigation;

        private static IDataService _service;

        private static int time = 0;
        private static string precisao = string.Empty;

        [ObservableProperty]
        private List<DataConfig> _data;

        [ObservableProperty]
        private bool loading = false;

        private static HttpPost http;

        [ObservableProperty]
        private Carga carga;

        [ObservableProperty]
        private string placa;

        [ObservableProperty]
        private string btntext = "Iniciar";

        [ObservableProperty]
        private bool entryenable = true;

        [ObservableProperty]
        private bool buttonenabled = true;

        [ObservableProperty]
        private bool borderenabled = false;

        [RelayCommand]
        private async void ChangedBtnColor()
        {
            if (!string.IsNullOrEmpty(Placa))
            {
                DisplayCommand(_service);
                Loading = true;
                Buttonenabled = false;
                await Task.Delay(2000);
                LocalLizacao();
                Loading = false;
                Buttonenabled = true;
            }
        }


        public HomeVM()
        {
            DisplayCommand(_service);
        }
        public HomeVM(IDataService service)
        {
            _service = service;
        }

        private void LocalLizacao()
        {
            Carga = new Carga();
            if (cancellationTokenSource != null)
            {
                // Cancela a tarefa se estiver ativa
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
                Btntext = "Iniciar";
                Entryenable = true;
                Borderenabled = false;
                MenuVM.exect = false;
            }
            else
            {
                // Inicia a tarefa de localização
                cancellationTokenSource = new CancellationTokenSource();
                _ = GetLocal(cancellationTokenSource.Token); // Inicia a tarefa de forma assíncrona
                Btntext = "Encerrar";
                Entryenable = false;
                Borderenabled = true;
                MenuVM.exect = true;
            }
        }

        private async Task DisplayCommand(IDataService service)
        {
            await service.InitilizeAsync();
            await Refresh(service);
            if (Data.Count > 0)
            {
                var obj = Data[0];
                time = obj.time * 60000;
                precisao = obj.location_precision;
                http = new HttpPost(obj.http_api);
                Placa = obj.placa.ToString();
            }

            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Http da API não está preenchido!", "OK");

                NavigationPage navpage = (NavigationPage)App.Current.MainPage;
                navpage.PushAsync(new ViewConfig(_service));
            }

        }

        private async Task Refresh(IDataService service)
        {
            Data = await service.GetDataConfigAsync();
        }

        private async Task GetLocal(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    GeolocationAccuracy accuracy = GeolocationAccuracy.Medium;

                    switch (precisao)
                    {
                        case "Baixa":
                            accuracy = GeolocationAccuracy.Low;
                            break;
                        case "Média":
                            accuracy = GeolocationAccuracy.Medium;
                            break;
                        case "Alta":
                            accuracy = GeolocationAccuracy.High;
                            break;
                    }

                    var request = new GeolocationRequest
                    {
                        DesiredAccuracy = accuracy
                    };

                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Carga.Latitude = location.Latitude.ToString();
                        Carga.Longitude = location.Longitude.ToString();
                        await http.Post(Placa,new Carga(location.Latitude.ToString(), location.Longitude.ToString()));
                    }

                    await Task.Delay(time, token);
                }
            }
            catch (Exception ex)
            {
                if (!token.IsCancellationRequested)
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Link da API inválido!", "OK");

                    LocalLizacao();

                    NavigationPage navpage = (NavigationPage)App.Current.MainPage;
                    navpage.PushAsync(new ViewConfig(_service));
                }
               
            }
        }
    }
}
