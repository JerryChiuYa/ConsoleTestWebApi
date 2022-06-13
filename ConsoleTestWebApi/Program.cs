using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;

namespace ConsoleTestWebApi
{
    internal class Program
    {
        static HttpClient client=new HttpClient();
        static string url = "https://localhost:44357/";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            await CreateStockAsync();

            var result = await GetStocksAsync();

            foreach (var item in result)
            {
                Console.WriteLine($"Id:{item.Id}, Name:{item.Name}, UnitPrice:{item.UnitPrice} ");
            }
        }

        static async Task<List<Stock>> GetStocksAsync()
        {
            var stocks=new List<Stock>();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJqZXJyeSIsImp0aSI6IjkxN2E0NWI3LTYyZTUtNDEzOC04M2VmLTQ5NGNjZTJmM2FmOCIsIkRlcHQiOiJFbmdpbmVlciIsIlJvbGUiOiJNYW5hZ2VyIiwibmJmIjoxNjU1MTA4NTU3LCJleHAiOjE2NTUxMDk0NTcsImlhdCI6MTY1NTEwODU1NywiaXNzIjoiSmxpekJhbmsiLCJhdWQiOiJKbGl6QmFuayJ9.CKrv8LatL2taMewVoVGBdYlBdXZeMekYCTOE9-5nkFU");
            var response = await client.GetAsync(url+ "api/Stock/GetStocks");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                stocks=JsonConvert.DeserializeObject<List<Stock>>(result);
            }
            else
            {
                Console.WriteLine(response.StatusCode);
                
            }
            return stocks;
        }

        static async Task CreateStockAsync()
        {
            var stock = new Stock
            {
                Id = 1234, Name ="OMG", UnitPrice =777M
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJqZXJyeSIsImp0aSI6IjkxN2E0NWI3LTYyZTUtNDEzOC04M2VmLTQ5NGNjZTJmM2FmOCIsIkRlcHQiOiJFbmdpbmVlciIsIlJvbGUiOiJNYW5hZ2VyIiwibmJmIjoxNjU1MTA4NTU3LCJleHAiOjE2NTUxMDk0NTcsImlhdCI6MTY1NTEwODU1NywiaXNzIjoiSmxpekJhbmsiLCJhdWQiOiJKbGl6QmFuayJ9.CKrv8LatL2taMewVoVGBdYlBdXZeMekYCTOE9-5nkFU");

            using (var content=new StringContent(JsonConvert.SerializeObject(stock), Encoding.UTF8, "application/json"))
            {
                var response=await client.PostAsync(url+"api/Stock", content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode+"---Create ok!");
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }

           
        }

        }
}
