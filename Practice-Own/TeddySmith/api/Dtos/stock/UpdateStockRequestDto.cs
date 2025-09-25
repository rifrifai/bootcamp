using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(6, ErrorMessage = "Symbol cannot exceed 6 characters.")]
        public string? Symbol { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Company Name cannot exceed 30 characters.")]
        public string? CompanyName { get; set; }
        [Required]
        [Range(1, 1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Industry cannot exceed 20 characters.")]
        public string? Industry { get; set; }
        [Range(1, 50000000000, ErrorMessage = "Market Cap must be between 1 and 50 billion.")]
        public long MarketCap { get; set; }
    }
}