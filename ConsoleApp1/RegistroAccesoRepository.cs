namespace ConsoleApp1
{
    public class RegistroAccesoRepository : IRegistroAccesoRepository
    {
        private readonly string _connectionString;

        public RegistroAccesoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("OracleDB");
        }

        public void RegistrarAcceso(string Empleado)
        {
            try
            {
                using var connection = new OracleConnection(_connectionString);
                connection.Open();
                using OracleCommand command = new("INSERT INTO RegistroAccesos (Empleado, Acceso) VALUES (:nombre, SYSDATE)", connection);
                command.Parameters.Add(new OracleParameter("nombre", Empleado));
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                // Manejo de la excepción específica de Oracle
                Console.WriteLine($"Error de Oracle: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejo de otras excepciones
                Console.WriteLine($"Error general: {ex.Message}");
            }
        }
    }
}
