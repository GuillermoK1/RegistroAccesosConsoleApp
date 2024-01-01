namespace ConsoleApp1
{
    public interface IUnitOfWork
    {
        IUsuarioRepository Usuarios { get; }
        IRegistroAccesoRepository RegistroAccesos { get; }
    }

}
