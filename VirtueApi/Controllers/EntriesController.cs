using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtueApi.Data;
using VirtueApi.Data.Dtos;
using VirtueApi.Data.Entities;
using VirtueApi.Data.Repositories;

namespace VirtueApi.Controllers
{
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
        public IActionResult GetAllEntriesDev()
        {
            var entriesFromRepo = _unitOfWork.Entries.GetAll();
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
            
            // Add virtue links
            if (data.VirtueLinks.Count > 0)
            {
                if (!VirtueLinkIdsAreUnique(data.VirtueLinks))
                    return StatusCode(422, "Virtue link IDs must be unique.");
                if (!await VirtueLinkIdsExistAsync(data.VirtueLinks))
                    return StatusCode(422, "Virtue IDs must be valid.");

                LinkVirtuesToEntryAsync(entryEntity, data.VirtueLinks);
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
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEntryAsync(int id, EntryEditDto updates)
        {
            var toUpdate = await _unitOfWork.Entries.GetByIdAsync(id);
            
            if (toUpdate == null) 
                return NotFound($"Could not find entry with id {id}");
            
            if (updates.VirtueLinks?.Count > 0)
            {
                if (!VirtueLinkIdsAreUnique(updates.VirtueLinks))
                    return StatusCode(422, "Virtue link ID(s) must be unique.");
                if (!await VirtueLinkIdsExistAsync(updates.VirtueLinks))
                    return StatusCode(422, "Virtue ID(s) must be valid.");

                LinkVirtuesToEntryAsync(toUpdate, updates.VirtueLinks);
            }
            
            _mapper.Map(updates, toUpdate);
            
            if (!await _unitOfWork.Complete())
                return BadRequest($"Could not update entry with id {id}");
            
            return NoContent();
        }

        private bool VirtueLinkIdsAreUnique(ICollection<VirtueEntryCreateDto> virtueLinks)
        {
            // Check for duplicate IDs and invalid difficulties
            foreach (var virtueLink in virtueLinks)
            {
                var idCount = virtueLinks.Count(vl => vl.VirtueId == virtueLink.VirtueId);
                if (idCount > 1)
                    return false;
            }

            return true;
        }
        
        private async Task<bool> VirtueLinkIdsExistAsync(ICollection<VirtueEntryCreateDto> virtueLinks)
        {
            foreach (var virtueLink in virtueLinks)
            {
                if (!await _unitOfWork.Virtues.Exists(virtueLink.VirtueId))
                    return false;
            }

            return true;
        }

        
        private async void LinkVirtuesToEntryAsync(Entry entry, ICollection<VirtueEntryCreateDto> virtueLinks)
        {
            entry.VirtueLinks = new List<VirtueEntry>();
            
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
                
                entry.VirtueLinks.Add(virtueEntry);
            }
        }

        // DELETE api/entries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntryAsync(int id)
        {
            var entry = await _unitOfWork.Entries.GetByIdAsync(id);

            if (entry == null)
                return NotFound($"Entry with id {id} could not be found");
            
            _unitOfWork.Entries.Remove(entry);
            if (!await _unitOfWork.Complete())
                return BadRequest($"Could not delete entry {id}");

            return NoContent();
        }
    }
}