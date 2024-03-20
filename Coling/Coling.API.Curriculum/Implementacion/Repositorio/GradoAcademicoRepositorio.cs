using Azure.Data.Tables;
using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Coling.Shared.Interfaz;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Curriculum.Implementacion.Repositorio
{
    public class GradoAcademicoRepositorio : IGradoAcademicoRepositorio
    {
        private readonly string? cadenaConexion;
        private readonly string tablaNombre;
        private readonly IConfiguration configuration;
        public GradoAcademicoRepositorio(IConfiguration conf)
        {
            configuration = conf;
            cadenaConexion = configuration.GetSection("cadenaconexion").Value;
            tablaNombre = "GradoAcademico";
        }
        public async Task<bool> Create(GradoAcademico gradoAcademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpsertEntityAsync(gradoAcademico);
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
        public async Task<GradoAcademico> Get(string id)
        {
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'GradoAcademico' an RowKey eq '{id}'";
            await foreach (GradoAcademico grado in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                return grado;
            }
            return null;
        }

        public async Task<List<GradoAcademico>> GetAll()
        {
            List<GradoAcademico> lista = new List<GradoAcademico>();
            var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
            var filtro = $"PartitionKey eq 'GradoAcademico'";
            await foreach (GradoAcademico grado in tablaCliente.QueryAsync<GradoAcademico>(filter: filtro))
            {
                lista.Add(grado);
            }
            return lista;
        }

        public async Task<bool> Update(GradoAcademico gradoAcademico)
        {
            try
            {
                var tablaCliente = new TableClient(cadenaConexion, tablaNombre);
                await tablaCliente.UpdateEntityAsync(gradoAcademico, gradoAcademico.ETag);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
