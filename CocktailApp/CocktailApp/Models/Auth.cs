using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public Order[] OrderList { get; set; }
        
    }
}
