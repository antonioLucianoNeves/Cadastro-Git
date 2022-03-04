using CadastroDeClientes.Data;
using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeClientes.Services
{
    public class RecordService
    {
        private readonly BancoContext _contexto;

        public RecordService(BancoContext contexto)
        {
            _contexto = contexto;
        }
        public async Task<List<ClientesModel>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _contexto.Clientes select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.DataNascimento >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.DataNascimento <= maxDate.Value);
            }
            return await result
                .Include(x => x.DataNascimento)
                .Include(x => x.DataNascimento.Value)
                .OrderByDescending(x => x.DataNascimento)
                .ToListAsync();
        }
    }
}
