﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CocktailApp.Models;

namespace CocktailApp.BackendAPI
{
    public class AuthAPI
    {

        public static string ipAdress = "http://10.0.2.2:5101";

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

        public static async Task<bool> VerifyPassword(string email, string password)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(ipAdress + $"/api/Auth/passwordVerify/{email}/{password}");

                response.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Fehler bei der Anfrage: {e.Message}");
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

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(ipAdress + "/api/Auth", content);

            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
    }
}