using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Kcal { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<RecipeDetail> RecipeDetails { get; set; }
    }
}
