using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuario(string id, string pass);
        IEnumerable<Usuario> GetUsuario();
        Usuario GetUsuarioByID(string id);
        void DeleteUsuario(string id);
        Usuario Save(Usuario usuario);
    }
}
