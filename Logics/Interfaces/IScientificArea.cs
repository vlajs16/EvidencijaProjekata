using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IScientificArea
    {
        Task<List<ScientificArea>> GetObjects();
        Task<List<ScientificArea>> Find(string criteria);
        Task<ScientificArea> GetById(long scientificAreaId);
    }
}
