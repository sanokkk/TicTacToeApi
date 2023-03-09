using TicTacToe.DAL.Interfaces;
using TicTacToe.DAL.Repos;
using TicTacToe.Service.Services;

namespace TicTacToe
{
    public static class Scopper
    {
        public static void AddScopes(this IServiceCollection services)
        {
            services.AddScoped<IGameRepo, GameRepo>();
            services.AddScoped<IGameService, GameService>();
        }
    }
}
