using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceAseguradora : IServiceAseguradora
    {
        public IEnumerable<Aseguradora> GetAseguradora()
        {
            IRepositoryAseguradora repository = new RepositoryAseguradora();
            return repository.GetAseguradora();
        }

        public Aseguradora GetAseguradoraByID(int id)
        {
            IRepositoryAseguradora repository = new RepositoryAseguradora();
            return repository.GetAseguradoraByID(id);
        }

        public Aseguradora Save(Aseguradora aseguradora)
        {
            IRepositoryAseguradora repository = new RepositoryAseguradora();
            return repository.Save(aseguradora);
        }
        public void DeleteAseguradora(int id)
        {
            IRepositoryAseguradora repository = new RepositoryAseguradora();
            repository.DeleteAseguradora(id);
        }
    }
}
