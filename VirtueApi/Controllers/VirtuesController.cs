using System;
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Extensions;
using VirtueApi.Services.Repositories;

namespace VirtueApi.Controllers
{
    [Authorize]
    [Route("api/virtues")]
    [ApiController]
    public class VirtuesController : ControllerBase
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
            var userId = this.GetCurrentUserId();
            var virtuesFromRepo = _unitOfWork.Virtues.GetVirtuesForUser(userId);
            var virtues = _mapper.Map<IEnumerable<VirtueGetDto>>(virtuesFromRepo);
            
            return Ok(virtues);
        }
        
        [HttpGet("{virtueId}", Name = "GetVirtue")] 
        public async Task<IActionResult> GetVirtueAsync(int virtueId)
        {
            var virtueFromRepo = await _unitOfWork.Virtues.GetByIdAsync(virtueId);

            if (virtueFromRepo == null) 
                return NotFound();
            
            if (virtueFromRepo.UserId != this.GetCurrentUserId())
                return Unauthorized();
            
            var virtueToReturn = _mapper.Map<VirtueGetDto>(virtueFromRepo);

            return Ok(virtueToReturn);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateVirtueAsync(VirtueCreateDto data)
        {
            if (data == null)
                return BadRequest("Failed to create virtue");
            
            var virtueEntity = _mapper.Map<Virtue>(data);
            virtueEntity.UserId = this.GetCurrentUserId();

            await _unitOfWork.Virtues.AddAsync(virtueEntity);

            if (!await _unitOfWork.Complete())
                throw new Exception("Creating a virtue failed on save.");

            var virtueToReturn = _mapper.Map<VirtueGetDto>(virtueEntity);
            
            return CreatedAtRoute(
                "GetVirtue",
                new { virtueId = virtueToReturn.VirtueId },
                virtueToReturn
            );
        }
        
        [HttpGet("{virtueId:int}/entries")]
        public async Task<IActionResult> GetEntriesForVirtueAsync(int virtueId)
        {
            var userId = this.GetCurrentUserId();
            
            if (!await _unitOfWork.Virtues.BelongsToUser(virtueId, userId))
                return Unauthorized();
            
            var entriesFromRepo = _unitOfWork.Entries.GetEntriesByVirtueId(virtueId);
            var entries = _mapper.Map<IEnumerable<EntryGetDto>>(entriesFromRepo);
            
            return Ok(entries);
        }

        // PATCH api/virtues/1
        [HttpPatch("{virtueId}")]
        public async Task<IActionResult> UpdateVirtueAsync(int virtueId, VirtueEditDto updates)
        {
            var toUpdate = await _unitOfWork.Virtues.GetByIdAsync(virtueId);
            
            if (toUpdate == null) 
                return NotFound($"Could not find virtue with virtueId of {virtueId}");
            
            if (toUpdate.UserId != this.GetCurrentUserId())
                return Unauthorized();

            _mapper.Map(updates, toUpdate);
            
            if (!await _unitOfWork.Complete())
                return BadRequest("Could not update virtue");
            
            return NoContent();
        }
        
        [HttpDelete("{virtueId}")]
        public async Task<IActionResult> DeleteVirtueAsync(int virtueId)
        {
            var virtue = await _unitOfWork.Virtues.GetByIdAsync(virtueId);

            if (virtue == null)
                return NotFound($"Virtue with virtueId {virtueId} could not be found");

            if (virtue.UserId != this.GetCurrentUserId())
                return Unauthorized();

            _unitOfWork.Virtues.Remove(virtue);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete virtue {virtueId}");
            
            return NoContent();
        }
    }
}