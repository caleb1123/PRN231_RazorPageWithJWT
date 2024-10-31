using BOs.Response;
using BOs.Resquest;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class FootballPlayerRepository :IFootballPlayerRepository
    {
        public async Task<FootballPlayerResponse> AddPlayer(FootballPlayerResquest resquest)
        {
            return await FootballPlayerDAO.Instance.AddPlayer(resquest);
        }

        public async Task<FootballPlayerResponse> GetPlayer(string footballPlayerId)
        {
            return await FootballPlayerDAO.Instance.getPlayerById(footballPlayerId);
        }

        public async Task<List<FootballPlayerResponse>> GetPlayers()
        {
            return await FootballPlayerDAO.Instance.GetAll();
        }

        public async Task<bool> RemovePlayer(string footballPlayerId)
        {
            return await FootballPlayerDAO.Instance.deletePlayer(footballPlayerId);
        }

        public async Task<FootballPlayerResponse> UpdatePlayer(FootballPlayerResquest request)
        {
           return await FootballPlayerDAO.Instance.updatePlayer(request);
        }
    }
}
