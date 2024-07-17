using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stocks;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository :IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await  _context.SaveChangesAsync();   
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await GetByIDAsync(id);
            if(stockModel == null){
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query){

           var stocks =   _context.Stocks.Include(c=>c.Comments).AsQueryable();
           if(!string.IsNullOrWhiteSpace(query.CompanyName)){
            stocks = stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
           }
           if(!String.IsNullOrWhiteSpace(query.Symbol)){
            stocks = stocks.Where(s=>s.Symbol.Contains(query.Symbol));
           }
          if(!String.IsNullOrWhiteSpace(query.SortBy)){
            if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                stocks = query.IsDecsending ? stocks.OrderByDescending(s=>s.Symbol) : stocks.OrderBy(s=>s.Symbol);
            }
           }
            var SkipNumber = (query.PageNumber - 1)  * query.PageSize;

           return await stocks.Skip(SkipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIDAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(x=>x.Id == id);
        }

        public Task<bool> StockExist(int id)
        {
            return _context.Stocks.AnyAsync(x=>x.Id==id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await GetByIDAsync(id) ;

            if(stockModel==null){
                return null;
            }

            stockModel.ToStockFromUpdateDto(stockDto);
            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}