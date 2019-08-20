using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtueJournal.Data.Dtos;
using VirtueJournal.Data.Entities;
using VirtueJournal.Data.Repositories;
using VirtueJournal.Shared.Extensions;

namespace VirtueJournal.API.Controllers
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
        public async Task<IActionResult> GetVirtues()
        {
            var userId = this.GetCurrentUserId();
            var virtuesFromRepo = await _unitOfWork.Virtues.GetVirtuesForUserAsync(userId);
            var virtues = _mapper.Map<IEnumerable<VirtueGetDto>>(virtuesFromRepo);
            
            return Ok(virtues);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult GetVirtuesDev()
        {
            var virtuesFromRepo = _unitOfWork.Virtues.GetVirtuesForUserAsync(1);
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
        
        [HttpGet("{virtueId}/entries")]
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
                return BadRequest($"Could not delete virtue {virtueId}.");
            
            return NoContent();
        }
        
        [HttpDelete("{virtueId}/entries")]
        public async Task<IActionResult> DeleteEntriesForVirtue(int virtueId)
        {
            var userId = this.GetCurrentUserId();
            
            if (!await _unitOfWork.Virtues.BelongsToUser(virtueId, userId))
                return Unauthorized();
            
            var entriesForVirtue = _unitOfWork.Entries.GetEntriesByVirtueId(virtueId);
            
            _unitOfWork.Entries.RemoveRange(entriesForVirtue);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete entries for virtue {virtueId}.");
            
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllVirtuesAsync()
        {
            var userId = this.GetCurrentUserId();
            var virtuesToDelete = await _unitOfWork.Virtues.GetVirtuesForUserAsync(userId);
            
            _unitOfWork.Virtues.RemoveRange(virtuesToDelete);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete virtues.");
            
            return NoContent();
        }
    }
}