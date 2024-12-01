using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;
using System.Data;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinasController : ControllerBase
    {
        private readonly string _connectionString;

        public MaquinasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todas las máquinas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquina>>> ObtenerTodasLasMaquinas()
        {
            var maquinas = new List<Maquina>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT ID_Maquina, Nombre_Maquina, ID_Tipo_Maquina, Estado, Fecha_Instalación, ID_Sala 
                                     FROM Maquinas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            maquinas.Add(new Maquina
                            {
                                ID_Maquina = reader.GetInt32(0),
                                Nombre_Maquina = reader.GetString(1),
                                ID_Tipo_Maquina = reader.GetInt32(2),
                                Estado = reader.GetString(3),
                                Fecha_Instalacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                ID_Sala = reader.GetInt32(5)
                            });
                        }
                    }
                }

                return Ok(maquinas);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener las máquinas: {ex.Message}");
            }
        }

        // Crear una nueva máquina
        [HttpPost]
        public async Task<ActionResult> CrearMaquina([FromBody] Maquina maquina)
        {
            // Validación de parámetros
            if (maquina == null)
            {
                return BadRequest("Los datos de la máquina son inválidos.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Maquinas 
                                     (Nombre_Maquina, ID_Tipo_Maquina, Estado, Fecha_Instalación, ID_Sala) 
                                     VALUES 
                                     (@Nombre_Maquina, @ID_Tipo_Maquina, @Estado, @Fecha_Instalación, @ID_Sala)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre_Maquina", maquina.Nombre_Maquina);
                        command.Parameters.AddWithValue("@ID_Tipo_Maquina", maquina.ID_Tipo_Maquina);
                        command.Parameters.AddWithValue("@Estado", maquina.Estado ?? "Activa");
                        command.Parameters.AddWithValue("@Fecha_Instalación", maquina.Fecha_Instalacion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ID_Sala", maquina.ID_Sala);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

                // Obtener el ID de la nueva máquina insertada
                return CreatedAtAction(nameof(ObtenerTodasLasMaquinas), new { id = maquina.ID_Maquina }, maquina);
            }
            catch (SqlException ex)
            {
                // Captura de errores específicos de SQL
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear la máquina: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejo general de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al crear la máquina: {ex.Message}");
            }
        }
        // Obtener una máquina por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquina>> ObtenerMaquinaPorId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"SELECT ID_Maquina, Nombre_Maquina, ID_Tipo_Maquina, Estado, Fecha_Instalación, ID_Sala 
                             FROM Maquinas 
                             WHERE ID_Maquina = @ID_Maquina";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Maquina", id);

                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            var maquina = new Maquina
                            {
                                ID_Maquina = reader.GetInt32(0),
                                Nombre_Maquina = reader.GetString(1),
                                ID_Tipo_Maquina = reader.GetInt32(2),
                                Estado = reader.GetString(3),
                                Fecha_Instalacion = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                                ID_Sala = reader.GetInt32(5)
                            };

                            return Ok(maquina);
                        }
                        else
                        {
                            return NotFound($"No se encontró la máquina con el ID {id}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener la máquina: {ex.Message}");
            }
        }

        // Actualizar una máquina por ID
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarMaquina(int id, [FromBody] Maquina maquina)
        {
            if (maquina == null || maquina.ID_Maquina != id)
            {
                return BadRequest("Los datos de la máquina son inválidos o el ID no coincide.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"UPDATE Maquinas 
                             SET Nombre_Maquina = @Nombre_Maquina, 
                                 ID_Tipo_Maquina = @ID_Tipo_Maquina, 
                                 Estado = @Estado, 
                                 Fecha_Instalación = @Fecha_Instalación, 
                                 ID_Sala = @ID_Sala
                             WHERE ID_Maquina = @ID_Maquina";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre_Maquina", maquina.Nombre_Maquina);
                        command.Parameters.AddWithValue("@ID_Tipo_Maquina", maquina.ID_Tipo_Maquina);
                        command.Parameters.AddWithValue("@Estado", maquina.Estado ?? "Activa");
                        command.Parameters.AddWithValue("@Fecha_Instalación", maquina.Fecha_Instalacion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ID_Sala", maquina.ID_Sala);
                        command.Parameters.AddWithValue("@ID_Maquina", id);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"No se encontró la máquina con el ID {id}.");
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar la máquina: {ex.Message}");
            }
        }


        // Eliminar una máquina por ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarMaquina(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"DELETE FROM Maquinas WHERE ID_Maquina = @ID_Maquina";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Maquina", id);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"No se encontró la máquina con el ID {id}.");
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al eliminar la máquina: {ex.Message}");
            }
        }
    }
}
