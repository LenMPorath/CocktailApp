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
        public bool IsAdmin { get; set; }
        public ICollection<Order>? OrderList { get; set; }
    }
}
