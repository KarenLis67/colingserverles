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
    public class PersonaTipoSocialLogic : IPersonaTipoSocialLogic
    {
        private readonly Contexto contexto;

        public PersonaTipoSocialLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial)
        {
            contexto.PersonaTipoSociales.Add(personaTipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarPersonaTipoSocial(PersonaTipoSocial personaTipoSocial, int id)
        {
            var pts = await contexto.PersonaTipoSociales.FindAsync(id);
            if (pts == null)
            {
                return false;
            }
            contexto.Entry(pts).CurrentValues.SetValues(personaTipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPersonaTipoSocial(int id)
        {
            var personaTipoSocial = await contexto.PersonaTipoSociales.FindAsync(id);
            if (personaTipoSocial == null)
            {
                return false;
            }
            contexto.PersonaTipoSociales.Remove(personaTipoSocial);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<PersonaTipoSocial>> ListarPersonasTipoSocial()
        {
            return await contexto.PersonaTipoSociales.ToListAsync();
        }

        public async Task<PersonaTipoSocial> ObtenerPersonaTipoSocialById(int id)
        {
            return await contexto.PersonaTipoSociales.FindAsync(id);
        }

        public Task<List<PersonaTipoSocial>> ListarPersonasTipoSocialTodos()
        {
            throw new NotImplementedException();
        }
    }

}
