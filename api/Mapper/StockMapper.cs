using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToStockDto(this Stock stockModel){
            return new StockDto{
                    Id = stockModel.Id,
                    Symbol = stockModel.Symbol,
                    CompanyName = stockModel.CompanyName,
                    Purchase = stockModel.Purchase,
                    LastDiv = stockModel.LastDiv,
                    Industry = stockModel.Industry,
                    MarketCap = stockModel.MarketCap
            };
        }

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto){
            return new Stock{
                    Symbol = stockDto.Symbol,
                    CompanyName = stockDto.CompanyName,
                    Purchase = stockDto.Purchase,
                    LastDiv = stockDto.LastDiv,
                    Industry = stockDto.Industry,
                    MarketCap = stockDto.MarketCap
            };
        }

        public static void ToStockFromUpdateDto(this Stock stock, UpdateStockRequestDto stockDto){
                    stock.Symbol = stockDto.Symbol;
                    stock.CompanyName = stockDto.CompanyName;
                    stock.Purchase = stockDto.Purchase;
                    stock.LastDiv = stockDto.LastDiv;
                    stock.Industry = stockDto.Industry;
                    stock.MarketCap = stockDto.MarketCap;
        }

    }
}