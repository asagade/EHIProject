using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHIProject.Data;
using EHIProject.Model;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace EHIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ILogger<ContactController> _logger;
        private readonly IMapper _mapper;

        public ContactController(IContactRepository contactRepository, ILogger<ContactController> logger, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            try
            {
                var contact = await _contactRepository.GetContact();
                var result = _mapper.Map<IList<ContactDTO>>(contact);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetContacts)}" + ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "Contact not found");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetContacts(int id)
        {
            try
            {
                var contact = await _contactRepository.GetContactById(id);
                var result = _mapper.Map<ContactDTO>(contact);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(GetContacts)}" + ex.Message);
                return StatusCode(500, "Internal server error,Please try again. Error: " + ex.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(AddContact)}");
                return BadRequest(ModelState);
            }

            try
            {
                var contact = _mapper.Map<Contact>(contactDTO);
                Contact newContact = await _contactRepository.Add(contact);
                return CreatedAtAction(nameof(GetContacts), new { id = newContact.Id }, newContact);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AddContact)}" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new recored");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Contact> DeleteContact(int id)
        {
            try
            {
                Contact contact = _contactRepository.GetContactById(id).Result;

                if (contact == null)
                {
                    return NotFound("Contact not found");
                }

                return _contactRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(DeleteContact)}" + ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, "Error contact delete");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult<Contact> UpdateContact(int id, [FromBody] UpdateContactDTO contactDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                
                Contact contactToUpdate = _contactRepository.GetContactById(id).Result;
                
                _mapper.Map(contactDTO, contactToUpdate);
                return _contactRepository.Update(contactToUpdate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(UpdateContact)}" + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
