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

        public async Task<FootballPlayerResponse> UpdatePlayer(FootballPlayerResquest request)
        {
            return await _repository.UpdatePlayer(request);
        }
    }
}
