using Microsoft.EntityFrameworkCore;
using ParcialEstadio.Shared.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Sales.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Boleta> Boletas { get; set; }
        public DbSet<Porteria> Porterias { get; set; }

    }
}
