using BOs;
using BOs.Response;
using BOs.Resquest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IFootballPlayerRepository
    {
        Task<FootballPlayerResponse> AddPlayer(FootballPlayerResquest resquest);

        Task<FootballPlayer> UpdatePlayer(FootballPlayer request);

        Task<bool> RemovePlayer(string footballPlayerId);

        Task<FootballPlayer> GetPlayer(string footballPlayerId);

        Task<List<FootballPlayerResponse>> GetPlayers();

        Task<List<FootballPlayerResponse>> SearchPlayers(string searchTerm);
    }
}
