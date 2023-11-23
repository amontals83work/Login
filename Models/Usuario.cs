using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Usuario
    {
        [Key]
        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
