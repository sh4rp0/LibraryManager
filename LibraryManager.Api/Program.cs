
using LibraryManager.Api.Middleware;
using LibraryManager.Application.Services;
using LibraryManager.Infrastructure.Services;

namespace LibraryManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);

                //builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
                builder.Services.AddProblemDetails();
                builder.Services.AddControllers();
            }

            var app = builder.Build();
            {
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.MapControllers();

                app.Run();
            }
        }
    }
}
