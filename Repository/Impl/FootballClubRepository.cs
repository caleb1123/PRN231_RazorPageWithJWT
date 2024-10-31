using BOs;
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
    public class FootballClubRepository : IFootballClubRepository
    {
        public async Task<FootballClub> AddFootballClub(FootballClubRequest footballClub)
        {
           return await FootballClubDAO.Instance.AddFootballClub(footballClub);
        }

        public async Task<bool> DeleteFootballClub(string footballClubId)
        {
            return await FootballClubDAO.Instance.DeleteFootballClub(footballClubId);
        }

        public async Task<FootballClubResponse> GetFootballClubById(string footballClubId)
        {
           return await FootballClubDAO.Instance.GetFootballClubById(footballClubId);
        }

        public async Task<List<FootballClubResponse>> GetFootballClubs()
        {
            return await FootballClubDAO.Instance.GetFootballClubs();
        }

        public Task<FootballClub> UpdateFootballClub(FootballClubRequest footballClub)
        {
            return FootballClubDAO.Instance.UpdateFootballClub(footballClub);
        }
    }
}
