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
    public class TipoSocialLogic : ITipoSocialLogic
    {
        private readonly Contexto contexto;

        public TipoSocialLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarTipoSocial(TipoSocial tipoSocial)
        {
            contexto.TipoSociales.Add(tipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarTipoSocial(TipoSocial tipoSocial, int id)
        {
            var ts = await contexto.TipoSociales.FindAsync(id);
            if (ts == null)
            {
                return false;
            }
            contexto.Entry(ts).CurrentValues.SetValues(tipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTipoSocial(int id)
        {
            var tipoSocial = await contexto.TipoSociales.FindAsync(id);
            if (tipoSocial == null)
            {
                return false;
            }
            contexto.TipoSociales.Remove(tipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<TipoSocial>> ListarTiposSociales()
        {
            return await contexto.TipoSociales.ToListAsync();
        }

        public async Task<TipoSocial> ObtenerTipoSocialById(int id)
        {
            return await contexto.TipoSociales.FindAsync(id);
        }

        public Task<List<TipoSocial>> ListarTipoSocialTodos()
        {
            throw new NotImplementedException();
        }
    }
}
