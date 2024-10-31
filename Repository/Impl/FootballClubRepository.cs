using BOs.Response;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Impl
{
    public class FootballClubRepository : IFootballClubRepository
    {
        public async Task<FootballClubResponse> GetFootballClubById(string footballClubId)
        {
           return await FootballClubDAO.Instance.GetFootballClubById(footballClubId);
        }

        public async Task<List<FootballClubResponse>> GetFootballClubs()
        {
            return await FootballClubDAO.Instance.GetFootballClubs();
        }
    }
}
