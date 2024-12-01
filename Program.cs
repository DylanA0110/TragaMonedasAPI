using Microsoft.EntityFrameworkCore;

namespace PrototipoAPIBaseDeDatos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Leer la cadena de conexi�n desde appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registrar los servicios necesarios para la API
            builder.Services.AddControllers(); // <-- Agregar los controladores

            // Si vas a usar Entity Framework, puedes agregar el DbContext aqu�
            // builder.Services.AddDbContext<YourDbContext>(options => options.UseSqlServer(connectionString));

            // Agregar la configuraci�n de Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configurar el pipeline de solicitud
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Mapear los controladores
            app.MapControllers(); // <-- Esto mapea los controladores

            app.Run();
        }
    }
}
