using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public interface IRepositoryAseguradora
    {
        IEnumerable<Aseguradora> GetAseguradora();
        Aseguradora GetAseguradoraByID(int id);
        Aseguradora Save(Aseguradora aseguradora);
        void DeleteAseguradora(int id);
    }
}
