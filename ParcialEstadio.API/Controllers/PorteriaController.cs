using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;

namespace ParcialEstadio.API.Controllers
{
    [ApiController]
    [Route("/api/porteria")]
    public class PorteriaController : ControllerBase
    {
        private readonly DataContext _context;

        public PorteriaController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("combo")]
        public async Task<ActionResult> GetCombo()
        {
            return Ok(await _context.Porterias.ToListAsync());
        }
    }
}
