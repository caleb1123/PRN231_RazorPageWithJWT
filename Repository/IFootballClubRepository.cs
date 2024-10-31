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
    public interface IFootballClubRepository
    {
        Task<FootballClubResponse> GetFootballClubById(string footballClubId);

        Task<List<FootballClubResponse>> GetFootballClubs();

        Task<FootballClub> AddFootballClub(FootballClubRequest footballClub);

        Task<FootballClub> UpdateFootballClub(FootballClubRequest footballClub);

        Task<bool> DeleteFootballClub(string footballClubId);
    }
}
