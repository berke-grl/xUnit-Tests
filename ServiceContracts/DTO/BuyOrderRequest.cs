using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class BuyOrderRequest : IValidatableObject
    {
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name can't be null or empty")]
        public string? StockName { get; set; }
        public DateTime DateAndTimeOfOrder { get; set; }

        [Range(1, 100000, ErrorMessage = "You can buy maximum of 100000 shares in single order. Minimum is 1.")]
        public uint Quantity { get; set; }

        [Range(1, 10000, ErrorMessage = "The maximum price of stock is 10000. Minimum is 1")]
        public double Price { get; set; }

        // Converts the current object of BuyOrderRequest into a new object of BuyOrder type
        public BuyOrder ToBuyOrder()
        {
            return new BuyOrder()
            {
                DateAndTimeOfOrder = DateAndTimeOfOrder,
                Quantity = Quantity,
                Price = Price,
                StockSymbol = StockSymbol
            };
        }
        // Model class-level validation using IValidatableObject
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            //Date of order should be less than Jan 01, 2000
            if (DateAndTimeOfOrder < Convert.ToDateTime("2000-01-01"))
            {
                validationResults.Add(new ValidationResult("Date of the order should not be older than Jan 01, 2000."));
            }
            return validationResults;
        }
    }
}
