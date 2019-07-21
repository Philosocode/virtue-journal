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

        // GET api/entries/1
        [HttpGet("{entryId}", Name = "GetEntry")] 
        public async Task<ActionResult<Entry>> GetEntryByIdAsync(int entryId)
        {
            var entry = await _unitOfWork.Entries.GetByIdAsync(entryId);
            
            if (entry == null)
                return NotFound();

            return Ok(entry);
        }
        
        // POST api/entries
        [HttpPost]
        public async Task<IActionResult> CreateEntryAsync(EntryCreateDto data)
        {
            if (data == null)
                return BadRequest();

            var entryEntity = _mapper.Map<Entry>(data);
            
            // Link multiple Virtues to the Entry
            foreach (var virtueEntryCreateDto in data.VirtuesLink)
            {
                var id = virtueEntryCreateDto.VirtueId;
                var virtueFromRepo = await _unitOfWork.Virtues.GetByIdAsync(id);

                if (virtueFromRepo == null)
                    return NotFound($"Virtue with id {id} not found.");

                var virtueEntry = new VirtueEntry()
                {
                    Difficulty = virtueEntryCreateDto.Difficulty,
                    Entry = entryEntity,
                    Virtue = virtueFromRepo
                };
                
                entryEntity.VirtuesLink.Add(virtueEntry);
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
        public void UpdateEntry(int id, [FromBody] Entry entry)
        {
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