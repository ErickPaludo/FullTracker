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

namespace FullLocator.ViewModels
{
    public partial class HomeVM : ObservableObject
    {
        CancellationTokenSource cancellationTokenSource;

        [ObservableProperty]
        private Carga carga;

        [ObservableProperty]
        private Color btnstart = Colors.Orange;

        [ObservableProperty]
        private string btntext = "Iniciar";

        [ObservableProperty]
        private bool entryenable = true;

        [ObservableProperty]
        private bool borderenabled = false;

        private static bool active = false;

        [RelayCommand]
        private void ChangedBtnColor()
        {
            if (!string.IsNullOrEmpty(Carga.Ncarga))
            {
              Carga.Ncarga = Carga.Ncarga.Replace("-", "").Replace(",", "").Replace(".", "").Replace(" ", ""); //Solução temporária
                LocalLizacao();
                if (active)
                {
                    Btnstart = Colors.Orange;
                    Btntext = "Iniciar";
                    Entryenable = true;
                    Borderenabled = false;
                }
                else
                {
                    Btnstart = Colors.Gray;
                    Btntext = "Encerrar";
                    Entryenable = false;
                    Borderenabled = true;
                }
                active = !active;
            }
        }

        public HomeVM()
        {
          Carga = new Carga();
        }

        private void LocalLizacao()
        {
            if (cancellationTokenSource != null)
            {
                // Cancela a tarefa se estiver ativa
                cancellationTokenSource.Cancel();
                cancellationTokenSource = null;
            }
            else
            {
                // Inicia a tarefa de localização
                cancellationTokenSource = new CancellationTokenSource();
                _ = GetLocal(cancellationTokenSource.Token); // Inicia a tarefa de forma assíncrona
            }
        }
        private async Task GetLocal(CancellationToken token)
        {
            HttpPost http = new HttpPost();
            try
            {
                while (!token.IsCancellationRequested)
                {
                    GeolocationAccuracy accuracy = GeolocationAccuracy.Medium;

                    //switch (config.nivel)
                    //{
                    //    case 0:
                    //        accuracy = GeolocationAccuracy.Low;
                    //        break;
                    //    case 1:
                    //        accuracy = GeolocationAccuracy.Medium;
                    //        break;
                    //    case 2:
                    //        accuracy = GeolocationAccuracy.High;
                    //        break;
                    //}

                    var request = new GeolocationRequest
                    {
                        DesiredAccuracy = accuracy
                    };

                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        Carga.Latitude = location.Latitude.ToString();
                        Carga.Longitude = location.Longitude.ToString();
                        await http.Post(new Carga(Carga.Ncarga.ToString(), location.Latitude.ToString(), location.Longitude.ToString()));
                    }
                
                    //  await Task.Delay(config.time, token); // Passa o token para Task.Delay para que ele seja cancelado corretamente
                    await Task.Delay(1000, token);
                }
            }
            catch
            {
            }
        }
    }
}
