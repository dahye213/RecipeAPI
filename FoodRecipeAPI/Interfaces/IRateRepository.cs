using System;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Models;
using System.Linq.Expressions;

namespace FoodRecipeAPI.Interfaces
{
    public interface IRateRepository
	{
        Task<IEnumerable<Rate>> GetRateAsync();
        Task<Rate> GetByIdAsync(int id);
        Task<Rate> GetByScoreAsync(double score);
        Task<Rate> GetCommentAsync(string comment);
        Task AddRateAsync(Rate rate);
        Task UpdateRateAsync(Rate rate);
        Task<bool> DeleteRateAsync(int id);
        Task<bool> RateExistsAsync(int id);
        Task<bool> SaveAsync();
        // Task<decimal> GetRecipeRating(int id);
    }
}
