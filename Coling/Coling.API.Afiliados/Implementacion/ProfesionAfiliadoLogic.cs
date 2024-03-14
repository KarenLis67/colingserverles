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
    public class ProfesionAfiliadoLogic : IProfesionAfiliadoLogic
    {
        private readonly Contexto contexto;

        public ProfesionAfiliadoLogic(Contexto _contexto)
        {
            contexto = _contexto;
        }

        public async Task<bool> InsertarProfesionAfiliado(ProfesionAfiliado profesionAfiliado)
        {
            contexto.ProfesionAfiliados.Add(profesionAfiliado);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ModificarProfesionAfiliado(ProfesionAfiliado profesionAfiliado, int id)
        {
            var pafiliado = await contexto.ProfesionAfiliados.FindAsync(id);
            if (pafiliado == null)
            {
                return false;
            }
            contexto.Entry(pafiliado).CurrentValues.SetValues(profesionAfiliado);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProfesionAfiliado(int id)
        {
            var profesionAfiliado = await contexto.ProfesionAfiliados.FindAsync(id);
            if (profesionAfiliado == null)
            {
                return false;
            }
            contexto.ProfesionAfiliados.Remove(profesionAfiliado);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProfesionAfiliado>> ListarProfesionesAfiliados()
        {
            return await contexto.ProfesionAfiliados.ToListAsync();
        }

        public async Task<ProfesionAfiliado> ObtenerProfesionAfiliadoById(int id)
        {
            return await contexto.ProfesionAfiliados.FindAsync(id);
        }

        public Task<List<ProfesionAfiliado>> ListarProfesionAfiliadosTodos()
        {
            throw new NotImplementedException();
        }
    }
}
