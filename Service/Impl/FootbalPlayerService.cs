using BOs;
using BOs.Response;
using BOs.Resquest;
using DAO;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class FootbalPlayerService :IFootballPlayerService
    {
        private readonly IFootballPlayerRepository _repository;
        public FootbalPlayerService(IFootballPlayerRepository repository)
        {
            _repository = repository;
        }
        public async Task<FootballPlayerResponse> AddPlayer(FootballPlayerResquest resquest)
        {
            return await _repository.AddPlayer(resquest);
        }

        public Task<FootballPlayer> GetPlayer(string footballPlayerId)
        {
            return _repository.GetPlayer(footballPlayerId);
        }

        public Task<List<FootballPlayerResponse>> GetPlayers()
        {
            return _repository.GetPlayers();
        }

        public Task<bool> RemovePlayer(string footballPlayerId)
        {
            return _repository.RemovePlayer(footballPlayerId);
        }

        public Task<List<FootballPlayerResponse>> SearchPlayers(string searchTerm)
        {
            return _repository.SearchPlayers(searchTerm);
        }

        public async Task<FootballPlayer> UpdatePlayer(FootballPlayer request)
        {
            return await _repository.UpdatePlayer(request);
        }
    }
}
