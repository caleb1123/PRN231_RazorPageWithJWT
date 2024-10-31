using BOs;
using BOs.Response;
using BOs.Resquest;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Impl
{
    public class FootballClubService : IFootballClubService
    {
        private IFootballClubRepository _footballClubRepository;
        public FootballClubService (IFootballClubRepository footballClubRepository)
        {
            _footballClubRepository = footballClubRepository;
        }

        public async Task<FootballClub> AddFootballClub(FootballClubRequest footballClub)
        {
            return await _footballClubRepository.AddFootballClub(footballClub);
        }

        public async Task<bool> DeleteFootballClub(string footballClubId)
        {
            return await _footballClubRepository.DeleteFootballClub(footballClubId);
        }

        public async Task<FootballClubResponse> GetFootballClubById(string footballClubId)
        {
            return await _footballClubRepository.GetFootballClubById(footballClubId);
        }

        public async Task<List<FootballClubResponse>> GetFootballClubs()
        {
            return await _footballClubRepository.GetFootballClubs();
        }

        public async Task<FootballClub> UpdateFootballClub(FootballClubRequest footballClub)
        {
            return await _footballClubRepository.UpdateFootballClub(footballClub);
        }
    }
}
