using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.DAL.Interfaces
{
    public interface IBaseRepo<T>
    {
        Task AddAsync(T entity);
        Task<T[]> SelectAsync();
        Task<T> GetByIdAsync(int id);
    }
}
