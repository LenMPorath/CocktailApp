using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class Favourite
    {
        [Key]
        public int Id { get; set; }
        public int AuthId { get; set; }
        public virtual Auth FavouritedByAuth { get; set; } = null!;
        public int RecipeId { get; set; }
        public virtual Recipe FavouritedRecipe { get; set; } = null!;
    }

    public class AFavourite
    {
        public int Id { get; set; }
        public int AuthId { get; set; }
        public int RecipeId { get; set; }
    }
}
