using System;
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IVirtueRepository _repo;

        public VirtuesController(IVirtueRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        // TODO: Test no virtues
        public IActionResult GetVirtues()
        {
            return Ok(_repo.GetAll());
        }
        
        [HttpGet("{id}")] 
        // TODO: Make sure virtue belongs to user
        public async Task<IActionResult> GetVirtueAsync(long id)
        {
            var virtue = await _repo.GetById(id);

            if (virtue == null)
                return NotFound();
            
            return Ok(virtue);
        }

        [HttpPost]
        // TODO: Add Virtue to user
        public async Task<IActionResult> CreateVirtueAsync([FromForm] VirtueCreateDTO data)
        {
            var newVirtue = VirtueMappers.VirtueFromCreateDTO(data);
            await _repo.Create(newVirtue);
            return Ok();
        }

        [HttpPatch("{id}")]
        // TODO: Make sure the Virtue belongs to the user
        public async Task<IActionResult> UpdateVirtueAsync(long id, [FromForm] VirtueEditDTO updates)
        {
            if (!await _repo.Exists(id)) 
                return NotFound();

            var oldVirtue = await _repo.GetById(id);
            var updatedVirtue = VirtueMappers.VirtueFromEditDTO(oldVirtue, updates);

            await _repo.Update(updatedVirtue);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        // TODO: Check that Virtue is owned by user
        public async Task<IActionResult> DeleteVirtueAsync(long id)
        {
            if (!await _repo.Exists(id)) return NotFound();

            try
            {
                await _repo.Delete(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}