using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Recipe> Recipes { get; set; }
    }
    public class ATag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<int> RecipeList { get; set; }
    }
}
