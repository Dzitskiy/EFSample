using EFSample.Data;
using EFSample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {;
            //var user = _context.Users.FirstOrDefault();
            //_context.Entry(user).Collection(u => u.Orders).Load();
            //_context.Entry(user).Reference(u => u.Profile).Load();

            return await _context.Users
                .Include(u => u.Profile)
                .Include(u => u.Orders)
                .ThenInclude(o => o.Products).ToListAsync();



        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            //var u = new User { Id = 1, Name = "User" };

            _context.Users.Add(user);

            //var emp = new Employee
            //{
            //    Id = 2,
            //    Name = "Emp",
            //    Salary = 999
            //};

            //_context.Employees.Add(emp);

            //var m = new Manager
            //{
            //    Id = 3,
            //    Name = "M",
            //    Department = "it"
            //};

            //_context.Managers.Add(m);
            
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            var currentUser = await _context.Users.FindAsync(user.UserId);

            if (currentUser == null)
            {
                currentUser = user;
                //currentUser.Name = user.Name;
                await _context.SaveChangesAsync();
            }

            return Ok(currentUser);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var currentUser = await _context.Users.FindAsync(id);

            if (currentUser != null)
            {
                _context.Users.Remove(currentUser);
                await _context.SaveChangesAsync();
            }

            return Ok(currentUser);
        }
    }
}
