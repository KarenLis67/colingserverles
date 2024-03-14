using Coling.API.Afiliados.Contratos;
using Coling.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coling.API.Afiliados.Endpoints
{
    public class DireccionFunction
    {
        private readonly ILogger<DireccionFunction> _logger;
        private readonly IDireccionLogic direccionLogic;

        public DireccionFunction(ILogger<DireccionFunction> logger, IDireccionLogic _direccionLogic)
        {
            _logger = logger;
            direccionLogic = _direccionLogic;
        }

        [Function("ListarDireccionesFunction")]
        public async Task<HttpResponseData> ListarDirecciones([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarDirecciones")] HttpRequestData req)
        {
            _logger.LogInformation("ListarDirecciones");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(await direccionLogic.ListarDireccionesTodos());
            return res;
        }

        [Function("InsertarDireccionFunction")]
        public async Task<HttpResponseData> InsertarDireccion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarDireccion")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarDireccion");
            var direccion = await req.ReadFromJsonAsync<Direccion>();
            bool success = await direccionLogic.InsertarDireccion(direccion);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarDireccionFunction")]
        public async Task<HttpResponseData> ModificarDireccion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarDireccion")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarDireccion");
            var direccion = await req.ReadFromJsonAsync<Direccion>();
            bool success = await direccionLogic.ModificarDireccion(direccion, direccion.Id);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarDireccionFunction")]
        public async Task<HttpResponseData> EliminarDireccion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarDireccion/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarDireccion");
            int direccionId = int.Parse(id);
            bool success = await direccionLogic.EliminarDireccion(direccionId);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerDireccionFunction")]
        public async Task<HttpResponseData> ObtenerDireccion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerDireccion/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerDireccion");
            var res = req.CreateResponse(HttpStatusCode.OK);
            int direccionId = int.Parse(id);
            var direccion = await direccionLogic.ObtenerDireccionById(direccionId);
            await res.WriteAsJsonAsync(direccion);
            return res;
        }
    }
}
