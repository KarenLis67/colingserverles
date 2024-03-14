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
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly IAfiliadoLogic afiliadoLogic;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadoLogic _afiliadoLogic)
        {
            _logger = logger;
            afiliadoLogic = _afiliadoLogic;
        }

        [Function("ListarAfiliadosFunction")]
        public async Task<HttpResponseData> ListarAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliados")] HttpRequestData req)
        {
            _logger.LogInformation("ListarAfiliados");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(await afiliadoLogic.ListarAfiliadoTodos());
            return res;
        }

        [Function("InsertarAfiliadoFunction")]
        public async Task<HttpResponseData> InsertarAfiliado([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarAfiliado");
            var afiliado = await req.ReadFromJsonAsync<Afiliado>();
            bool success = await afiliadoLogic.InsertarAfiliado(afiliado);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarAfiliadoFunction")]
        public async Task<HttpResponseData> ModificarAfiliado([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarAfiliado")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarAfiliado");
            var afiliado = await req.ReadFromJsonAsync<Afiliado>();
            bool success = await afiliadoLogic.ModificarAfiliado(afiliado, afiliado.Id);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("EliminarAfiliadoFunction")]
        public async Task<HttpResponseData> EliminarAfiliado([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarAfiliado/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("EliminarAfiliado");
            int afiliadoId = int.Parse(id);
            bool success = await afiliadoLogic.EliminarAfiliado(afiliadoId);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerAfiliadoFunction")]
        public async Task<HttpResponseData> ObtenerAfiliado([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerAfiliado/{id}")] HttpRequestData req, string id)
        {
            _logger.LogInformation("ObtenerAfiliado");
            var res = req.CreateResponse(HttpStatusCode.OK);
            int afiliadoId = int.Parse(id);
            var afiliado = await afiliadoLogic.ObtenerAfiliadoById(afiliadoId);
            await res.WriteAsJsonAsync(afiliado);
            return res;
        }
    }

}
