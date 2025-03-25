using ConsultaFunction;
using Microsoft.EntityFrameworkCore;
using PersistenciaService.Data;
using PersistenciaService.Data.Models;

namespace ConsultaFunction.Tests
{
    public class ConsultarContatosFunctionTests
    {
        [Fact]
        public async Task BuscarContatos_DeveRetornarContatosFiltrados()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            using var context = new AppDbContext(options);
            context.CONTATO_CTT.Add(new ContatoModel
            {
                CTT_ID = 1,
                CTT_NOME = "Daniel",
                CTT_EMAIL = "daniel@teste.com",
                CTT_NUMERO = "912345678",
                CTT_DDD = 11,
                CTT_DTCRIACAO = DateTime.Now
            });
            await context.SaveChangesAsync();

            var function = new ConsultarContatos(context);

            var resultado = await function.BuscarContatos("daniel");

            Assert.Single(resultado);
        }
    }
}
