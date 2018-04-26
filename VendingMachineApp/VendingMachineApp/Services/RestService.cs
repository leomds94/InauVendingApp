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
            Url = "http://vendinginautec.azurewebsites.net/api/PendingApp";
            PendingCommand pendingProd = new PendingCommand { ProductMachineId = boughtProd.ProductMachineId, status = 0 };
            var uri = new Uri(string.Format(Url, string.Empty));
            string content = JsonConvert.SerializeObject(pendingProd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = client.PostAsync(Url, stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                HttpResponseMessage getresponse;
                string contentRes;
                do
                {
                    getresponse = client.GetAsync("http://vendinginautec.azurewebsites.net/api/GetPendingProducts/productstatus").Result;
                    contentRes = await getresponse.Content.ReadAsStringAsync();
                } while (contentRes == "false");
                if(contentRes == "true")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
