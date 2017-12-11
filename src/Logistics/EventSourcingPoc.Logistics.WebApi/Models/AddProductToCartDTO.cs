using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingPoc.Logistics.WebApi.Models
{
    public class AddProductToCartDTO
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
