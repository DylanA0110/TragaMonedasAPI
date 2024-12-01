using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly string _connectionString;
        public DepartamentoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> ObtenerTodosLosDepartamentos()
        {
            List<Departamento> departamentos = new List<Departamento>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Departamentos, Nombre_Departamento FROM Departamentos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Departamento departamento = new Departamento
                        {
                            ID_Departamentos = reader.GetInt32(0),
                            Nombre_Departamento = reader.GetString(1)
                        };
                        departamentos.Add(departamento);
                    }
                }
            }

            return Ok(departamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> ObtenerDepartamentoPorId(int id)
        {
            Departamento departamento = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Departamentos, Nombre_Departamento FROM Departamentos WHERE ID_Departamentos = @ID_Departamentos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Departamentos", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        departamento = new Departamento
                        {
                            ID_Departamentos = reader.GetInt32(0),
                            Nombre_Departamento = reader.GetString(1)
                        };
                    }
                }
            }

            if (departamento == null)
            {
                return NotFound();
            }

            return Ok(departamento);
        }

        [HttpPost]
        public async Task<ActionResult> CrearDepartamento([FromBody] Departamento departamento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Departamentos (Nombre_Departamento) VALUES (@Nombre_Departamento)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre_Departamento", departamento.Nombre_Departamento);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerDepartamentoPorId), new { id = departamento.ID_Departamentos }, departamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarDepartamento(int id, [FromBody] Departamento departamento)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Departamentos SET Nombre_Departamento = @Nombre_Departamento WHERE ID_Departamentos = @ID_Departamentos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Departamentos", id);
                    command.Parameters.AddWithValue("@Nombre_Departamento", departamento.Nombre_Departamento);

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

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarDepartamento(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Departamentos WHERE ID_Departamentos = @ID_Departamentos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Departamentos", id);

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
