using LibrarySystem.DataAccessLayer.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibrarySystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Context 
            builder.Services.AddDbContext<LibraryContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection"));
            });

            // Add services to the container.

            builder.Services.AddControllers();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
