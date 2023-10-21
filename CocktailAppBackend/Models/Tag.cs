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
        public string Title { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
