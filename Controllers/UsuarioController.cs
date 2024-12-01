using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PrototipoAPIBaseDeDatos.SharedModels;

namespace PrototipoAPIBaseDeDatos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly string _connectionString;

        public UsuariosController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT IdUsuario, Cuenta, FechaCreacion 
                         FROM Usuarios";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Cuenta = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2)
                        });
                    }
                }
            }

            if (usuarios.Count == 0) return NotFound("No se encontraron usuarios.");
            return Ok(usuarios);
        }


        [HttpPost("Registrar")]
        public async Task<ActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Usuarios (Cuenta, Contraseña, FechaCreacion) 
                             VALUES (@Cuenta, @Contraseña, @FechaCreacion)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cuenta", usuario.Cuenta);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña); // Considerar encriptar la contraseña
                    command.Parameters.AddWithValue("@FechaCreacion", usuario.FechaCreacion);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }

            return Ok("Usuario registrado exitosamente.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT IdUsuario, Cuenta, FechaCreacion 
                         FROM Usuarios 
                         WHERE IdUsuario = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = reader.GetInt32(0),
                            Cuenta = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2)
                        };
                    }
                }
            }

            if (usuario == null) return NotFound("Usuario no encontrado.");
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Usuarios 
                         SET Cuenta = @Cuenta, Contraseña = @Contraseña, FechaCreacion = @FechaCreacion 
                         WHERE IdUsuario = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.Parameters.AddWithValue("@Cuenta", usuario.Cuenta);
                    command.Parameters.AddWithValue("@Contraseña", usuario.Contraseña); // Considerar encriptar la contraseña
                    command.Parameters.AddWithValue("@FechaCreacion", usuario.FechaCreacion);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Usuario no encontrado.");
                }
            }

            return NoContent(); // No devuelve contenido, solo el código 204
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM Usuarios WHERE IdUsuario = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected == 0) return NotFound("Usuario no encontrado.");
                }
            }

            return NoContent(); // Eliminar correctamente y no devuelve contenido
        }


        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario usuario)
        {
            Usuario usuarioEncontrado = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "EXEC sp_ObtenerUsuariosDesencriptados"; // Llamamos al SP que desencripta las contraseñas

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        var cuenta = reader.GetString(reader.GetOrdinal("Cuenta"));
                        var contraseñaDesencriptada = reader.GetString(reader.GetOrdinal("ContraseñaDesencriptada"));

                        if (cuenta == usuario.Cuenta && contraseñaDesencriptada == usuario.Contraseña)
                        {
                            // Si la cuenta y contraseña coinciden, retornamos el usuario encontrado
                            usuarioEncontrado = new Usuario
                            {
                                IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                Cuenta = cuenta,
                                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion"))
                            };
                            break;
                        }
                    }
                }
            }

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(usuarioEncontrado); // Login exitoso, devolvemos los datos del usuario
        }



    }

}
