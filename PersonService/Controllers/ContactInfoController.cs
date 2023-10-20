using Microsoft.AspNetCore.Mvc;
using Entity.Models;
using PersonService.DTO;
using Microsoft.EntityFrameworkCore;

namespace PersonService.Controllers
{
    [ApiController]
    [Route("api/contactinfo")]
    public class ContactInfoController : ControllerBase
    {
        private readonly EntityContext _context;

        public ContactInfoController(EntityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ContactInfo>> GetContactInfos()
        {
            var contactInfos = _context.ContactInfos.Include(ci => ci.ContactTypes).ToList();

            return Ok(contactInfos);
        }

        [HttpGet("{id}")]
        public ActionResult<ContactInfo> Show(Guid id)
        {
            var contactInfo = _context.ContactInfos.Include(ci => ci.ContactTypes).FirstOrDefault(p => p.Id == id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            return Ok(contactInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactInfoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound($"Kullanıcı bulunamadı. UserId: {request.UserId}");

            }

            var contactInfo = new ContactInfo
            {
                UserId = request.UserId,
                ContactTypeId = request.ContactTypeId,
                Content = request.Content
            };

            _context.ContactInfos.Add(contactInfo);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Show), new { id = contactInfo.Id }, contactInfo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ContactInfoRequest request)
        {
            if (request == null)
            {
                return BadRequest("Geçersiz veri.");
            }

            var contactInfo = await _context.ContactInfos.FindAsync(id);

            if (contactInfo == null)
            {
                return NotFound("İletişim bilgisi bulunamadı.");
            }

            contactInfo.Content = request.Content;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Show), new { id = contactInfo.Id }, contactInfo);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var contactInfo = await _context.ContactInfos.FindAsync(id);

            if (contactInfo == null)
            {
                return NotFound("İletişim bilgisi bulunamadı.");
            }

            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
