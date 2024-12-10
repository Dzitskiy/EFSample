using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFSample.Models
{
    public class Order
    {
        //[Required]
        public int OrderId { get; set; }

        //[ForeignKey("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
