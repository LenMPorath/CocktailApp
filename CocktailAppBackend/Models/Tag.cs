using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public virtual ICollection<Recipe>? Recipes { get; set; }
    }
    public class ATag
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public virtual ICollection<int>? RecipeList { get; set; }
    }
}
