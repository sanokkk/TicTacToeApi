namespace TicTacToe.DAL.Interfaces;

public interface IBaseRepo<T>
{
    Task AddAsync(T entity);
    Task<T[]> SelectAsync();
    Task<T> GetByIdAsync(int id);
}
