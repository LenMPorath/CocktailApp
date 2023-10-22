using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class Auth
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Order>? OrderList { get; set; }
        public ICollection<Recipe>? Favourites { get; set; }
    }
}
