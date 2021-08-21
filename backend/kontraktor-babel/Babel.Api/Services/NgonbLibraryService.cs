using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Babel.Api.Services
{
    public class NgonbLibraryService
    {
         private const string baseUrl = "http://poisk.ngonb.ru/opacg.integration.smev/STORAGE/opacfindd/FindView/2.3.0";
        // private const string baseUrl = "http://poisk.ngonb.ru/opacg.integration.smev/ajax.php";

        public async Task<string> SearchById(string id)
        {
            var search = new SearchRusmarc();
            search.ID = id;
            search.RUSMARC = "1";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(search));


            var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync(baseUrl, 
                new StringContent(stringPayload,
                    Encoding.UTF8, "application/json"));

            

            var asString = await responseMessage.Content.ReadAsStringAsync();
            if (!asString.EndsWith("}"))
            {
                asString += "\"}]}";
                asString = asString.Remove(0, 1);
            }

            try
            {
                dynamic desirialized = Newtonsoft.Json.JsonConvert.DeserializeObject(asString);
                var unimarc = desirialized.result[0].UNIMARC;
                
                List<string> attributesArray = unimarc.ToObject<List<string>>();


                var attr = attributesArray.First(x => x.StartsWith("899"));
                var splitted = attr.Split('$');
                var position = splitted.First(x => x.StartsWith("b"));
                position = position.Replace("b", "").Trim();
                return position;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return "";
        }

        public async Task<string> SearchByAll(SearchRusmarc search)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(search));

            var httpClient = new HttpClient();

            var responseMessage = await httpClient.PostAsync(baseUrl,
                new StringContent(stringPayload,
                    Encoding.UTF8, "application/json"));

            var asString = await responseMessage.Content.ReadAsStringAsync();
            if (!asString.EndsWith("}"))
            {
                //asString += "\"}]}";
                //asString = asString.Remove(0, 1);
            }
            return asString;
        }
    }

    public class SearchRusmarc
    {
        [JsonPropertyName("iddb")]
        public string iddb { get; set; } = "";
        [JsonPropertyName("ID")]
        public string ID { get; set; } = "";
        [JsonPropertyName("AU")]
        public string AU { get; set; } = "";
        [JsonPropertyName("TI")]
        public string TI { get; set; } = "";
        [JsonPropertyName("PY")]
        public string PY { get; set; } = "";
        [JsonPropertyName("PU")]
        public string PU { get; set; } = "";
        [JsonPropertyName("PP")]
        public string PP { get; set; } = "";
        [JsonPropertyName("RUSMARC")]
        public string RUSMARC { get; set; } = "";
    }
}
