using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public int Grade { get; set; }
        public int AuthId { get; set; }
        public virtual Auth RatedBy { get; set; } = null!;
        public int RecipeId { get; set; }
        public virtual Recipe RatedRecipe { get; set; } = null!;
        public string? Comment { get; set; }
    }

    public class ARating
    {
        [Key]
        public int Id { get; set; }
        public int Grade { get; set; }
        public int AuthId { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
    }
}
