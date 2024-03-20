using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class TipoInstitucionRepositorio : ITipoInstitucionRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public TipoInstitucionRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "TipoInstitucion";
        }
        public async Task<bool> Create(TipoInstitucion tipoInstitucion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(tipoInstitucion);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(string partitionKey, string rowkey, string etag)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.DeleteEntityAsync(partitionKey, rowkey);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Task<bool> Delete(string partitionKey, string rowkey)
        {
            throw new NotImplementedException();
        }
        public async Task<TipoInstitucion> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'TipoInstitucion' an RowKey eq '{id}'";
            await foreach (TipoInstitucion tipoInstitucion in tablaCliente.QueryAsync<TipoInstitucion>(filter: filtro))
            {
                return tipoInstitucion;
            }
            return null;
        }

        public async Task<List<TipoInstitucion>> GetAll()
        {
            List<TipoInstitucion> lista = new List<TipoInstitucion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'TipoInstitucion'";
            await foreach (TipoInstitucion tipoInstitucion in tablaCliente.QueryAsync<TipoInstitucion>(filter: filtro))
            {
                lista.Add(tipoInstitucion);
            }
            return lista;
        }

        public async Task<bool> Update(TipoInstitucion tipoInstitucion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(tipoInstitucion, tipoInstitucion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
