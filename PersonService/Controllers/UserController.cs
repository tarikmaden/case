using Microsoft.AspNetCore.Mvc;
using Entity.Models;
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
        public IActionResult Index()
        {
            var usersWithContacts = _context.Users.ToList();

            var userResources = _mapper.Map<List<UserResource>>(usersWithContacts);

            return Ok(userResources);
        }

        [HttpGet("{id}")]
        public IActionResult Show(Guid id)
        {
            var userWithContacts = _context.Users.FirstOrDefault(p => p.Id == id);

            if (userWithContacts == null)
            {
                return NotFound();
            }

            var userResource = _mapper.Map<UserResource>(userWithContacts);

            return Ok(userResource);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserRequest request)
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

            return CreatedAtAction(nameof(Show), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = request.FirstName;
            existingUser.LastName = request.LastName;
            existingUser.Company = request.Company;

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            var userResource = _mapper.Map<UserResource>(existingUser);

            return CreatedAtAction(nameof(Show), new { id = userResource.Id }, userResource);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
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
