using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CocktailApp.Models;
using System.Text.Json;
using Xamarin.Essentials;
using System.Net.Http.Headers;

namespace CocktailApp.BackendAPI
{
    public class IngredientAPI
    {

        public static string ipAdress = GlobalVariables.ipAdress;

        private static HttpClient client = new HttpClient();

        
        public static async Task AddIngredient(string name, float kcal, bool inStorage)
        {
            try
            {
                var data = new
                {
                    Name = name,
                    Kcal = kcal,
                    InStorage = inStorage,
                    Token = await SecureStorage.GetAsync("auth_token")
                };

                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.GetAsync(ipAdress + "/api/Ingredient" + content);

                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Fehler bei der Anfrage: {e.Message}");
                throw;
            }
        }

        public static async Task<AuthResponseData> VerifyPassword(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(ipAdress + $"/api/Auth/passwordVerify/{email}/{password}");

                if (!response.IsSuccessStatusCode)
                {
                    return new AuthResponseData()
                    {
                        Token = null,
                        IsAdmin = false,
                    };
                }

                string responseString = await response.Content.ReadAsStringAsync();
                AuthResponseData responseData = JsonConvert.DeserializeObject<AuthResponseData>(responseString);
                return responseData;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Fehler bei der Anfrage: {e.Message}");
                return new AuthResponseData()
                {
                    Token = null,
                    IsAdmin = false,
                };
                throw;
            }
        }
        public static async Task CreateAuth(string username, string password, string salt, string email)
        {
            var data = new AAuthRequestModel {
                Username = username,
                Password = password,
                Salt = salt,
                EMail = email,
                IsAdmin = false,
            };

            var json = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ipAdress + "/api/Auth", content);

            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
    }
}
