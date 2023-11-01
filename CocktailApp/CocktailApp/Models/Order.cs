using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Recipe Recipe { get; set; }
        public int? CreatedByUserId { get; set; }
        public virtual Auth CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }

    public class AOrder
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }
}
