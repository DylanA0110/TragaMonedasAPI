using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        private readonly string _connectionString;

        public MantenimientoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mantenimiento>>> ObtenerTodosLosMantenimientos()
        {
            var mantenimientos = new List<Mantenimiento>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Mantenimiento, ID_Maquina, ID_Empleado, ID_Tipo_Mantenimiento, 
                                    Fecha_Mantenimiento, Descripcion 
                             FROM Mantenimientos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        mantenimientos.Add(new Mantenimiento
                        {
                            ID_Mantenimiento = reader.GetInt32(0),
                            ID_Maquina = reader.GetInt32(1),
                            ID_Empleado = reader.GetInt32(2),
                            ID_Tipo_Mantenimiento = reader.GetInt32(3),
                            Fecha_Mantenimiento = reader.GetDateTime(4),
                            Descripcion = reader.GetString(5)
                        });
                    }
                }
            }

            return Ok(mantenimientos);
        }

        [HttpPost]
        public async Task<ActionResult> CrearMantenimiento([FromBody] Mantenimiento mantenimiento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Mantenimientos 
                             (ID_Maquina, ID_Empleado, ID_Tipo_Mantenimiento, Fecha_Mantenimiento, Descripcion) 
                             VALUES 
                             (@ID_Maquina, @ID_Empleado, @ID_Tipo_Mantenimiento, @Fecha_Mantenimiento, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Maquina", mantenimiento.ID_Maquina);
                    command.Parameters.AddWithValue("@ID_Empleado", mantenimiento.ID_Empleado);
                    command.Parameters.AddWithValue("@ID_Tipo_Mantenimiento", mantenimiento.ID_Tipo_Mantenimiento);
                    command.Parameters.AddWithValue("@Fecha_Mantenimiento", mantenimiento.Fecha_Mantenimiento);
                    command.Parameters.AddWithValue("@Descripcion", mantenimiento.Descripcion);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerTodosLosMantenimientos), new { id = mantenimiento.ID_Mantenimiento }, mantenimiento);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mantenimiento>> ObtenerMantenimientoPorId(int id)
        {
            Mantenimiento mantenimiento = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Mantenimiento, ID_Maquina, ID_Empleado, ID_Tipo_Mantenimiento, Fecha_Mantenimiento, Descripcion 
                         FROM Mantenimientos 
                         WHERE ID_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        mantenimiento = new Mantenimiento
                        {
                            ID_Mantenimiento = reader.GetInt32(0),
                            ID_Maquina = reader.GetInt32(1),
                            ID_Empleado = reader.GetInt32(2),
                            ID_Tipo_Mantenimiento = reader.GetInt32(3),
                            Fecha_Mantenimiento = reader.GetDateTime(4),
                            Descripcion = reader.GetString(5)
                        };
                    }
                }
            }

            if (mantenimiento == null) return NotFound("Mantenimiento no encontrado.");
            return Ok(mantenimiento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMantenimiento(int id, [FromBody] Mantenimiento mantenimiento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Mantenimientos 
                         SET ID_Maquina = @ID_Maquina, ID_Empleado = @ID_Empleado, ID_Tipo_Mantenimiento = @ID_Tipo_Mantenimiento, 
                             Fecha_Mantenimiento = @Fecha, Descripcion = @Descripcion 
                         WHERE ID_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@ID_Maquina", mantenimiento.ID_Maquina);
                    command.Parameters.AddWithValue("@ID_Empleado", mantenimiento.ID_Empleado);
                    command.Parameters.AddWithValue("@ID_Tipo_Mantenimiento", mantenimiento.ID_Tipo_Mantenimiento);
                    command.Parameters.AddWithValue("@Fecha", mantenimiento.Fecha_Mantenimiento);
                    command.Parameters.AddWithValue("@Descripcion", mantenimiento.Descripcion);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Mantenimiento no encontrado.");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarMantenimiento(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Mantenimientos WHERE ID_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Mantenimiento no encontrado.");
                }
            }

            return NoContent();
        }

    }

}
