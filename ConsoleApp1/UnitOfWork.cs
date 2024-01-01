namespace ConsoleApp1
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRegistroAccesoRepository _registroAccesoRepository;

        public UnitOfWork(IUsuarioRepository usuarioRepository, IRegistroAccesoRepository registroAccesoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _registroAccesoRepository = registroAccesoRepository;
        }

        public IUsuarioRepository Usuarios => _usuarioRepository;
        public IRegistroAccesoRepository RegistroAccesos => _registroAccesoRepository;

    }
}
