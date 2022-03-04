using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ClientesModel> Clientes { get; set; }

        internal Task FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            throw new NotImplementedException();
        }
    }
}
