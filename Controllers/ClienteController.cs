using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using SharedModels.SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly string _connectionString;
        public ClienteController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Método para obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObtenerTodosLosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Cliente, Nombres, Apellidos, Sexo, No_Cedula, Direccion, Profesion, No_Teléfono FROM Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        Cliente cliente = new Cliente
                        {
                            ID_Cliente = reader.GetInt32(0),
                            Nombres = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Sexo = reader.GetString(3), // Ahora se lee como un string (Ejemplo: "M" o "F")
                            No_Cedula = reader.GetString(4),
                            Direccion = reader.GetString(5),
                            Profesion = reader.IsDBNull(6) ? null : reader.GetString(6),
                            No_Telefono = reader.GetString(7)
                        };
                        clientes.Add(cliente);
                    }
                }
            }

            return Ok(clientes);
        }

        // Método para obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> ObtenerClientePorId(int id)
        {
            Cliente cliente = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT ID_Cliente, Nombres, Apellidos, Sexo, No_Cedula, Direccion, Profesion, No_Teléfono FROM Cliente WHERE ID_Cliente = @ID_Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Cliente", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        await reader.ReadAsync();
                        cliente = new Cliente
                        {
                            ID_Cliente = reader.GetInt32(0),
                            Nombres = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Sexo = reader.GetString(3), // Se lee como string
                            No_Cedula = reader.GetString(4),
                            Direccion = reader.GetString(5),
                            Profesion = reader.IsDBNull(6) ? null : reader.GetString(6),
                            No_Telefono = reader.GetString(7)
                        };
                    }
                }
            }

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // Método para crear un cliente
        [HttpPost]
        public async Task<ActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Cliente (Nombres, Apellidos, Sexo, No_Cedula, Direccion, Profesion, No_Teléfono) " +
                "VALUES (@Nombres, @Apellidos, @Sexo, @No_Cedula, @Direccion, @Profesion, @No_Telefono)";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Sexo", cliente.Sexo); // Se pasa el sexo como string
                    command.Parameters.AddWithValue("@No_Cedula", cliente.No_Cedula);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Profesion", cliente.Profesion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@No_Telefono", cliente.No_Telefono);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return CreatedAtAction(nameof(ObtenerClientePorId), new { id = cliente.ID_Cliente }, cliente);
        }

        // Método para actualizar un cliente
        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Cliente SET Nombres = @Nombres, Apellidos = @Apellidos, Sexo = @Sexo, " +
                               "No_Cedula = @No_Cedula, Direccion = @Direccion, Profesion = @Profesion, No_Teléfono = @No_Telefono " +
                               "WHERE ID_Cliente = @ID_Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Cliente", id);
                    command.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                    command.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                    command.Parameters.AddWithValue("@Sexo", cliente.Sexo); // Se pasa el sexo como string
                    command.Parameters.AddWithValue("@No_Cedula", cliente.No_Cedula);
                    command.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                    command.Parameters.AddWithValue("@Profesion", cliente.Profesion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@No_Telefono", cliente.No_Telefono);

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

        // Método para eliminar un cliente
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCliente(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Cliente WHERE ID_Cliente = @ID_Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_Cliente", id);

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
