﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocktailApp.Models
{
    public class RecipeDetail
    {
        [Key]
        public int Id { get; set; }
        public float AmountInOz { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; } = null!;
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; } = null!;
    }

}
