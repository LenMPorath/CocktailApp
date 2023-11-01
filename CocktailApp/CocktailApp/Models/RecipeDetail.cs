using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class RecipeDetail
    {
        public int Id { get; set; }
        public float AmountInOz { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }

}
