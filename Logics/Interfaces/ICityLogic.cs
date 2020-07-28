using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface ICityLogic
    {
        Task<List<City>> GetAll();
        Task<City> GetById(float id);
        Task<List<City>> Find(string criteria);
    }
}
