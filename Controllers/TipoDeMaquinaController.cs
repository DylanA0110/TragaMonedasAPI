using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDeMaquinasController : ControllerBase
    {
        private readonly string _connectionString;

        public TipoDeMaquinasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeMaquina>>> ObtenerTodosLosTiposDeMaquinas()
        {
            var tipos = new List<TipoDeMaquina>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Tipo_Maquina, Nombre_Tipo, Descripcion FROM Tipo_de_maquinas";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        tipos.Add(new TipoDeMaquina
                        {
                            ID_Tipo_Maquina = reader.GetInt32(0),
                            Nombre_Tipo = reader.GetString(1),
                            Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                        });
                    }
                }
            }

            return Ok(tipos);
        }

        [HttpPost]
        public async Task<ActionResult> CrearTipoDeMaquina([FromBody] TipoDeMaquina tipo)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Tipo_de_maquinas (Nombre_Tipo, Descripcion) VALUES (@Nombre_Tipo, @Descripcion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre_Tipo", tipo.Nombre_Tipo);
                    command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerTodosLosTiposDeMaquinas), new { id = tipo.ID_Tipo_Maquina }, tipo);
        }

        // Obtener un tipo de máquina por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDeMaquina>> ObtenerTipoDeMaquinaPorId(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ID_Tipo_Maquina, Nombre_Tipo, Descripcion FROM Tipo_de_maquinas WHERE ID_Tipo_Maquina = @ID_Tipo_Maquina";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Tipo_Maquina", id);

                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            var tipo = new TipoDeMaquina
                            {
                                ID_Tipo_Maquina = reader.GetInt32(0),
                                Nombre_Tipo = reader.GetString(1),
                                Descripcion = reader.IsDBNull(2) ? null : reader.GetString(2)
                            };

                            return Ok(tipo);
                        }
                        else
                        {
                            return NotFound($"No se encontró el tipo de máquina con el ID {id}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al obtener el tipo de máquina: {ex.Message}");
            }
        }

        // Actualizar un tipo de máquina por ID
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarTipoDeMaquina(int id, [FromBody] TipoDeMaquina tipo)
        {
            if (tipo == null || tipo.ID_Tipo_Maquina != id)
            {
                return BadRequest("Los datos del tipo de máquina son inválidos o el ID no coincide.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"UPDATE Tipo_de_maquinas 
                             SET Nombre_Tipo = @Nombre_Tipo, 
                                 Descripcion = @Descripcion
                             WHERE ID_Tipo_Maquina = @ID_Tipo_Maquina";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nombre_Tipo", tipo.Nombre_Tipo);
                        command.Parameters.AddWithValue("@Descripcion", tipo.Descripcion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ID_Tipo_Maquina", id);

                        await connection.OpenAsync();
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected == 0)
                        {
                            return NotFound($"No se encontró el tipo de máquina con el ID {id}.");
                        }
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al actualizar el tipo de máquina: {ex.Message}");
            }
        }

    }


}
