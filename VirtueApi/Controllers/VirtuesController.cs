using System;
using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtueApi.Data;
using VirtueApi.Entities;
using VirtueApi.Repositories;

namespace VirtueApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtuesController : Controller
    {
        private IVirtueRepository _repo;
        
        public VirtuesController(IVirtueRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public Task<JsonResult> GetVirtues()
        {
            return Json(_repo.GetAll());
        }
        
        [HttpGet("{id}", Name = "GetVirtue")] 
        public async Task<ActionResult<Virtue>> GetByIdAsync(long id)
        {
            return Json(_repo.GetById(id));
        }
    }
}