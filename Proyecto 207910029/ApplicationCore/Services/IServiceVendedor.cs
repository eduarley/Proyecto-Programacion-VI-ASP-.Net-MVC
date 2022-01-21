using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceVendedor
    {
        IEnumerable<Vendedor> GetVendedor();
        Vendedor GetVendedorByID(string cedulaJuridica);
        void DeleteVendedor(string cedulaJuridica);
        Vendedor Save(Vendedor vendedor);
    }
}
