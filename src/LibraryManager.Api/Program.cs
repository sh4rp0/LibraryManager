using LibraryManager.Application;
using LibraryManager.Infrastructure;

namespace LibraryManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services
                    .AddPresentation()
                    .AddApplication(builder.Configuration)
                    .AddInfrastructure(builder.Configuration);
            }

            var app = builder.Build();
            {
                app.UseExceptionHandler("/error");

                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
        }
    }
}
