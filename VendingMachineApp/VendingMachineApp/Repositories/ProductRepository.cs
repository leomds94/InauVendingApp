//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;

//namespace VendingMachineApp.Repositories
//{
//    class ProductRepository
//    {
//        private async Task<JsonValue> FetchWeatherAsync(string url)
//        {
//            // Create an HTTP web request using the URL:
//            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
//            request.ContentType = "application/json";
//            request.Method = "GET";

//            // Send the request to the server and wait for the response:
//            using (WebResponse response = await request.GetResponseAsync())
//            {
//                // Get a stream representation of the HTTP web response:
//                using (Stream stream = response.GetResponseStream())
//                {
//                    // Use this stream to build a JSON document object:
//                    JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
//                    //Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

//                    // Return the JSON document:
//                    return jsonDoc;
//                }
//            }
//        }
//    }
//}
