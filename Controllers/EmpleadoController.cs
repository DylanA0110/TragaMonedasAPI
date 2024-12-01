using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly string _connectionString;

        public EmpleadoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Obtener todos los empleados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empleado>>> ObtenerTodosLosEmpleados()
        {
            var empleados = new List<Empleado>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    SELECT e.ID_Empleados, e.Nombres, e.Apellidos, e.Turno, e.ID_Sala, e.id_Puesto
                    FROM Empleados e";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            var empleado = new Empleado
                            {
                                ID_Empleados = reader.GetInt32(reader.GetOrdinal("ID_Empleados")),
                                Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                                Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                                Turno = reader.GetString(reader.GetOrdinal("Turno")),
                                ID_Sala = reader.GetInt32(reader.GetOrdinal("ID_Sala")),
                                id_Puesto = reader.GetInt32(reader.GetOrdinal("id_Puesto"))
                            };
                            empleados.Add(empleado);
                        }
                    }
                }

                if (empleados.Count == 0)
                {
                    return NotFound("No se encontraron empleados.");
                }

                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los empleados: {ex.Message}");
            }
        }

        // Obtener empleado por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> ObtenerEmpleadoPorId(int id)
        {
            Empleado empleado = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                SELECT e.ID_Empleados, e.Nombres, e.Apellidos, e.Turno, e.ID_Sala, e.id_Puesto
                FROM Empleados e
                WHERE e.ID_Empleados = @ID_Empleados";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Empleados", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        empleado = new Empleado
                        {
                            ID_Empleados = reader.GetInt32(reader.GetOrdinal("ID_Empleados")),
                            Nombres = reader.GetString(reader.GetOrdinal("Nombres")),
                            Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                            Turno = reader.GetString(reader.GetOrdinal("Turno")),
                            ID_Sala = reader.GetInt32(reader.GetOrdinal("ID_Sala")),
                            id_Puesto = reader.GetInt32(reader.GetOrdinal("id_Puesto"))
                        };
                    }
                }
            }

            if (empleado == null)
            {
                return NotFound();
            }

            return Ok(empleado);
        }

        // Crear un empleado
        [HttpPost]
        public async Task<ActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    INSERT INTO Empleados (Nombres, Apellidos, Turno, ID_Sala, id_Puesto)
                    VALUES (@Nombres, @Apellidos, @Turno, @ID_Sala, @id_Puesto)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Turno", empleado.Turno);
                    command.Parameters.AddWithValue("@ID_Sala", empleado.ID_Sala);
                    command.Parameters.AddWithValue("@id_Puesto", empleado.id_Puesto);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerEmpleadoPorId), new { id = empleado.ID_Empleados }, empleado);
        }

        // Actualizar un empleado
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarEmpleado(int id, [FromBody] Empleado empleado)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE Empleados
                    SET Nombres = @Nombres, Apellidos = @Apellidos, Turno = @Turno, 
                        ID_Sala = @ID_Sala, id_Puesto = @id_Puesto
                    WHERE ID_Empleados = @ID_Empleados";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Empleados", id);
                    command.Parameters.AddWithValue("@Nombres", empleado.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", empleado.Apellidos);
                    command.Parameters.AddWithValue("@Turno", empleado.Turno);
                    command.Parameters.AddWithValue("@ID_Sala", empleado.ID_Sala);
                    command.Parameters.AddWithValue("@id_Puesto", empleado.id_Puesto);

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

        // Eliminar un empleado
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEmpleado(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Empleados WHERE ID_Empleados = @ID_Empleados";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Empleados", id);

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
