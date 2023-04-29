using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialEstadio.API.Helpers;
using ParcialEstadio.Shared.DTOs;
using ParcialEstadio.Shared.Entities;
using Sales.API.Data;

namespace ParcialEstadio.API.Controllers
{
    [ApiController]
    [Route("/api/estadio")]
    public class BoletaController : ControllerBase
    {
        private readonly DataContext _context;

        public BoletaController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Boletas
                .AsQueryable();

            return Ok(await queryable
                .OrderBy(x => x.Id)
                .Paginate(pagination)
                .ToListAsync());
        }

        [HttpGet("totalPages")]
        public async Task<ActionResult> GetPages([FromQuery] PaginationDTO pagination)
        {
            var queryable = _context.Boletas.AsQueryable();
            double count = await queryable.CountAsync();
            double totalPages = Math.Ceiling(count / pagination.RecordsNumber);
            return Ok(totalPages);
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
        public async Task<ActionResult> Put(Boleta boletaDatos)
        {

            var boleta = await _context.Boletas.FirstOrDefaultAsync(x => x.Id == boletaDatos.Id);
            if (boleta == null)
            {
                return BadRequest("Error al actualizar");
            }
            boleta.Id = boletaDatos.Id;
            boleta.FechaUso = DateTime.Now;
            boleta.Usada = true;
            boleta.PorteriaId = boletaDatos.PorteriaId;

            _context.Update(boleta);
            await _context.SaveChangesAsync();
            return Ok(boleta);
        }

        [HttpGet("info/{id:int}")]
        public async Task<ActionResult> GetBoleta(int id)
        {
            var boleta = await _context.Boletas
                             .FirstOrDefaultAsync(x => x.Id == id); 
            if (boleta!.Usada == true)
            {
                return BadRequest("Fue usada en la fecha: " + boleta.FechaUso + " en la porteria: " + boleta.Porteria);
            }

            return Ok(boleta);
        }
    }
}