using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Infraestructure.Payment
{
    public class generalProcess<Request>
    {
        public async Task<HttpResponseMessage> generalApiProcess(Request dto, string url, string token)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            using (HttpClient httpClient = new HttpClient())
            {              
                try
                {
                    string[] sToken = token.Split(' ');
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(sToken[0], sToken[1]);
                }
                catch
                {
                    throw new Exception("La simulación exige un bearer token, cualquiera sirve ;)");
                }

                HttpResponseMessage message = await httpClient.PostAsync(url, content);

                return message;
            }
        }
    }
}
