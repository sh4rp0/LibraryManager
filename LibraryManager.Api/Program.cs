
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
                    .AddInfrastructure();


                builder.Services.AddControllers();
            }

            var app = builder.Build();
            {
                app.UseHttpsRedirection();
                app.MapControllers();

                app.Run();
            }
        }
    }
}
