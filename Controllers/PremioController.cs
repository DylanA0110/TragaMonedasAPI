using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremioController : ControllerBase
    {
        private readonly string _connectionString;

        public PremioController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Premio>>> ObtenerTodosLosPremios()
        {
            var premios = new List<Premio>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Premio, ID_Cliente, ID_Maquina, Monto_Total, FechaGanadoElPremio, FechaEntregadoElPremio 
                             FROM Premios";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        premios.Add(new Premio
                        {
                            ID_Premio = reader.GetInt32(0),
                            ID_Cliente = reader.GetInt32(1),
                            ID_Maquina = reader.GetInt32(2),
                            Monto_Total = reader.GetInt32(3),
                            FechaGanadoElPremio = reader.GetDateTime(4),
                            FechaEntregadoElPremio = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        });
                    }
                }
            }

            return Ok(premios);
        }

        [HttpPost]
        public async Task<ActionResult> CrearPremio([FromBody] Premio premio)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Premios 
                             (ID_Cliente, ID_Maquina, Monto_Total, FechaGanadoElPremio, FechaEntregadoElPremio) 
                             VALUES 
                             (@ID_Cliente, @ID_Maquina, @Monto_Total, @FechaGanadoElPremio, @FechaEntregadoElPremio)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Cliente", premio.ID_Cliente);
                    command.Parameters.AddWithValue("@ID_Maquina", premio.ID_Maquina);
                    command.Parameters.AddWithValue("@Monto_Total", premio.Monto_Total);
                    command.Parameters.AddWithValue("@FechaGanadoElPremio", premio.FechaGanadoElPremio);
                    command.Parameters.AddWithValue("@FechaEntregadoElPremio", premio.FechaEntregadoElPremio ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerTodosLosPremios), new { id = premio.ID_Premio }, premio);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Premio>> ObtenerPremioPorId(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT ID_Premio, ID_Cliente, ID_Maquina, Monto_Total, FechaGanadoElPremio, FechaEntregadoElPremio 
                         FROM Premios WHERE ID_Premio = @ID_Premio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Premio", id);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        var premio = new Premio
                        {
                            ID_Premio = reader.GetInt32(0),
                            ID_Cliente = reader.GetInt32(1),
                            ID_Maquina = reader.GetInt32(2),
                            Monto_Total = reader.GetInt32(3),
                            FechaGanadoElPremio = reader.GetDateTime(4),
                            FechaEntregadoElPremio = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5)
                        };

                        return Ok(premio);
                    }

                    return NotFound(); // Premio no encontrado
                }
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarPremio(int id, [FromBody] Premio premio)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Premios 
                         SET ID_Cliente = @ID_Cliente, 
                             ID_Maquina = @ID_Maquina, 
                             Monto_Total = @Monto_Total, 
                             FechaGanadoElPremio = @FechaGanadoElPremio, 
                             FechaEntregadoElPremio = @FechaEntregadoElPremio
                         WHERE ID_Premio = @ID_Premio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Premio", id);
                    command.Parameters.AddWithValue("@ID_Cliente", premio.ID_Cliente);
                    command.Parameters.AddWithValue("@ID_Maquina", premio.ID_Maquina);
                    command.Parameters.AddWithValue("@Monto_Total", premio.Monto_Total);
                    command.Parameters.AddWithValue("@FechaGanadoElPremio", premio.FechaGanadoElPremio);
                    command.Parameters.AddWithValue("@FechaEntregadoElPremio", premio.FechaEntregadoElPremio ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return NoContent(); // Actualización exitosa
                    }

                    return NotFound(); // Premio no encontrado
                }
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPremio(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Premios WHERE ID_Premio = @ID_Premio";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Premio", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return NoContent(); // Eliminación exitosa
                    }

                    return NotFound(); // Premio no encontrado
                }
            }
        }

    }

}
