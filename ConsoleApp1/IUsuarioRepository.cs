namespace ConsoleApp1
{
    public interface IUsuarioRepository
    {
        Usuario? GetByUsername(string username);
        IEnumerable<Usuario> GetAll();
        void Add(Usuario usuario);
        void Update(Usuario usuario);
        void Delete(Usuario usuario);
    }

}
