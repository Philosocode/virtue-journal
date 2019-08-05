using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtueApi.Data;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Data.Repositories;
using VirtueApi.Extensions;

namespace VirtueApi.Controllers
{
    [Authorize]
    [Route("api/entries")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EntriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEntries()
        {
            var userId = this.GetCurrentUserId();
            
            var entriesFromRepo = _unitOfWork.Entries.GetEntriesForUser(userId);
            var entriesToReturn = _mapper.Map<IEnumerable<EntryGetDto>>(entriesFromRepo);
            
            return Ok(entriesToReturn);
        }

        // GET api/entries/1
        [HttpGet("{entryId}", Name = "GetEntry")] 
        public async Task<ActionResult<Entry>> GetEntryByIdAsync(int entryId)
        {
            var entryEntity = await _unitOfWork.Entries.GetByIdAsync(entryId);
            
            if (entryEntity == null)
                return NotFound();
            
            if (entryEntity.UserId != this.GetCurrentUserId())
                return Unauthorized();
            
            var entryToReturn = _mapper.Map<EntryGetDto>(entryEntity);
            
            return Ok(entryToReturn);
        }
        
        // POST api/entries
        [HttpPost]
        public async Task<IActionResult> CreateEntryAsync(EntryCreateDto data)
        {
            if (data == null)
                return BadRequest();

            var entryEntity = _mapper.Map<Entry>(data);
            
            entryEntity.UserId = this.GetCurrentUserId();
            
            // Add virtue links
            if (data.VirtueLinks?.Count > 0)
            {
                if (!await VirtueLinksAreValid(data.VirtueLinks))
                    return StatusCode(422, "Virtue link ID(s) must be valid and unique.");

                entryEntity.VirtueLinks = await LinkVirtuesToEntryAsync(entryEntity, data.VirtueLinks); 
            }
            
            await _unitOfWork.Entries.AddAsync(entryEntity);

            if (!await _unitOfWork.Complete())
                throw new Exception("Creating an entry failed on save");

            var entryToReturn = _mapper.Map<EntryGetDto>(entryEntity);
            
            return CreatedAtRoute(
                "GetEntry",
                new { entryId = entryToReturn.EntryId },
                entryToReturn
            );
        }
        
        // PATCH api/entries/1
        [HttpPatch("{entryId}")]
        public async Task<IActionResult> UpdateEntryAsync(int entryId, EntryEditDto updates)
        {
            var toUpdate = await _unitOfWork.Entries.GetByIdAsync(entryId);
            
            if (toUpdate == null) 
                return NotFound($"Could not find entry with entryId {entryId}");

            var userId = this.GetCurrentUserId();
            if (toUpdate.UserId != userId)
                return Unauthorized();
            
            if (updates.VirtueLinks != null)
            {
                if (!await VirtueLinksAreValid(updates.VirtueLinks))
                    return StatusCode(422, "Virtue link ID(s) must be valid and unique.");

                toUpdate.VirtueLinks = await LinkVirtuesToEntryAsync(toUpdate, updates.VirtueLinks);
            }
            
            _mapper.Map(updates, toUpdate);
            
            if (!await _unitOfWork.Complete())
                return BadRequest($"Could not update entry with entryId {entryId}");
            
            return NoContent();
        }
        
        // DELETE api/entries/5
        [HttpDelete("{entryId}")]
        public async Task<IActionResult> DeleteEntryAsync(int id)
        {
            var entry = await _unitOfWork.Entries.GetByIdAsync(id);

            if (entry == null)
                return NotFound($"Entry with entryId {id} could not be found");

            if (entry.UserId != this.GetCurrentUserId())
                return Unauthorized();
            
            _unitOfWork.Entries.Remove(entry);
            
            if (!await _unitOfWork.Complete())
                return BadRequest($"Could not delete entry {id}");

            return NoContent();
        }
        
        // DELETE api/entries
        [HttpDelete]
        public async Task<IActionResult> DeleteAllEntriesAsync()
        {
            var userId = this.GetCurrentUserId();
            var entriesToDelete = _unitOfWork.Entries.GetEntriesForUser(userId);
            
            _unitOfWork.Entries.RemoveRange(entriesToDelete);
            
            if (!await _unitOfWork.Complete()) 
                return BadRequest($"Could not delete entries.");
            
            return NoContent();
        }

        private async Task<bool> VirtueLinksAreValid(ICollection<VirtueEntryCreateDto> virtueLinks)
        {
            // Check for duplicate IDs and make sure virtues exist
            foreach (var virtueLink in virtueLinks)
            {
                if (!await _unitOfWork.Virtues.Exists(virtueLink.VirtueId))
                    return false;
                
                var idCount = virtueLinks.Count(vl => vl.VirtueId == virtueLink.VirtueId);
                if (idCount > 1)
                    return false;
            }

            return true;
        }
        
        private async Task<ICollection<VirtueEntry>>LinkVirtuesToEntryAsync(Entry entry, ICollection<VirtueEntryCreateDto> virtueLinks)
        {
            var newVirtueLinks = new List<VirtueEntry>();
            
            foreach (var virtueEntryCreateDto in virtueLinks)
            {
                var virtueId = virtueEntryCreateDto.VirtueId;
                var virtueFromRepo = await _unitOfWork.Virtues.GetByIdAsync(virtueId);
                var virtueEntry = new VirtueEntry()
                {
                    Difficulty = virtueEntryCreateDto.Difficulty,
                    Entry = entry,
                    Virtue = virtueFromRepo
                };
                
                newVirtueLinks.Add(virtueEntry);
            }

            return newVirtueLinks;
        }
    }
}