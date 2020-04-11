using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notia.Data;

namespace Notia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController: ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var values = await _context.Values.ToListAsync();
            List<int> ids = new List<int>();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }
    }
}