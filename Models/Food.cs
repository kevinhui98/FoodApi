using System;
namespace FoodApi.Models
{
    public class Food
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public bool? glutenFree { get; set; }
        public bool? vegan { get; set; }
        public double protein { get; set; }
        public double carbs { get; set; }
        public Ingredient? Ingredient { get; set; }
        

    }
}
