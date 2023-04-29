using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialEstadio.Shared.DTOs;
using Sales.API.Data;

namespace ParcialEstadio.API.Controllers
{
    [ApiController]
    [Route("/api/estadio")]
    public class BoletasController : ControllerBase
    {
        private readonly DataContext _context;

        public BoletasController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var boleta = await _context.Boletas.FirstOrDefaultAsync(x => x.Id == id);
            if (boleta == null)
            {
                return BadRequest("Boleta no Valida");
            }

            if (boleta.Usada == true && boleta.FechaUso != null)
            {
                return BadRequest("La Boleta ya fue usada en la fecha: " + boleta.FechaUso + " en la porteria: " + boleta.Porteria);
            }
            return Ok(boleta);
        }

        [HttpPut]
        public async Task<ActionResult> Put(BoletaDTO boletaDTO)
        {
            var boleta = await _context.Boletas.FirstOrDefaultAsync(x => x.Id == boletaDTO.Id);

            if(boleta == null)
            {
                return BadRequest("Error");
            }

            return Ok();
        }
    }
}
