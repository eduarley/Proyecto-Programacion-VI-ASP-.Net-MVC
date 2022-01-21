using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceVendedor: IServiceVendedor
    {
        public void DeleteVendedor(string cedulaJuridica)
        {
            IRepositoryVendedor repository = new RepositoryVendedor(); //importante poner public en los repositories
            repository.DeleteVendedor(cedulaJuridica);
        }

        public IEnumerable<Vendedor> GetVendedor()
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.GetVendedor();
        }

        public Vendedor GetVendedorByID(string cedulaJuridica)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.GetVendedorByID(cedulaJuridica);
        }

        public Vendedor Save(Vendedor vendedor)
        {
            IRepositoryVendedor repository = new RepositoryVendedor();
            return repository.Save(vendedor);
        }
    }
}
