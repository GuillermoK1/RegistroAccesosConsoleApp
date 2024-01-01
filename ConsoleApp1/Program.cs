using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Construcción del objeto de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            // Configuración e Inyección de Dependencias
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddTransient<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddTransient<IRegistroAccesoRepository, RegistroAccesoRepository>();
            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            string correctPassword = configuration["Password"] ?? "";

            if (string.IsNullOrEmpty(correctPassword))
            {
                Console.WriteLine("La configuración de la contraseña no se ha establecido correctamente.");
                return;
            }

            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            if (unitOfWork == null)
            {
                Console.WriteLine("No se pudo obtener la unidad de trabajo.");
                return;
            }

            bool loginSuccessful = false;

            while (!loginSuccessful)
            {
                try
                {
                    Console.WriteLine("Por favor, ingresa tu nombre de usuario:");
                    string username = Console.ReadLine() ?? "";

                    Console.WriteLine("Por favor, ingresa tu contraseña:");
                    string password = Console.ReadLine() ?? "";

                    var usuario = unitOfWork.Usuarios.GetByUsername(username);
                    if (usuario != null && password == correctPassword)
                    {
                        Console.WriteLine("Inicio de sesión exitoso!");
                        try
                        {
                            unitOfWork.RegistroAccesos.RegistrarAcceso(usuario.EMPLEADO);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al registrar el acceso: {ex.Message}");
                            // Considera si quieres continuar en este punto o no.
                        }
                        loginSuccessful = true;
                    }
                    else
                    {
                        Console.WriteLine("Usuario o contraseña incorrectos. Inténtalo de nuevo.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocurrió un error: {ex.Message}");
                }
            }

            Console.WriteLine("Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}
