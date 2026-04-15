using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        public ContactInfoController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        [HttpGet]
        public async Task<ActionResult<ContactInfoDto>> Get()
        {
            return await _contactRepository.GetContactInfoAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactInfoDto dto)
        {
            await _contactRepository.UpdateContactInfoAsync(dto);
            return Ok(new { message = "Updated successfully" });
        }
    }
}
