using GlobalSolutionRopz.Data;
using GlobalSolutionRopz.Repository.Interface;
using GlobalSolutionRopz.Repository;
using GlobalSolutionRopz.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GlobalSolutionRopz
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Conexão com banco Oracle FIAP
            var stringConexao = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))) (CONNECT_DATA=(SERVER=DEDICATED)(SID=ORCL)));User Id=;Password=;";
            builder.Services.AddDbContext<Context>(options => options.UseOracle(stringConexao));

            // Swagger com comentários XML
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddScoped<ConsumerRabbit>();
            builder.Services.AddScoped<PublisherRabbit>();

         





            // Serviços e repositórios
            builder.Services.AddScoped<IUsuarioRepository, RepositoryUsuario>();
            builder.Services.AddScoped<IMensagemRepository, RepositoryMensagem>();
            builder.Services.AddScoped<IAlertaRepository, RepositoryAlerta>();

            // Injetar HttpClient no WeatherService
            builder.Services.AddHttpClient<WeatherService>();

            // Adicionar controladores e endpoints
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Pipeline do aplicativo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
