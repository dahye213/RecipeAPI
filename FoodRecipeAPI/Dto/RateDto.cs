﻿using System;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Dto
{
    public class RateDto
    {
        public int Id { get; set; }
        public double score { get; set; }
        public string comment { get; set; }
        public int RecipeId { get; set; }
    }
}
