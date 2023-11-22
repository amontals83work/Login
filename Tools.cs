using Login.Models;
using Microsoft.EntityFrameworkCore;

namespace Login
{
    public class Tools
    {
        private readonly DemoContext _context;

        public Tools(DemoContext context)
        {
            _context = context;
        }

        public int ValidaUsuario(Usuario model)
        {
            try
            {
                var query = $"SELECT COUNT(*) FROM Usuarios WHERE Email = '{model.Email}' AND Password = '{model.Password}'";
                var result = _context.Database.ExecuteSqlRaw(query);

                return result > 0 ? 1 : 0;
            }
            catch
            {
                Exception e;
            }
            return 0;
        }
    }
}
