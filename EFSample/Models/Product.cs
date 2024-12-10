using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EFSample.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int OrderId { get; set; } 
        public Order Order { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        }
}
