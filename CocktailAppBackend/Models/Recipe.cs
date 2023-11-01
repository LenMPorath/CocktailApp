using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Directions { get; set; }
        public string? ImgPath { get; set; }
        public string? Source { get; set; }
        public float KcalInTotal { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<Tag>? Tags { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; } = null!;
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; } = null!;
        public virtual ICollection<Auth>? FavouritedByAuths { get; set; }
        public virtual ICollection<Favourite>? FavouritedBy { get; set; }
        public virtual ICollection<Auth>? RatedByAuths { get; set; }
        public virtual ICollection<Rating>? Ratings { get; set; }
    }

    public class ARecipe
    { 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Directions { get; set; }
        public string? ImgPath { get; set; }
        public string? Source { get; set; }
        public float KcalInTotal { get; set; }
        public bool IsAvailable { get; set; }
        public virtual ICollection<int>? Tags { get; set; }
        public virtual ICollection<int>? Orders { get; set; }
        public virtual ICollection<int> RecipeDetails { get; set; } = null!;
        public virtual ICollection<int>? FavouritedBy { get; set; }
        public virtual ICollection<int>? Ratings { get; set; }
    }
}
