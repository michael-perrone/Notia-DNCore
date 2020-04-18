using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notia.Data;
using Notia.Dtos;

namespace Notia.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartTwoController : ControllerBase
    {
        private readonly DataContext _context;
        public PartTwoController(DataContext context) {
            _context = context;
        }
        [HttpPost("needed")]
        public async Task<IActionResult> RegisterPartTwo(UserRegisterPtTwoNeededDto partTwo) {

            var userFound = await _context.Users.FirstOrDefaultAsync(x => x.Email == partTwo.Email && x.Id == partTwo.Id);
            if (userFound == null) {
                return Unauthorized("User not found");
            }
            return Ok(userFound.NewUser);
        }
    }
}