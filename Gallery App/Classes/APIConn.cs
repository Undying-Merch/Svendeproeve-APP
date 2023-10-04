using Microsoft.Maui.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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

        public int getUserId(string userName)
        {
            string json = client.GetStringAsync(url + "BrugerListe/").Result;
            string[] bList = jsonSplit(json);
            for (int i = 0; i < bList.Length; i++)
            {
                Bruger bruger = System.Text.Json.JsonSerializer.Deserialize<Bruger>(bList[i]);
                if (bruger.brugernavn == userName)
                {
                    return bruger.id;
                }
            }
            return 0;

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


        public int uploadGeoLocation(Geo_Location geoLocation)
        {
            var sJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(geoLocation), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "GeoCreate/", sJson);
            int returnId = int.Parse(response.Result.Content.ReadAsStringAsync().Result);
            return returnId;
        }

        public bool uploadBillede(Billede billede)
        {
            
            using (var multupartFomrContent = new MultipartFormDataContent())
            {
                multupartFomrContent.Add(new StringContent(billede.titel), name: "titel");
                multupartFomrContent.Add(new StringContent(billede.beskrivelse), name: "beskrivelse");
                multupartFomrContent.Add(new StringContent(billede.geo_id.ToString()), name: "geo_id");
                multupartFomrContent.Add(new StringContent(billede.kategori_id.ToString()), name: "kategori_id");
                multupartFomrContent.Add(new StringContent(billede.upload_id.ToString()), name: "upload_id ");

                var fileStreamContent = new StreamContent(File.OpenRead(billede.billedet.FullPath));
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                multupartFomrContent.Add(fileStreamContent, name: "billedet", fileName: $"{billede.titel}.jpeg");

                var response = client.PostAsync(url + "BillederCreate/", multupartFomrContent);
                string test = response.Result.Content.ReadAsStringAsync().Result;
                return response.Result.IsSuccessStatusCode;

            }
        }

        public List<Gallery_Class> getAllBilleder()
        {
            string json = client.GetStringAsync(url + "BillederListe/").Result;
            string[] bList = jsonSplit(json);
            List<Gallery_Class> billeder = new List<Gallery_Class>();
            for (int i = 0; i < bList.Length; i++)
            {
                Gallery_Class billede = System.Text.Json.JsonSerializer.Deserialize<Gallery_Class>(bList[i]);
                billeder.Add(billede);
            }
            return billeder;
        }

        public Geo_Location getLocation(int id)
        {
            string json = client.GetStringAsync($"{url}Geo/{id}/").Result;
            string[] oneList = jsonSplit(json);
            Geo_Location location = System.Text.Json.JsonSerializer.Deserialize<Geo_Location>(oneList[0]);
            return location;
        }

        public bool subscribeTo(Subscribe_Class subscribe_class)
        {
            var sJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(subscribe_class), Encoding.UTF8, "application/json");
            var response = client.PostAsync(url + "SubscribedCreate/", sJson);
            return response.Result.IsSuccessStatusCode;
        }

        public List<Subscribe_Class> getSubscribedItems(int id)
        {
            string json = client.GetStringAsync(url + "SubscribedListe/").Result;
            string[] subList = jsonSplit(json);
            List<Subscribe_Class> subs = new List<Subscribe_Class>();
            for (int i = 0; i < subList.Length; i++)
            {
                Subscribe_Class sub = System.Text.Json.JsonSerializer.Deserialize<Subscribe_Class>(subList[i]);
                if (sub.bruger_id == id)
                {
                    subs.Add(sub);
                }
            }
            return subs;
        }
    }
}
