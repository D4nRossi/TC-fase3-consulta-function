using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using PersistenciaService.Data;
using PersistenciaService.Data.Models;
using System.Linq;

namespace ConsultaFunction
{
    public class ConsultarContatos
    {
        private readonly AppDbContext _context;

        public ConsultarContatos(AppDbContext context)
        {
            _context = context;
        }

        //    [Function("ConsultarContatos")]
        //    public async Task<HttpResponseData> Run(
        //[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cadastro/api/Contatos")] HttpRequestData req)

        //    {
        //        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        //        string filtro = query.Get("filtro") ?? "";

        //        var contatos = await _context.CONTATO_CTT
        //            .Where(c => c.CTT_NOME.Contains(filtro) || c.CTT_EMAIL.Contains(filtro))
        //            .Select(c => new
        //            {
        //                c.CTT_ID,
        //                c.CTT_NOME,
        //                c.CTT_EMAIL,
        //                c.CTT_NUMERO,
        //                c.CTT_DDD,
        //                c.CTT_DTCRIACAO
        //            })
        //            .ToListAsync();

        //        var response = req.CreateResponse(HttpStatusCode.OK);
        //        await response.WriteAsJsonAsync(contatos);
        //        return response;
        //    }

        [Function("ConsultarContatos")]
        public async Task<HttpResponseData> Run(
    [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cadastro/api/Contatos")] HttpRequestData req)
        {
            var response = req.CreateResponse();

            try
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                string filtro = query.Get("filtro") ?? "";

                var contatos = await _context.CONTATO_CTT
                    .Where(c => c.CTT_NOME.Contains(filtro) || c.CTT_EMAIL.Contains(filtro))
                    .Select(c => new
                    {
                        c.CTT_ID,
                        c.CTT_NOME,
                        c.CTT_EMAIL,
                        c.CTT_NUMERO,
                        c.CTT_DDD,
                        c.CTT_DTCRIACAO
                    })
                    .ToListAsync();

                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(contatos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro: {ex.Message}");
                response.StatusCode = HttpStatusCode.InternalServerError;
                await response.WriteStringAsync($"Erro interno: {ex.Message}");
            }

            return response;
        }



    }
}
