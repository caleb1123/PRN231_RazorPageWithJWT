using BOs;
using BOs.Response;
using BOs.Resquest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class FootballPlayerDAO
    {
        private static FootballPlayerDAO instance = null;
        private readonly EnglishPremierLeague2024DbContext context;

        private FootballPlayerDAO()
        {
            context = new EnglishPremierLeague2024DbContext();
        }

        public static FootballPlayerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FootballPlayerDAO();
                }
                return instance;
            }
        }

        public Task<FootballPlayerResponse> AddPlayer(FootballPlayerResquest resquest)
        {
            FootballClub club = context.FootballClubs.Find(resquest.FootballClubId);
            
            if(club == null)
            {
                throw new Exception("Club not found");
            }

            FootballPlayer player = new FootballPlayer
            {
                FootballPlayerId = resquest.FootballPlayerId,
                FullName = resquest.FullName,
                Achievements = resquest.Achievements,
                Birthday = resquest.Birthday,
                PlayerExperiences = resquest.PlayerExperiences,
                Nomination = resquest.Nomination,
                FootballClubId = resquest.FootballClubId,
                FootballClub = club
            };

            context.FootballPlayers.Add(player);
            context.SaveChanges();

            return Task.FromResult(new FootballPlayerResponse
            {
                FootballPlayerId = player.FootballPlayerId,
                FullName = player.FullName,
                Achievements = player.Achievements,
                Birthday = player.Birthday,
                PlayerExperiences = player.PlayerExperiences,
                Nomination = player.Nomination,
                FootballClubId = player.FootballClubId,
                
            });

        }
        public async Task<FootballPlayerResponse> updatePlayer(FootballPlayerResquest request)
        {
            var club = await context.FootballClubs.FindAsync(request.FootballClubId);
            if (club == null)
            {
                throw new Exception("Club not found");
            }
            var player = await context.FootballPlayers.FindAsync(request.FootballPlayerId);
            if(player == null)
            {
                throw new Exception("Player not exist");
            }

            player.FullName = request.FullName;
            player.Achievements = request.Achievements;
            player.Birthday = request.Birthday;
            player.PlayerExperiences = request.PlayerExperiences;
            player.Nomination = request.Nomination;
            player.FootballClubId = request.FootballClubId;
            player.FootballClub = club;
            await context.SaveChangesAsync();

            return new FootballPlayerResponse
            {
                FootballPlayerId = player.FootballPlayerId,
                FullName = player.FullName,
                Achievements = player.Achievements,
                Birthday = player.Birthday,
                PlayerExperiences = player.PlayerExperiences,
                Nomination = player.Nomination,
                FootballClubId = player.FootballClubId,
            };
        }
    }
}
