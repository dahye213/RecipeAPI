using System;
using System.Runtime.CompilerServices;
using FoodRecipeAPI.Data;
using FoodRecipeAPI.Dto;
using FoodRecipeAPI.Interfaces;
using FoodRecipeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeAPI.Repository
{
    public class RateRepository : IRateRepository
    {
        private readonly DataContext _context;
        public RateRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddRateAsync(Rate rate)
        {
            await _context.Rates.AddAsync(rate);
            await _context.SaveChangesAsync();
        }

        // Delete Rate.
        public async Task<bool> DeleteRateAsync(int id)
        {
            var rateToDelete = await _context.Rates.Include(r => r.Recipes).SingleAsync(r => r.Id == id);
            var rateTemp = new Rate();
            if (rateToDelete != null)
            {
                rateToDelete.Recipes.Clear();
                _context.Rates.Remove(rateToDelete);
            }

            return await SaveAsync();
        }
        

        public async Task<Rate> GetByIdAsync(int id)
        {
            var rate = await _context.Rates.FirstOrDefaultAsync(r => r.Id == id);
            if (rate == null)
            {
                throw new Exception("The rating does not exist");
            }
            return rate;
        }

        public async Task<Rate> GetByScoreAsync(double score)
        {
            var rate = await _context.Rates.FirstOrDefaultAsync(r => r.score == score);
            if (rate == null)
            {
                throw new Exception("This score does not exist");
            }
            return rate;
        }

        public async Task<Rate> GetCommentAsync(string comment) 
        {
            var rate = await _context.Rates.FirstOrDefaultAsync(r => r.comment == comment);
            if (rate == null) 
            {
                throw new Exception("This comment does not exist");
            }
            return rate;

        }

        public async Task<IEnumerable<Rate>> GetRateAsync()
        {
            var rate = await _context.Rates.ToListAsync();
            return rate.OrderBy(r => r.Id);
        }

        public async Task<bool> RateExistsAsync(int id)
        {
            return await _context.Rates.AnyAsync(r => r.Id == id);
        }

        public async Task UpdateRateAsync(Rate rate)
        {
            _context.Rates.Update(rate);
            await SaveAsync();
        }


        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
