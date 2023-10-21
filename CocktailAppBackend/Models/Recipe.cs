﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tag>? Tags { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
