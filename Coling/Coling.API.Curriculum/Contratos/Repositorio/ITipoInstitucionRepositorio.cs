using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface ITipoInstitucionRepositorio
    {
        public Task<bool> Create(TipoInstitucion tipoInstitucion);
        public Task<bool> Update(TipoInstitucion tipoInstitucion);
        public Task<bool> Delete(string partitionKey, string rowkey);
        public Task<List<TipoInstitucion>> GetAll();
        public Task<TipoInstitucion> Get(string id);
    }
}
