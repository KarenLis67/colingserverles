using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface IGradoAcademicoRepositorio
    {
        public Task<bool> Create(GradoAcademico gradoAcademico);
        public Task<bool> Update(GradoAcademico gradoAcademico);
        public Task<bool> Delete(string partitionKey, string rowkey);
        public Task<List<GradoAcademico>> GetAll();
        public Task<GradoAcademico> Get(string id);
    }
}
