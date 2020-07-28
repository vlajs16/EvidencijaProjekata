using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logics.Interfaces
{
    public interface IPositionLogic
    {
        Task<List<Position>> GetObjects();
        Task<bool> Insert(Position position);
        Task<bool> Update(Position position);
        Task<Position> Find(int id);
        Task<List<Position>> FindByName(string value);
    }
}
