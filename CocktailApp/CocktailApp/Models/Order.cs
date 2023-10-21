﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public int RecipeId { get; set; } // Fremdschlüsselbeziehung zu Recipe.Id
        public int CreatedByUserId { get; set; } // Fremdschlüsselbeziehung zu Auth.Id
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
