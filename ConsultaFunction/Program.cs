using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PersistenciaService.Data;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION") ??
                                 "Server=localhost\\SQLEXPRESS;User Id=sa;Password=senha123;Database=TechChallenge;TrustServerCertificate=True;"));
    })
    .Build();

host.Run();
