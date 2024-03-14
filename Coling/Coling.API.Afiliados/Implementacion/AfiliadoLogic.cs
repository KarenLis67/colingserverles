using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Implementacion
{
    public class AfiliadoLogic : IAfiliadoLogic
    {
        private readonly Contexto contexto;

        public AfiliadoLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }
        public async Task<bool> EliminarAfiliado(int id)
        {
            var afiliado = await contexto.Afiliados.FindAsync(id);

            contexto.Afiliados.Remove(afiliado);
            await contexto.SaveChangesAsync();

            return true;
        }

        public async Task<bool> InsertarAfiliado(Afiliado afiliado)
        {
            contexto.Afiliados.Add(afiliado);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Afiliado>> ListarAfiliados()
        {
            return await contexto.Afiliados.ToListAsync();
        }

        public Task<List<Persona>> ListarAfiliadoTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ModificarAfiliado(Afiliado afiliado, int id)
        {
            Afiliado afi = await contexto.Afiliados.FindAsync(id);
            if (afi == null)
            {
                return false;
            }
            contexto.Entry(afi).CurrentValues.SetValues(afiliado);
            await contexto.SaveChangesAsync();
            return true;

        }

        public async Task<Afiliado> ObtenerAfiliadoById(int id)
        {
            return await contexto.Afiliados.FindAsync(id);
        }

        Task<Persona> IAfiliadoLogic.ObtenerAfiliadoById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
