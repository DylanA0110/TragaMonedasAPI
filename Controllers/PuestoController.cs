using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestoController : ControllerBase
    {
        private readonly string _connectionString;

        public PuestoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todos los puestos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puesto>>> ObtenerTodosLosPuestos()
        {
            var puestos = new List<Puesto>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Puestos, Rol, Descripcion FROM Puesto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var puesto = new Puesto
                        {
                            ID_Puesto = reader.GetInt32(0),  // Ajusta el nombre de columna
                            Rol = reader.GetString(1),       // Agrega el campo Rol
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                        puestos.Add(puesto);
                    }
                }
            }

            return Ok(puestos);
        }

        // Obtener un puesto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Puesto>> ObtenerPuestoPorId(int id)
        {
            Puesto puesto = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Puestos, Rol, Descripcion FROM Puesto WHERE ID_Puestos = @ID_Puestos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Puestos", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        puesto = new Puesto
                        {
                            ID_Puesto = reader.GetInt32(0),
                            Rol = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }
                }
            }

            if (puesto == null)
            {
                return NotFound();
            }

            return Ok(puesto);
        }

        // Crear un puesto
        [HttpPost]
        public async Task<ActionResult> CrearPuesto([FromBody] Puesto puesto)
        {
            if (puesto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Puesto (Rol, Descripcion) VALUES (@Rol, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Rol", puesto.Rol);
                    command.Parameters.AddWithValue("@Descripcion", puesto.Descripcion ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerPuestoPorId), new { id = puesto.ID_Puesto }, puesto);
        }

        // Actualizar un puesto
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarPuesto(int id, [FromBody] Puesto puesto)
        {
            if (puesto == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Puesto SET Rol = @Rol, Descripcion = @Descripcion WHERE ID_Puestos = @ID_Puestos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Puestos", id);
                    command.Parameters.AddWithValue("@Rol", puesto.Rol);
                    command.Parameters.AddWithValue("@Descripcion", puesto.Descripcion ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }

        // Eliminar un puesto
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPuesto(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Puesto WHERE ID_Puestos = @ID_Puestos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Puestos", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        return NotFound();
                    }
                }
            }

            return NoContent();
        }
    }
}
