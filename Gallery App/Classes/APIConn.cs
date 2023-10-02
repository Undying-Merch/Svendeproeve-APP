using Microsoft.Maui.Graphics;
using Newtonsoft.Json;
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
            var sJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "PC/", sJson);
            bool boolResp = Boolean.Parse(response.Result.Content.ReadAsStringAsync().Result);
            return boolResp;
        }

        public bool exsistingUser(Bruger bruger)
        {
            var sJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "BC/", sJson);
            bool boolResp = Boolean.Parse(response.Result.Content.ReadAsStringAsync().Result);
            return boolResp;
        }

        public bool createUser(Bruger bruger)
        {
            var sJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(bruger), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "BrugerCreate/", sJson);
            return response.Result.IsSuccessStatusCode;
        }

        public List<Kategori> getCategories()
        {
            List<Kategori> categories = new List<Kategori>();
            string json = client.GetStringAsync(url + "KategoriListe/").Result;
            string[] categoryStrings = jsonSplit(json);
            for (int i = 0; i < categoryStrings.Length; i++)
            {
                Kategori kategori = JsonConvert.DeserializeObject<Kategori>(categoryStrings[i]);
                categories.Add(kategori);
            }
            return categories;

        }

        private String[] jsonSplit(string json)
        {
            char split = '}';
            String[] splittedString = json.Split(split);
            splittedString = splittedString.SkipLast(1).ToArray();
            for (int i = 0; i < splittedString.Length; i++)
            {
                splittedString[i] = splittedString[i].TrimStart(',', '[');
                splittedString[i] = splittedString[i].TrimEnd(']');
                splittedString[i] = splittedString[i] + "}";
            }
            return splittedString;
        }

    }
}
