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
    public class ProfesionRepositorio : IProfesionRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public ProfesionRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "Profesion";
        }
        public async Task<bool> Create(Profesion profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(profesion);
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
        public async Task<Profesion> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Profesion' an RowKey eq '{id}'";
            await foreach (Profesion profesion in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                return profesion;
            }
            return null;
        }

        public async Task<List<Profesion>> GetAll()
        {
            List<Profesion> lista = new List<Profesion>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'Profesion'";
            await foreach (Profesion profesion in tablaCliente.QueryAsync<Profesion>(filter: filtro))
            {
                lista.Add(profesion);
            }
            return lista;
        }

        public async Task<bool> Update(Profesion profesion)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(profesion, profesion.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
