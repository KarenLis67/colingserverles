using Coling.API.Curriculum.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Contratos.Repositorio
{
    public interface ITipoEstudioRepositorio
    {
        public Task<bool> Create(TipoEstudio tipoEstudio);
        public Task<bool> Update(TipoEstudio tipoEstudio);
        public Task<bool> Delete(string partitionKey, string rowkey);
        public Task<List<TipoEstudio>> GetAll();
        public Task<TipoEstudio> Get(string id);
    }
}
