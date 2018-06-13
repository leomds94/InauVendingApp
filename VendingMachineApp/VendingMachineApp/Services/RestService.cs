using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VendingMachineApp.Models;

namespace VendingMachineApp.Services
{
    public class RestService
    {
        HttpClient client;
        public static List<ProductMachine> Products { get; private set; }
        public string Url { get; set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<ProductMachine>> RefreshDataAsync()
        {
            var Url = "http://inauvendingserver.azurewebsites.net/api/machinesapi/0";
            var uri = new Uri(string.Format(Url, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductMachine>>(content);
            }
            return Products;
        }


        public async Task<bool> SendPendingCommand(ProductMachine boughtProd)
        {
            PendingCommand pendingProd = new PendingCommand { ProductMachineId = boughtProd.ProductMachineId, status = 0 };

            string content = JsonConvert.SerializeObject(pendingProd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = client.PostAsync("http://inauvendingserver.azurewebsites.net/api/PendingApp", stringContent).Result;
            var contentResPost = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                HttpResponseMessage getresponse;
                do
                {
                    getresponse = client.GetAsync("http://inauvendingserver.azurewebsites.net/api/PendingApp/productstatus").Result;
                } while (!getresponse.IsSuccessStatusCode);
                return true;
            }
            else
                return false;
        }
    }
}
