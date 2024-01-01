namespace ConsoleApp1
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string _connectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDB") ?? throw new InvalidOperationException("La cadena de conexión no puede ser nula.");
        }

        public void Add(Usuario usuario)
        {
            //throw new NotImplementedException();
        }

        public void Update(Usuario usuario)
        {
            //throw new NotImplementedException();
        }

        public void Delete(Usuario usuario)
        {
            //throw new NotImplementedException();
        }

        public IEnumerable<Usuario> GetAll()
        {
            return Enumerable.Empty<Usuario>();
        }

        public Usuario? GetByUsername(string username)
        {
            using var connection = new OracleConnection(_connectionString);
            connection.Open();
            using var command = new OracleCommand("SELECT * FROM EMPLEADOS WHERE   EMPLEADO = :username", connection);
            command.Parameters.Add(new OracleParameter("username", username));

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    EMPLEADO = reader.GetString(reader.GetOrdinal("EMPLEADO"))
                };
            }
            return null;
        }
    }
}