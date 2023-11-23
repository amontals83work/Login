using Login.Models;
using Microsoft.Data.SqlClient;
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
                var query = "SELECT * FROM Usuarios WHERE Email = @Email AND Password = @Password";
                var result = _context.Usuarios.FromSqlRaw(query, new SqlParameter("@Email", model.Email), new SqlParameter("@Password", model.Password)).FirstOrDefault();

                return result != null ? 1 : 0;
            }
            catch
            {
                Exception e;
            }
            return 0;
        }
    }
}
