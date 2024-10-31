using BOs;
using BOs.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FootballClubDAO
    {
        private static FootballClubDAO instance = null;
        private readonly EnglishPremierLeague2024DbContext context;

        private FootballClubDAO()
        {
            context = new EnglishPremierLeague2024DbContext();
        }

        public static FootballClubDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FootballClubDAO();
                }
                return instance;
            }
        }


        public async Task<FootballClubResponse> GetFootballClubById(string footballClubId)
        {
            // Tìm kiếm câu lạc bộ theo ID
            var club = await context.FootballClubs.FindAsync(footballClubId);

            // Kiểm tra nếu câu lạc bộ không tồn tại
            if (club == null)
            {
                throw new Exception("Club not found");
            }

            // Ánh xạ FootballClub sang FootballClubResponse
            return new FootballClubResponse
            {
                FootballClubId = club.FootballClubId,
                ClubName = club.ClubName,
                ClubShortDescription = club.ClubShortDescription,
                SoccerPracticeField = club.SoccerPracticeField,
                Mascos = club.Mascos
            };
        }


        public async Task<List<FootballClubResponse>> GetFootballClubs()
        {
            List<FootballClub> footballClubs = await context.FootballClubs.ToListAsync();

            if(footballClubs == null)
            {
                throw new Exception("Clubs not found");
            }
            // Ánh xạ danh sách FootballClub sang danh sách FootballClubResponse
            return footballClubs.Select(club => new FootballClubResponse
            {
                FootballClubId = club.FootballClubId,
                ClubName = club.ClubName,
                ClubShortDescription = club.ClubShortDescription,
                SoccerPracticeField = club.SoccerPracticeField,
                Mascos = club.Mascos
            }).ToList();

        }
    }
}
