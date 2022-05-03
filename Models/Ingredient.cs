using System;
namespace FoodApi.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double ServingSize { get; set; }
        public string ServingSizeUnit { get; set; }
    }
}
