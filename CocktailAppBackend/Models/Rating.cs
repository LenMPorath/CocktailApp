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
        public Auth RatedBy { get; set; }
        public Recipe RatedRecipe { get; set; }
        public string? Comment { get; set; }
    }
}
