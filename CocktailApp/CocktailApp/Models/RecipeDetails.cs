using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class OrderDetails
    {
        public int RecipeId { get; set; } // Fremdschlüsselbeziehung zu Recipe.Id
        public int IngredientId { get; set; } // Fremdschlüsselbeziehung zu Ingredients.Id
        public int AmountInOz { get; set; }

    }
}
