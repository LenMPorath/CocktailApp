using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
        public int AmountInOz { get; set; }
    }

}
