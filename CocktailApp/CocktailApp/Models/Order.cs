using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailApp.Models
{
    public class Order
    {
        public string Id { get; set; }
        public string Recipe { get; set; }
        public int Amount { get; set; }
        public string Note { get; set; }

    }
}
