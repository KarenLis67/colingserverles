using Coling.API.Curriculum.Contratos.Repositorio;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class TipoInstitucionFunction
    {
        private readonly ILogger<TipoInstitucionFunction> _logger;
        private readonly ITipoInstitucionRepositorio repos;

        public TipoInstitucionFunction(ILogger<TipoInstitucionFunction> logger, ITipoInstitucionRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarTipoInstitucion")]
        public async Task<HttpResponseData> InsertarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<TipoInstitucion>() ?? throw new Exception("Debe ingresar un tipo de institución con todos sus datos");
                registro.RowKey = Guid.NewGuid().ToString();
                registro.Timestamp = DateTime.UtcNow;
                bool sw = await repos.Create(registro);
                if (sw)
                {
                    respuesta = req.CreateResponse(HttpStatusCode.OK);
                    return respuesta;
                }
                else
                {
                    respuesta = req.CreateResponse(HttpStatusCode.BadRequest);
                    return respuesta;
                }
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ListarTipoInstitucion")]
        [OpenApiOperation("Listar spec", "ListarTipoInstitucion", Description = "Sirve para listar todos los tipos de institución")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<TipoInstitucion>),
            Description = "Mostrará una lista de tipos de institución")
        ]
        public async Task<HttpResponseData> ListarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var lista = repos.GetAll();
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(lista.Result);
                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }

        [Function("ListaNombresTipoInstitucion")]
        [OpenApiOperation("Listar spec", "ListaNombresTipoInstitucion", Description = "Sirve para listar todos los tipos de institución")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<string>),
            Description = "Mostrará una lista de tipos de institución")]
        public async Task<HttpResponseData> ListaNombresTipoInstitucion([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                List<string> cadenas = new List<string>();
                cadenas.Add("Tipo de Institución A");
                cadenas.Add("Tipo de Institución B");
                cadenas.Add("Tipo de Institución C");
                respuesta = req.CreateResponse(HttpStatusCode.OK);
                await respuesta.WriteAsJsonAsync(cadenas);

                return respuesta;
            }
            catch (Exception)
            {
                respuesta = req.CreateResponse(HttpStatusCode.InternalServerError);
                return respuesta;
            }
        }
    }

}
