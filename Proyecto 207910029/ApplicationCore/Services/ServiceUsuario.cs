using ApplicationCore.Utils;
using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario:IServiceUsuario
    {
        public void DeleteUsuario(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario(); //importante poner public en los repositories
            repository.DeleteUsuario(id);
        }

        public IEnumerable<Usuario> GetUsuario()
        {
           
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario();
        }
        public Usuario GetUsuario(string id, string pass)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            // Se encripta el valor que viene y se compara con el valor encriptado en al BD.
            string crytpPasswd = Cryptography.EncrypthAES(pass);
            return repository.GetUsuario(id,crytpPasswd);
        }

        public Usuario GetUsuarioByID(string id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuarioByID(id);
        }

        public Usuario Save(Usuario usuario)
        {
            string crytpPasswd = Cryptography.EncrypthAES(usuario.PasswordHash);
            usuario.PasswordHash = crytpPasswd;
            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.Save(usuario);
        }

        
    }
}
