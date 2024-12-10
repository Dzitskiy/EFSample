namespace EFSample.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
