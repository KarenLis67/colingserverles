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
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;
        private readonly ITelefonoLogic telefonoLogic;

        public TelefonoFunction(ILogger<TelefonoFunction> logger, ITelefonoLogic _telefonoLogic)
        {
            _logger = logger;
            telefonoLogic = _telefonoLogic;
        }

        [Function("ListarTelefonosFunction")]
        public async Task<HttpResponseData> ListarTelefonos([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTelefonos")] HttpRequestData req)
        {
            _logger.LogInformation("ListarTelefonos");
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(await telefonoLogic.ListarTelefonosTodos());
            return response;
        }

        [Function("InsertarTelefonoFunction")]
        public async Task<HttpResponseData> InsertarTelefono([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTelefono")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarTelefono");
            var telefono = await req.ReadFromJsonAsync<Telefono>();
            bool success = await telefonoLogic.InsertarTelefono(telefono);
            if (success)
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarTelefonoFunction")]
        public async Task<HttpResponseData> ModificarTelefono([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarTelefono")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarTelefono");
            var telefono = await req.ReadFromJsonAsync<Telefono>();
            bool success = await telefonoLogic.ModificarTelefono(telefono, telefono.Id);
            if (success)
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarTelefonoFunction")]
        public async Task<HttpResponseData> EliminarTelefono([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTelefono/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarTelefono");
            int telefonoId = int.Parse(id);
            bool success = await telefonoLogic.EliminarTelefono(telefonoId);
            if (success)
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                return response;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerTelefonoByIdFunction")]
        public async Task<HttpResponseData> ObtenerTelefonoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTelefono/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerTelefonoById");
            int telefonoId = int.Parse(id);
            var telefono = await telefonoLogic.ObtenerTelefonoById(telefonoId);
            var response = req.CreateResponse(telefono != null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            await response.WriteAsJsonAsync(telefono);
            return response;
        }
    }
}
