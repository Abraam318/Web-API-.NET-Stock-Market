using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stocks
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10,ErrorMessage ="Symbol cann't be more than 10 characters")]
        public string Symbol {get; set;} = String.Empty;
        [Required]
        [MaxLength(10,ErrorMessage ="Company cann't be more than 10 characters")]
        public string CompanyName {get; set;} = String.Empty;
        [Required]
        [Range(1,100000)]
        public decimal Purchase {get; set;}
        [Required]
        [Range(0.01,100)]
        public decimal LastDiv {get; set;}
        [Required]
        [MaxLength(10,ErrorMessage ="Industry cann't be more than 10 characters")]
        public string Industry {get; set;} = String.Empty;
        [Range(1,500000000)]
        public long MarketCap { get; set;}
    }
}