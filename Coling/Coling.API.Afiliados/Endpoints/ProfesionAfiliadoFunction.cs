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
    public class ProfesionAfiliadoFunction
    {
        private readonly ILogger<ProfesionAfiliadoFunction> _logger;
        private readonly IProfesionAfiliadoLogic profesionAfiliadoLogic;

        public ProfesionAfiliadoFunction(ILogger<ProfesionAfiliadoFunction> logger, IProfesionAfiliadoLogic _profesionAfiliadoLogic)
        {
            _logger = logger;
            profesionAfiliadoLogic = _profesionAfiliadoLogic;
        }

        [Function("ListarProfesionAfiliadoFunction")]
        public async Task<HttpResponseData> ListarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("ListarProfesionAfiliado");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(await profesionAfiliadoLogic.ListarProfesionAfiliadosTodos());
            return res;
        }

        [Function("InsertarProfesionAfiliadoFunction")]
        public async Task<HttpResponseData> InsertarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarProfesionAfiliado");
            var profesionAfiliado = await req.ReadFromJsonAsync<ProfesionAfiliado>();
            bool success = await profesionAfiliadoLogic.InsertarProfesionAfiliado(profesionAfiliado);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarProfesionAfiliadoFunction")]
        public async Task<HttpResponseData> ModificarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarProfesionAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarProfesionAfiliado");
            var profesionAfiliado = await req.ReadFromJsonAsync<ProfesionAfiliado>();
            bool success = await profesionAfiliadoLogic.ModificarProfesionAfiliado(profesionAfiliado, profesionAfiliado.Id);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarProfesionAfiliadoFunction")]
        public async Task<HttpResponseData> EliminarProfesionAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarProfesionAfiliado/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarProfesionAfiliado");
            int profesionAfiliadoId = int.Parse(id);
            bool success = await profesionAfiliadoLogic.EliminarProfesionAfiliado(profesionAfiliadoId);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerProfesionAfiliadoByIdFunction")]
        public async Task<HttpResponseData> ObtenerProfesionAfiliadoById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerProfesionAfiliado/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerProfesionAfiliadoById");
            int profesionAfiliadoId = int.Parse(id);
            var profesionAfiliado = await profesionAfiliadoLogic.ObtenerProfesionAfiliadoById(profesionAfiliadoId);
            var res = req.CreateResponse(profesionAfiliado != null ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            await res.WriteAsJsonAsync(profesionAfiliado);
            return res;
        }
    }
}
