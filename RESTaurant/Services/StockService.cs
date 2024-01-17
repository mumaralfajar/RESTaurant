using Microsoft.EntityFrameworkCore;
using RESTaurant.Data;
using RESTaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTaurant.Services
{
    public class StockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetStock(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<Stock> GetStockByFoodId(int foodId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.FoodId == foodId);
        }

        public async Task<Stock> AddStock(Stock Stock)
        {
            _context.Stocks.Add(Stock);
            await _context.SaveChangesAsync();
            return Stock;
        }

        public async Task<Stock> UpdateStock(int id, Stock stock)
        {
            _context.Entry(stock).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock> DeleteStock(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                throw new ArgumentException("Stock with specified ID not found");
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StockId == id);
        }
    }
}
