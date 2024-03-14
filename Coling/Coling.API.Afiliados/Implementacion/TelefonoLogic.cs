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
    public class TelefonoLogic : ITelefonoLogic
    {
        private readonly Contexto contexto;

        public TelefonoLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarTelefono(Telefono telefono)
        {
            contexto.Telefonos.Add(telefono);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarTelefono(Telefono telefono, int id)
        {
            var tel = await contexto.Telefonos.FindAsync(id);
            if (tel == null)
            {
                return false;
            }
            contexto.Entry(tel).CurrentValues.SetValues(telefono);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTelefono(int id)
        {
            var telefono = await contexto.Telefonos.FindAsync(id);
            if (telefono == null)
            {
                return false;
            }
            contexto.Telefonos.Remove(telefono);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<Telefono>> ListarTelefonos()
        {
            return await contexto.Telefonos.ToListAsync();
        }

        public async Task<Telefono> ObtenerTelefonoById(int id)
        {
            return await contexto.Telefonos.FindAsync(id);
        }

        public Task<List<Telefono>> ListarTelefonosTodos()
        {
            throw new NotImplementedException();
        }
    }
}
