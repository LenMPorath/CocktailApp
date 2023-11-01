using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public int AuthId { get; set; }
        public virtual Auth FavouritedByAuth { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe FavouritedRecipe { get; set; }
    }

    public class AFavourite
    {
        public int Id { get; set; }
        public int AuthId { get; set; }
        public int RecipeId { get; set; }
    }
}
