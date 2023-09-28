using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gallery_App.Classes
{
    class APIConn
    {
        private string url = "http://10.130.54.78:8000/data/";
        private string token = "86839eb173ab3783f76b3a431e6c1a3ce9f47029";
        HttpClient client;

        public APIConn()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
        }

        public bool passwordCheck(Bruger bruger)
        {
            var sJson = new StringContent(JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "PC/", sJson);
            bool boolResp = Boolean.Parse(response.Result.Content.ReadAsStringAsync().Result);
            return boolResp;
        }

        public bool exsistingUser(Bruger bruger)
        {
            var sJson = new StringContent(JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "BC/", sJson);
            bool boolResp = Boolean.Parse(response.Result.Content.ReadAsStringAsync().Result);
            return boolResp;
        }

        public bool createUser(Bruger bruger)
        {
            var sJson = new StringContent(JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "BrugerCreate/", sJson);
            return response.Result.IsSuccessStatusCode;
        }

    }
}
