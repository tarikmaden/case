using Microsoft.AspNetCore.Mvc;
using PersonService.Models;
using PersonService.DTO;
using AutoMapper;
using PersonService.Resource;
using Microsoft.EntityFrameworkCore;

namespace PersonService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EntityContext _context;
        private readonly IMapper _mapper;

        public UserController(EntityContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var usersWithContacts = _context.Users
            .Include(u => u.ContactInfos)
            .ThenInclude(ci => ci.ContactTypes)
            .ToList();

            // AutoMapper ile projeksiyon işlemi
            var userResources = _mapper.Map<List<UserResource>>(usersWithContacts);

            return Ok(userResources);
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

        [HttpGet("users/{userId}/contactinfos")]
        public IActionResult GetUserContactInfos(Guid userId)
        {
            // Verilen userId'ye sahip kullanıcıyı bulun
            var user = _context.Users.Include(u => u.ContactInfos).FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            // Kullanıcının iletişim bilgilerini döndürün
            var contactInfos = user.ContactInfos;

            return Ok(contactInfos);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest request)
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
