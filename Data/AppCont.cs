using Microsoft.EntityFrameworkCore;
using Prova1Bimestre.Models;

namespace Prova1Bimestre.Data
{
    public class AppCont : DbContext
    {
        public AppCont(DbContextOptions<AppCont> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
