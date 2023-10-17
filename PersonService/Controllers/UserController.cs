using Microsoft.AspNetCore.Mvc;
using PersonService.Models;
using PersonService.DTO;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EntityContext _context;

        public UserController(EntityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Company = request.Company
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest();
            }

            var existingUser = _context.Users.FirstOrDefault(p => p.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Güncelleme işlemi
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Company = updatedUser.Company;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
