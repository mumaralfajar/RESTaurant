using Microsoft.EntityFrameworkCore;
using RESTaurant.Data;
using RESTaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Services
{
    public class FoodService
    {
        private readonly ApplicationDbContext _context;
        private readonly StockService _stockService;

        public FoodService(ApplicationDbContext context, StockService stockService)
        {
            _context = context;
            _stockService = stockService;
        }

        public async Task<List<Food>> GetFoods()
        {
            return await _context.Foods.ToListAsync();
        }

        public async Task<Food> GetFood(int id)
        {
            return await _context.Foods.FindAsync(id);
        }

        public async Task<Food> AddFood(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            // Create a new stock object for the food
            var stock = new Stock
            {
                FoodId = food.FoodId,
                QuantityChange = 0, // Set the initial stock level
            };

            // Add the stock to the database
            await _stockService.AddStock(stock);

            return food;
        }

        public async Task<Food> UpdateFood(int id, Food food)
        {
            _context.Entry(food).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return food;
        }

        public async Task<Food> DeleteFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                throw new ArgumentException("Food with specified ID not found");
            }

            var stock = await _stockService.GetStockByFoodId(id);
            if (stock == null)
            {
                throw new ArgumentException("Stock for specified Food ID not found");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();

                await _stockService.DeleteStock(stock.StockId);

                transaction.Commit();
            }

            return food;
        }

        public bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodId == id);
        }
    }
}