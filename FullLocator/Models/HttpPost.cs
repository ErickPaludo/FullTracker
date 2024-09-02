using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FullLocator.Models;
using Newtonsoft.Json;

namespace ProjetoMuai
{
    class HttpPost
    {
        public HttpPost()
        {
           
        }
        public async Task Post(Carga info)
        {
            try
            {
                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();

                var obj = new { carga = info.Ncarga, longitude = info.Longitude.Replace(",","."), latitude = info.Latitude.Replace(",", ".") };

                var content = ToRequest(obj);

                var response = await httpClient.PostAsync("http://172.25.100.14:8787/api/cargas", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro na solicitação: {response.StatusCode}");
                }
            }
            catch (Exception ex) 
            { 
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
