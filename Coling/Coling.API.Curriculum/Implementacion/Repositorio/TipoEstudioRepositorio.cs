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
    public class TipoEstudioRepositorio : ITipoEstudioRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public TipoEstudioRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "TipoEstudio";
        }
        public async Task<bool> Create(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(tipoEstudio);
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
        public async Task<TipoEstudio> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'TipoEstudio' an RowKey eq '{id}'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                return tipoEstudio;
            }
            return null;
        }

        public async Task<List<TipoEstudio>> GetAll()
        {
            List<TipoEstudio> lista = new List<TipoEstudio>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'TipoEstudio'";
            await foreach (TipoEstudio tipoEstudio in tablaCliente.QueryAsync<TipoEstudio>(filter: filtro))
            {
                lista.Add(tipoEstudio);
            }
            return lista;
        }

        public async Task<bool> Update(TipoEstudio tipoEstudio)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(tipoEstudio, tipoEstudio.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
