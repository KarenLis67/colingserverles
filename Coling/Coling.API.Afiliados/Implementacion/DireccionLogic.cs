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
    public class DireccionLogic : IDireccionLogic
    {
        private readonly Contexto contexto;

        public DireccionLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarDireccion(Direccion direccion)
        {
            contexto.Direcciones.Add(direccion);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarDireccion(Direccion direccion, int id)
        {
            var dir = await contexto.Direcciones.FindAsync(id);
            if (dir == null)
            {
                return false;
            }
            contexto.Entry(dir).CurrentValues.SetValues(direccion);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarDireccion(int id)
        {
            var direccion = await contexto.Direcciones.FindAsync(id);
            contexto.Direcciones.Remove(direccion);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Direccion>> ListarDirecciones()
        {
            return await contexto.Direcciones.ToListAsync();
        }

        public async Task<Direccion> ObtenerDireccionById(int id)
        {
            return await contexto.Direcciones.FindAsync(id);
        }

        public Task<List<Direccion>> ListarDireccionesTodos()
        {
            throw new NotImplementedException();
        }
    }

}
