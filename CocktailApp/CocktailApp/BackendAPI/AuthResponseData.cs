using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.BackendAPI
{
    public class AuthResponseData
    {
        public string Token { get; set; }
        public string Nutzername { get; set; }
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
    }
}
