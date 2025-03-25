using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using PersistenciaService.Data;
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

        [Function("ConsultarContatos")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cadastro/api/Contatos")] HttpRequestData req)
        {
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string filtro = query.Get("filtro") ?? "";

            var contatos = await BuscarContatos(filtro);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(contatos);
            return response;
        }

        // Método testável isoladamente
        public async Task<List<object>> BuscarContatos(string filtro)
        {
            return await _context.CONTATO_CTT
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
                .Cast<object>()
                .ToListAsync();
        }
    }
}
