using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VirtueApi.Data;
using VirtueApi.Entities;

namespace VirtueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly DataContext _context;

        public EntriesController(DataContext context)
        {
            _context = context;
        }
        
        // GET api/entries
        [HttpGet]
        public ActionResult<IEnumerable<Entry>> GetEntries()
        {
            return _context.Entries.ToList();
        }
        
        // GET api/entries/1
        [HttpGet("{id}")] 
        public ActionResult<Entry> GetEntry(int id) 
        {    
            var entry = _context.Entries.Find(id);     
            if (entry == null)
            {         
                return NotFound();     
            }
            return entry; 
        }
        
        // POST api/entries
        [HttpPost]
        public void Post([FromBody] Entry entry)
        {
        }

        // PUT api/entries/1
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Entry entry)
        {
        }

        // DELETE api/entries/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}