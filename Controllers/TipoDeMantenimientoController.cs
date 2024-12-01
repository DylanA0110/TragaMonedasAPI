using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeMantenimientoController : ControllerBase
    {
        private readonly string _connectionString;

        public TipoDeMantenimientoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeMantenimiento>>> ObtenerTiposDeMantenimiento()
        {
            var tipos = new List<TipoDeMantenimiento>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Tipo_Mantenimiento, Nombre_Mantenimiento, Descripcion FROM Tipo_de_Mantenimiento";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        tipos.Add(new TipoDeMantenimiento
                        {
                            ID_Tipo_Mantenimiento = reader.GetInt32(0),
                            Nombre_Mantenimiento = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return Ok(tipos);
        }

        [HttpPost]
        public async Task<ActionResult> CrearTipoDeMantenimiento([FromBody] TipoDeMantenimiento tipo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Tipo_de_Mantenimiento (Nombre_Mantenimiento, Descripcion) 
                             VALUES (@Nombre_Mantenimiento, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre_Mantenimiento", tipo.Nombre_Mantenimiento);
                    command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerTiposDeMantenimiento), new { id = tipo.ID_Tipo_Mantenimiento }, tipo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeMantenimiento>> ObtenerTipoDeMantenimientoPorId(int id)
        {
            TipoDeMantenimiento tipo = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Tipo_Mantenimiento, Nombre_Mantenimiento, Descripcion 
                         FROM Tipo_de_Mantenimiento 
                         WHERE ID_Tipo_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        tipo = new TipoDeMantenimiento
                        {
                            ID_Tipo_Mantenimiento = reader.GetInt32(0),
                            Nombre_Mantenimiento = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        };
                    }
                }
            }

            if (tipo == null) return NotFound("Tipo de mantenimiento no encontrado.");
            return Ok(tipo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarTipoDeMantenimiento(int id, [FromBody] TipoDeMantenimiento tipo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tipo_de_Mantenimiento 
                         SET Nombre_Mantenimiento = @Nombre, Descripcion = @Descripcion 
                         WHERE ID_Tipo_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Nombre", tipo.Nombre_Mantenimiento);
                    command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Tipo de mantenimiento no encontrado.");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarTipoDeMantenimiento(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Tipo_de_Mantenimiento WHERE ID_Tipo_Mantenimiento = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Tipo de mantenimiento no encontrado.");
                }
            }

            return NoContent();
        }

    }



}
