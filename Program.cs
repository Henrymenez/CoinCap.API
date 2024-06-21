
using CoinCap.API.Implementation;
using CoinCap.API.Infrastructure;
using CoinCap.API.Interface;
using CoinCap.API.Middlewares;

namespace CoinCap.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration Configuration = builder.Configuration;
            builder.Services.Configure<CoinCapConfig>(Configuration.GetSection("CoinCapConfig"));
            

            // Add services to the container.
            builder.Services.AddTransient<ICoinCapService, CoinCapService>();
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.ConfigureExceptionHandler();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
