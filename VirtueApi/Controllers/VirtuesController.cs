using System;
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data;
using VirtueApi.Entities;
using VirtueApi.Shared;

namespace VirtueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public VirtuesController(IUnitOfWork unitOfWork, IMapper mapper, LinkGenerator linkGenerator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        
        [HttpGet]
        // TODO: Test no virtues
        public IActionResult GetVirtues()
        {
            try
            {
                return Ok(_unitOfWork.Virtues.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "Failed to retrieve virtues"
                );
            }
        }
        
        [HttpGet("{id}")] 
        // TODO: Make sure virtue belongs to user
        public async Task<IActionResult> GetVirtueAsync(int id)
        {
            var virtue = await _unitOfWork.Virtues.GetByIdAsync(id);
            
            if (virtue == null) return NotFound();

            return Ok(virtue);
        }
        
        [HttpPost]
        // TODO: Add Virtue to user
        public async Task<IActionResult> CreateVirtueAsync([FromForm] VirtueCreateDTO data)
        {
            var newVirtue = VirtueMappers.VirtueFromCreateDTO(data);

            await _unitOfWork.Virtues.AddAsync(newVirtue);

            if (!await _unitOfWork.Complete()) return BadRequest("Could not create virtue");
            
            return Created($"api/virtues/{newVirtue.VirtueId}", newVirtue);
        }

        [HttpPatch("{id}")]
        // TODO: Make sure the Virtue belongs to the user
        public async Task<IActionResult> UpdateVirtueAsync(int id, [FromForm] VirtueEditDTO updates)
        {
            var toUpdate = await _unitOfWork.Virtues.GetByIdAsync(id);
            if (toUpdate == null) return NotFound($"Could not find virtue with id of {id}");

            _mapper.Map(updates, toUpdate);
            
            if (!await _unitOfWork.Complete()) return BadRequest("Could not update virtue");
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        // TODO: Check that Virtue is owned by user
        public async Task<IActionResult> DeleteVirtueAsync(int id)
        {
            if (await _unitOfWork.Virtues.GetByIdAsync(id) == null) return NotFound();

            var virtue = await _unitOfWork.Virtues.GetByIdAsync((id));

            if (virtue == null) return NotFound($"Virtue with id {id} could not be found");

            _unitOfWork.Virtues.Remove(virtue);
            if (!await _unitOfWork.Complete()) return BadRequest("Could not delete virtue");
            
            return NoContent();
        }
    }
}