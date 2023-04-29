using ParcialEstadio.Shared.Entities;
using Sales.API.Data;
using System;
using System.Data;

namespace ParcialEstadio.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckBoletasAsync();
            await CheckPorteriaAsync();
        }

        private async Task CheckBoletasAsync()
        {

            for(int i = 0; i<50000; i++) {

                _context.Boletas.Add(new Boleta { Usada = false });

            }
            await _context.SaveChangesAsync();

        }

        private async Task CheckPorteriaAsync()
        {
            if (!_context.Porterias.Any())
            {
                _context.Porterias.Add(new Porteria { Name = "Sur" });
                _context.Porterias.Add(new Porteria { Name = "Norte" });
                _context.Porterias.Add(new Porteria { Name = "Oriente" });
                _context.Porterias.Add(new Porteria { Name = "Occidente" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
