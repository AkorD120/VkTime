using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xNet;
using Newtonsoft.Json;

namespace ViewWPF
{
    /// <summary>
    /// Логика работы с VKAPI.
    /// </summary>
    class VKAPI
    {
        /// <summary>
        /// //ID приложения.
        /// </summary>
        public const string APPID = "7111113";  
        private const string VKAPIURL = "https://api.vk.com/method/";  //Ссылка для запросов
        private string Token;  //Токен, использующийся при запросах

        public VKAPI(string AccessToken)
        {
            Token = AccessToken;
        }

        /// <summary>
        /// Получение заданной информации о пользователе с заданным ID.
        /// </summary>
        /// <param name="UserId"> Id пользователя. </param>
        /// <param name="Fields"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetInformation(string UserId, string[] Fields) 
        {
            HttpRequest GetInformation = new HttpRequest();
            GetInformation.AddUrlParam("user_ids", UserId);
            GetInformation.AddUrlParam("access_token", Token);
            GetInformation.AddUrlParam("version", "5.52");
            string Params = "";
            foreach (string i in Fields)
            {
                Params += i + ",";
            }
            Params = Params.Remove(Params.Length - 1);
            GetInformation.AddUrlParam("fields", Params);
            string Result = GetInformation.Get(VKAPIURL + "users.get").ToString();
            Result = Result.Substring(13, Result.Length - 15);
            Dictionary<string, string> Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict;
        }

        public string GetCityById(string CityId)  //Перевод ID города в название
        {
            HttpRequest GetCityById = new HttpRequest();
            GetCityById.AddUrlParam("city_ids", CityId);
            GetCityById.AddUrlParam("access_token", Token);
            GetCityById.AddUrlParam("version", "5.52");
            string Result = GetCityById.Get(VKAPIURL + "database.getCitiesById").ToString();
            Result = Result.Substring(13, Result.Length - 15);
            Dictionary<string, string> Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict["name"];
        }

        public string GetCountryById(string CountryId)  //Перевод ID страны в название
        {
            HttpRequest GetCountryById = new HttpRequest();
            GetCountryById.AddUrlParam("country_ids", CountryId);
            GetCountryById.AddUrlParam("access_token", Token);
            GetCountryById.AddUrlParam("version", "5.52");
            string Result = GetCountryById.Get(VKAPIURL + "database.getCountriesById").ToString();
            Result = Result.Substring(13, Result.Length - 15);
            Dictionary<string, string> Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(Result);
            return Dict["name"];
        }
    }
}
