using AutoMapper;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Models;

namespace FoodRecipeAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recipe, RecipeDto>();
            CreateMap<RecipeDto, Recipe>();
        }
    }
}
