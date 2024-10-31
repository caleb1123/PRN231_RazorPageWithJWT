using BOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IFootballClubService
    {
        Task<FootballClubResponse> GetFootballClubById(string footballClubId);

        Task<List<FootballClubResponse>> GetFootballClubs();
    }
}
