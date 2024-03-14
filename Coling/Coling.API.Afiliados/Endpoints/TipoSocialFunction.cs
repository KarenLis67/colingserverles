using Coling.API.Afiliados.Contratos;
using Coling.Shared;
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
    public class TipoSocialFunction
    {
        private readonly ILogger<TipoSocialFunction> _logger;
        private readonly ITipoSocialLogic tipoSocialLogic;

        public TipoSocialFunction(ILogger<TipoSocialFunction> logger, ITipoSocialLogic _tipoSocialLogic)
        {
            _logger = logger;
            tipoSocialLogic = _tipoSocialLogic;
        }

        [Function("ListarTiposSocialesFunction")]
        public async Task<HttpResponseData> ListarTiposSociales([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTiposSociales")] HttpRequestData req)
        {
            _logger.LogInformation("ListarTiposSociales");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(await tipoSocialLogic.ListarTipoSocialTodos());
            return res;
        }

        [Function("InsertarTipoSocialFunction")]
        public async Task<HttpResponseData> InsertarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarTipoSocial");
            var tipoSocial = await req.ReadFromJsonAsync<TipoSocial>();
            bool success = await tipoSocialLogic.InsertarTipoSocial(tipoSocial);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarTipoSocialFunction")]
        public async Task<HttpResponseData> ModificarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarTipoSocial");
            var tipoSocial = await req.ReadFromJsonAsync<TipoSocial>();
            bool success = await tipoSocialLogic.ModificarTipoSocial(tipoSocial, tipoSocial.Id);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarTipoSocialFunction")]
        public async Task<HttpResponseData> EliminarTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTipoSocial/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarTipoSocial");
            int tipoSocialId = int.Parse(id);
            bool success = await tipoSocialLogic.EliminarTipoSocial(tipoSocialId);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerTipoSocialByIdFunction")]
        public async Task<HttpResponseData> ObtenerTipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTipoSocial/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerTipoSocialById");
            int tipoSocialId = int.Parse(id);
            var tipoSocial = await tipoSocialLogic.ObtenerTipoSocialById(tipoSocialId);
            var res = req.CreateResponse(tipoSocial != null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            await res.WriteAsJsonAsync(tipoSocial);
            return res;
        }


    }
}
