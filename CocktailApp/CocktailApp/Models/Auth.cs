﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string EMail { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Recipe> RatedRecipes { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<Favourite> Favourites { get; set; }
        public virtual ICollection<Recipe> FavouritedRecipes { get; set; }
    }

    public class AAuth
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string EMail { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<int> Ratings { get; set; }
        public virtual ICollection<int> OrderList { get; set; }
        public virtual ICollection<int> Favourites { get; set; }
    }

    public class AAuthRequestModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string EMail { get; set; }
        public bool IsAdmin { get; set; }
    }
}
