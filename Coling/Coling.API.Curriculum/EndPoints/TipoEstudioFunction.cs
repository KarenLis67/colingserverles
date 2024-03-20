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
    public class TipoEstudioFunction
    {
        private readonly ILogger<TipoEstudioFunction> _logger;
        private readonly ITipoEstudioRepositorio repos;

        public TipoEstudioFunction(ILogger<TipoEstudioFunction> logger, ITipoEstudioRepositorio repos)
        {
            _logger = logger;
            this.repos = repos;
        }

        [Function("InsertarTipoEstudio")]
        public async Task<HttpResponseData> InsertarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                var registro = await req.ReadFromJsonAsync<TipoEstudio>() ?? throw new Exception("Debe ingresar un tipo de estudio con todos sus datos");
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

        [Function("ListarTipoEstudio")]
        [OpenApiOperation("Listar spec", "ListarTipoEstudio", Description = "Sirve para listar todos los tipos de estudio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<TipoEstudio>),
            Description = "Mostrará una lista de tipos de estudio")
        ]
        public async Task<HttpResponseData> ListarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
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

        [Function("ListaNombresTipoEstudio")]
        [OpenApiOperation("Listar spec", "ListaNombresTipoEstudio", Description = "Sirve para listar todos los tipos de estudio")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json",
            bodyType: typeof(List<string>),
            Description = "Mostrará una lista de tipos de estudio")]
        public async Task<HttpResponseData> ListaNombresTipoEstudio([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            HttpResponseData respuesta;
            try
            {
                List<string> cadenas = new List<string>();
                cadenas.Add("Tipo de Estudio A");
                cadenas.Add("Tipo de Estudio B");
                cadenas.Add("Tipo de Estudio C");
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
