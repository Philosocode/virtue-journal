using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtueApi.Data;
using VirtueApi.Data.Entities;
using VirtueApi.Data.Repositories;

namespace VirtueApi.Controllers
{
    [Route("api/[controller]")]
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
        
        // GET api/entries
        [HttpGet("{virtueId:int}")]
        public IActionResult GetEntriesForVirtue(int virtueId)
        {
            return Ok(_unitOfWork.Entries.GetEntriesByVirtueId(virtueId));
        }
        
        // GET api/entries/1
        [HttpGet("{entryId}")] 
        public async Task<ActionResult<Entry>> GetEntryById(int entryId)
        {
            var entry = await _unitOfWork.Entries.GetByIdAsync(entryId);
            
            if (entry == null)
                return NotFound();

            return Ok(entry);
        }
        
        // POST api/entries
        [HttpPost]
        public void CreateEntry([FromBody] Entry entry)
        {
        }

        // PATCH api/entries/1
        [HttpPatch("{id}")]
        public void UpdateEntry(int id, [FromBody] Entry entry)
        {
        }

        // DELETE api/entries/5
        [HttpDelete("{id}")]
        public void DeleteEntry(int id)
        {
        }
    }
}