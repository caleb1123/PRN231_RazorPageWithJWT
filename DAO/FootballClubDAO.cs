using BOs;
using BOs.Response;
using BOs.Resquest;
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

            if (footballClubs == null)
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

        public async Task<FootballClub> AddFootballClub(FootballClubRequest footballClub)
        {
            var club = await context.FootballClubs.FindAsync(footballClub.FootballClubId);
            if (club != null)
            {
                throw new Exception("Club already exists");
            }

            FootballClub clubNew = new FootballClub
            {
                FootballClubId = footballClub.FootballClubId,
                ClubName = footballClub.ClubName,
                ClubShortDescription = footballClub.ClubShortDescription,
                SoccerPracticeField = footballClub.SoccerPracticeField,
                Mascos = footballClub.Mascos
            };

            context.Add(clubNew);
            await context.SaveChangesAsync();
            return clubNew;
        }

        public async Task<FootballClub> UpdateFootballClub(FootballClubRequest footballClub)
        {
            var club = await context.FootballClubs.FindAsync(footballClub.FootballClubId);
            if (club == null)
            {
                throw new Exception("Club not found");
            }

            club.ClubName = footballClub.ClubName;
            club.ClubShortDescription = footballClub.ClubShortDescription;
            club.SoccerPracticeField = footballClub.SoccerPracticeField;
            club.Mascos = footballClub.Mascos;
            await context.SaveChangesAsync();
            return club;
        }

        public async Task<bool> DeleteFootballClub(String footballClubId)
        {
            var club = await context.FootballClubs.FindAsync(footballClubId);
            if (club == null)
            {
                throw new Exception($"Unable to delete football club");
            }

            context.FootballClubs.Remove(club);
            await context.SaveChangesAsync();
            return true;

        }
    }
}

