using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public Recipe Recipe { get; set; } // Navigationseigenschaft zu Recipe hinzugefügt
        public Auth CreatedByUser { get; set; } // Navigationseigenschaft zu Auth hinzugefügt
        public DateTime CreatedAt { get; set; }
        public int Amount { get; set; }
        public string? Note { get; set; }
        public string Status { get; set; }
    }
}
