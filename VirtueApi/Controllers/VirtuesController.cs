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
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Data.Repositories;

namespace VirtueApi.Controllers
{
    [Route("api/virtues")]
    [ApiController]
    public class VirtuesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VirtuesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult GetVirtues()
        {
            var virtuesFromRepo = _unitOfWork.Virtues.GetAll();
            var virtues = _mapper.Map<IEnumerable<VirtueGetDto>>(virtuesFromRepo);
            
            return Ok(virtues);
        }
        
        [HttpGet("{id}", Name = "GetVirtue")] 
        // TODO: Make sure virtue belongs to user
        public async Task<IActionResult> GetVirtueAsync(int id)
        {
            var virtueFromRepo = await _unitOfWork.Virtues.GetByIdAsync(id);
            
            if (virtueFromRepo == null) 
                return NotFound();
            
            var virtueToReturn = _mapper.Map<VirtueGetDto>(virtueFromRepo);

            return Ok(virtueToReturn);
        }
        
        [HttpPost]
        // TODO: Add Virtue to user
        public async Task<IActionResult> CreateVirtueAsync(VirtueCreateDto data)
        {
            if (data == null)
                return BadRequest("Failed to create virtue");
            
            var virtueEntity = _mapper.Map<Virtue>(data);

            await _unitOfWork.Virtues.AddAsync(virtueEntity);

            if (!await _unitOfWork.Complete())
                throw new Exception("Creating a virtue failed on save");

            var virtueToReturn = _mapper.Map<VirtueGetDto>(virtueEntity);
            
            return CreatedAtRoute(
                "GetVirtue",
                new { id = virtueToReturn.VirtueId },
                virtueToReturn
            );
        }
        
        [HttpGet("{virtueId:int}/entries")]
        public async Task<IActionResult> GetEntriesForVirtue(int virtueId)
        {
            if (!await _unitOfWork.Virtues.Exists(virtueId))
                return NotFound($"Could not find virtue with id of {virtueId}");
            
            var entriesFromRepo = _unitOfWork.Entries.GetEntriesByVirtueId(virtueId);
            var entries = _mapper.Map<IEnumerable<EntryGetDto>>(entriesFromRepo);
            
            return Ok(entries);
        }

        // PATCH api/virtues/1
        [HttpPatch("{id}")]
        // TODO: Make sure the Virtue belongs to the user
        public async Task<IActionResult> UpdateVirtueAsync(int id, VirtueEditDto updates)
        {
            var toUpdate = await _unitOfWork.Virtues.GetByIdAsync(id);
            if (toUpdate == null) 
                return NotFound($"Could not find virtue with id of {id}");

            _mapper.Map(updates, toUpdate);
            
            if (!await _unitOfWork.Complete())
                return BadRequest("Could not update virtue");
            
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        // TODO: Check that Virtue is owned by user
        public async Task<IActionResult> DeleteVirtueAsync(int id)
        {
            var virtue = await _unitOfWork.Virtues.GetByIdAsync(id);

            if (virtue == null)
                return NotFound($"Virtue with id {id} could not be found");

            _unitOfWork.Virtues.Remove(virtue);
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete virtue {id}");
            
            return NoContent();
        }
    }
}