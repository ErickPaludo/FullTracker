using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using FullLocator;
using FullLocator.Models;
using FullLocator.Models.Armazenamento;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json;

namespace ProjetoMuai
{
    public partial class HttpPost : ObservableObject
    {
        private static string https = string.Empty;

        [ObservableProperty]
        private List<DataConfig> _data;

        public HttpPost(string https_)
        {
            https = https_;
        }

        public async Task Post(Carga info)
        {

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage();

            var obj = new { carga = info.Ncarga, longitude = info.Longitude.Replace(",", "."), latitude = info.Latitude.Replace(",", ".") };

            var content = ToRequest(obj);

            var response = await httpClient.PostAsync(https, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("");
            }

        }
        private static StringContent ToRequest(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "aplication/json");
            return data;
        }
    }
}
