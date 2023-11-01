using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Kcal { get; set; }
        public string ImgPath { get; set; }
        public bool InStorage { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
        public virtual ICollection<RecipeDetail> RecipeDetails { get; set; }
    }

    public class AIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Kcal { get; set; }
        public string ImgPath { get; set; }
        public bool InStorage { get; set; }
        public ICollection<int> Recipes { get; set; }
    }
}
