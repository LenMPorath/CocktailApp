﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CocktailApp.Models;
using System.Text.Json;

namespace CocktailApp.BackendAPI
{
    public class AuthAPI
    {

        public static string ipAdress = GlobalVariables.ipAdress;

        private static HttpClient client = new HttpClient();

        
        public static async Task<string> GetSaltWithEMail(string email)
        {
            try
            {
                string link = ipAdress + "/api/Auth/GetSalt/" + email;
                HttpResponseMessage response = await client.GetAsync(link);

                if ( !response.IsSuccessStatusCode) { 
                    return null;
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Fehler bei der Anfrage: {e.Message}");
                return null;
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
                        Nutzername = null,
                        UserId = -1,
                        IsAdmin = false
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
                    Nutzername = null,
                    UserId = -1,
                    IsAdmin = false
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
