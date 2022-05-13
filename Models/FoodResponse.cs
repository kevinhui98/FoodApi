using System;
using Microsoft.EntityFrameworkCore;

namespace FoodApi.Models
{
    public class FoodResponse
    {
        public int statusCode { get; set; }
        public string? statusDescription { get; set; }
        public List<Food> food { get; set; } = new();
    }
}
