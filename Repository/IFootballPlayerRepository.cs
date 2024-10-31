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

        Task<FootballPlayerResponse> UpdatePlayer(FootballPlayerResquest request);
    }
}
