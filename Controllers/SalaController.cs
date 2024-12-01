using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;
using System.Data;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalasController : ControllerBase
    {
        private readonly string _connectionString;

        public SalasController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todas las salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> ObtenerTodasLasSalas()
        {
            var salas = new List<Sala>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ID_Sala, id_Departamento, Dirección FROM Salas";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var sala = new Sala
                            {
                                ID_Sala = reader.GetInt32(reader.GetOrdinal("ID_Sala")),
                                Id_Departamento = reader.GetInt32(reader.GetOrdinal("id_Departamento")),
                                Direccion = reader.GetString(reader.GetOrdinal("Dirección"))
                            };
                            salas.Add(sala);
                        }
                    }
                }

                return Ok(salas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las salas: {ex.Message}");
            }
        }

        // Obtener una sala por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> ObtenerSalaPorId(int id)
        {
            Sala sala = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ID_Sala, id_Departamento, Dirección FROM Salas WHERE ID_Sala = @ID_Sala";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID_Sala", SqlDbType.Int).Value = id;
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            sala = new Sala
                            {
                                ID_Sala = reader.GetInt32(reader.GetOrdinal("ID_Sala")),
                                Id_Departamento = reader.GetInt32(reader.GetOrdinal("id_Departamento")),
                                Direccion = reader.GetString(reader.GetOrdinal("Dirección"))
                            };
                        }
                    }
                }

                if (sala == null)
                {
                    return NotFound();
                }

                return Ok(sala);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la sala: {ex.Message}");
            }
        }

        // Obtener todas las salas por Departamento
        [HttpGet("departamento/{id}")]
        public async Task<ActionResult<IEnumerable<Sala>>> ObtenerSalasPorDepartamento(int id)
        {
            var salas = new List<Sala>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT ID_Sala, id_Departamento, Dirección FROM Salas WHERE id_Departamento = @id_Departamento";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@id_Departamento", SqlDbType.Int).Value = id;
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var sala = new Sala
                            {
                                ID_Sala = reader.GetInt32(reader.GetOrdinal("ID_Sala")),
                                Id_Departamento = reader.GetInt32(reader.GetOrdinal("id_Departamento")),
                                Direccion = reader.GetString(reader.GetOrdinal("Dirección"))
                            };
                            salas.Add(sala);
                        }
                    }
                }

                if (salas.Count == 0)
                {
                    return NotFound("No se encontraron salas para el departamento especificado.");
                }

                return Ok(salas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las salas por departamento: {ex.Message}");
            }
        }

        // Crear una sala
        [HttpPost]
        public async Task<ActionResult> CrearSala([FromBody] Sala sala)
        {
            if (sala == null || string.IsNullOrEmpty(sala.Direccion) || sala.Id_Departamento <= 0)
            {
                return BadRequest("Los datos proporcionados son inválidos.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Salas (id_Departamento, Dirección) VALUES (@id_Departamento, @Direccion)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@id_Departamento", SqlDbType.Int).Value = sala.Id_Departamento;
                        command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = sala.Direccion;

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return CreatedAtAction(nameof(ObtenerSalaPorId), new { id = sala.ID_Sala }, sala);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la sala: {ex.Message}");
            }
        }

        // Actualizar una sala
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarSala(int id, [FromBody] Sala sala)
        {
            if (sala == null || string.IsNullOrEmpty(sala.Direccion) || sala.Id_Departamento <= 0)
            {
                return BadRequest("Los datos proporcionados son inválidos.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Salas SET id_Departamento = @id_Departamento, Dirección = @Direccion WHERE ID_Sala = @ID_Sala";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID_Sala", SqlDbType.Int).Value = id;
                        command.Parameters.Add("@id_Departamento", SqlDbType.Int).Value = sala.Id_Departamento;
                        command.Parameters.Add("@Direccion", SqlDbType.NVarChar).Value = sala.Direccion;

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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la sala: {ex.Message}");
            }
        }

        // Eliminar una sala
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarSala(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "DELETE FROM Salas WHERE ID_Sala = @ID_Sala";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID_Sala", SqlDbType.Int).Value = id;

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
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la sala: {ex.Message}");
            }
        }
    }
}

