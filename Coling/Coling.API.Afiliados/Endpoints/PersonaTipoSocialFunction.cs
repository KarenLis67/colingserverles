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
    public class PersonaTipoSocialFunction
    {
        private readonly ILogger<PersonaTipoSocialFunction> _logger;
        private readonly IPersonaTipoSocialLogic personaTipoSocialLogic;

        public PersonaTipoSocialFunction(ILogger<PersonaTipoSocialFunction> logger, IPersonaTipoSocialLogic _personaTipoSocialLogic)
        {
            _logger = logger;
            personaTipoSocialLogic = _personaTipoSocialLogic;
        }

        [Function("ListarPersonaTipoSocialFunction")]
        public async Task<HttpResponseData> ListarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonaTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("ListarPersonaTipoSocial");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(await personaTipoSocialLogic.ListarPersonasTipoSocialTodos());
            return res;
        }


        [Function("InsertarPersonaTipoSocialFunction")]
        public async Task<HttpResponseData> InsertarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersonaTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarPersonaTipoSocial");
            var personaTipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>();
            bool success = await personaTipoSocialLogic.InsertarPersonaTipoSocial(personaTipoSocial);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarPersonaTipoSocialFunction")]
        public async Task<HttpResponseData> ModificarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarPersonaTipoSocial")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarPersonaTipoSocial");
            var personaTipoSocial = await req.ReadFromJsonAsync<PersonaTipoSocial>();
            bool success = await personaTipoSocialLogic.ModificarPersonaTipoSocial(personaTipoSocial, personaTipoSocial.Id);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarPersonaTipoSocialFunction")]
        public async Task<HttpResponseData> EliminarPersonaTipoSocial([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarPersonaTipoSocial/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarPersonaTipoSocial");
            int personaTipoSocialId = int.Parse(id);
            bool success = await personaTipoSocialLogic.EliminarPersonaTipoSocial(personaTipoSocialId);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerPersonaTipoSocialByIdFunction")]
        public async Task<HttpResponseData> ObtenerPersonaTipoSocialById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerPersonaTipoSocial/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerPersonaTipoSocialById");
            var res = req.CreateResponse(HttpStatusCode.OK);
            int personaTipoSocialId = int.Parse(id);
            var personaTipoSocial = await personaTipoSocialLogic.ObtenerPersonaTipoSocialById(personaTipoSocialId);
            await res.WriteAsJsonAsync(personaTipoSocial);
            return res;
        }


    }
}
