namespace EFSample.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public UserProfile? Profile { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }

    //public class Manager : User
    //{ 
    //    public string? Department { get; set; }
    
    //}

    //public class Employee : User 
    //{
    //    public decimal? Salary { get; set; }
    //}
}
