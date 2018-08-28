using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Zwaby.Interfaces;
using System;
using System.Diagnostics;
using Zwaby.Models;
using System.Text;

namespace Zwaby.Services
{
    public class APIRepository: IAPIRepository
    {
        const string Url = "https://yourapiname.azurewebsites.net/api/payment";
        //private string authorizationKey;

        public async Task<string> ChargeCard(string token, int amount)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            response = await client.PostAsync(Url,
                                              new StringContent(JsonConvert.SerializeObject(new PaymentModel()
                                              {
                                                  Email = "zwabyapp@gmail.com",
                                                  Token = token,
                                                  Amount = amount
                                              }),
                                              Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsStringAsync();
        }
    }
}
