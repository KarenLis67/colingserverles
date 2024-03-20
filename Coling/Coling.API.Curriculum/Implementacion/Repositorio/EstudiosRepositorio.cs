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
    public class EstudiosRepositorio : IEstudiosRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public EstudiosRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Estudios";
        }
        public async Task<bool> Create(Estudios estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(estudios);
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

        public async Task<Estudios> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion' and RowKey eq '{id}'";
            await foreach (Estudios estudios in tablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                return estudios;
            }
            return null;
        }

        public async Task<List<Estudios>> GetAll()
        {
            List<Estudios> lista = new List<Estudios>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Educacion'";
            await foreach (Estudios estudios in tablaCliente.QueryAsync<Estudios>(filter: filtro))
            {
                lista.Add(estudios);
            }

            return lista;
        }

        public async Task<bool> Update(Estudios estudios)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(estudios, estudios.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
