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

        public async Task<FootballPlayerResponse> AddPlayer(FootballPlayerResquest request)
        {
            // Tìm kiếm câu lạc bộ theo FootballClubId
            var club = await context.FootballClubs.FindAsync(request.FootballClubId);

            if (club == null)
            {
                throw new Exception("Club not found");
            }

            // Tạo đối tượng cầu thủ bóng đá mới
            FootballPlayer player = new FootballPlayer
            {
                FootballPlayerId = request.FootballPlayerId,
                FullName = request.FullName,
                Achievements = request.Achievements,
                Birthday = request.Birthday,
                PlayerExperiences = request.PlayerExperiences,
                Nomination = request.Nomination,
                FootballClubId = request.FootballClubId,
                FootballClub = club
            };

            // Thêm cầu thủ vào context
            await context.FootballPlayers.AddAsync(player); // Sử dụng AddAsync để thêm cầu thủ
            await context.SaveChangesAsync(); // Sử dụng SaveChangesAsync để lưu thay đổi

            // Trả về đối tượng FootballPlayerResponse
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

        public async Task<FootballPlayer> updatePlayer(FootballPlayer request)
        {
            var club = await context.FootballClubs.FindAsync(request.FootballClubId);
            if (club == null)
            {
                throw new Exception("Club not found");
            }
            var player = await context.FootballPlayers.FindAsync(request.FootballPlayerId);
            if (player == null)
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

            return player;
        }

        public async Task<bool> deletePlayer(string id)
        {
            var player = await context.FootballPlayers.FindAsync(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            context.FootballPlayers.Remove(player);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FootballPlayerResponse>> GetAll()
        {
            // Retrieve all football players from the context, including their FootballClub
            List<FootballPlayer> players = await context.FootballPlayers
                .Include(p => p.FootballClub) // Include the FootballClub navigation property
                .ToListAsync();

            // Map the players to their response DTOs
            List<FootballPlayerResponse> playerResponses = players.Select(player => new FootballPlayerResponse
            {
                FootballPlayerId = player.FootballPlayerId,
                FullName = player.FullName,
                Achievements = player.Achievements,
                Birthday = player.Birthday,
                PlayerExperiences = player.PlayerExperiences,
                Nomination = player.Nomination,
                FootballClubId = player.FootballClubId,
                FootballClub = player.FootballClub != null ? new FootballClubResponse
                {
                    FootballClubId = player.FootballClub.FootballClubId,
                    ClubName = player.FootballClub.ClubName,
                    ClubShortDescription = player.FootballClub.ClubShortDescription,
                    SoccerPracticeField = player.FootballClub.SoccerPracticeField,
                    Mascos = player.FootballClub.Mascos
                } : null // Handle the case where FootballClub is null
            }).ToList();

            // Throw an exception if no players were found
            if (!playerResponses.Any())
            {
                throw new Exception("No player found");
            }

            return playerResponses;
        }




        public async Task<FootballPlayer> getPlayerById(string id)
        {
            var player = await context.FootballPlayers.FindAsync(id);
            if (player == null)
            {
                throw new Exception("Player not found");
            }
            return player;
        }

        public async Task<List<FootballPlayerResponse>> SearchPlayers(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await GetAll();
            }

            // Perform search based on FullName and Achievements fields
            List<FootballPlayer> players = await context.FootballPlayers
                .Include(p => p.FootballClub) // Include the FootballClub navigation property
                .Where(p => p.FullName.Contains(searchTerm) ||
                            p.Achievements.Contains(searchTerm))
                .ToListAsync();

            // Map the players to their response DTOs
            List<FootballPlayerResponse> playerResponses = players.Select(player => new FootballPlayerResponse
            {
                FootballPlayerId = player.FootballPlayerId,
                FullName = player.FullName,
                Achievements = player.Achievements,
                Birthday = player.Birthday,
                PlayerExperiences = player.PlayerExperiences,
                Nomination = player.Nomination,
                FootballClubId = player.FootballClubId,
                FootballClub = player.FootballClub != null ? new FootballClubResponse
                {
                    FootballClubId = player.FootballClub.FootballClubId,
                    ClubName = player.FootballClub.ClubName,
                    ClubShortDescription = player.FootballClub.ClubShortDescription,
                    SoccerPracticeField = player.FootballClub.SoccerPracticeField,
                    Mascos = player.FootballClub.Mascos
                } : null // Handle the case where FootballClub is null
            }).ToList();

            // Throw an exception if no players were found
            if (!playerResponses.Any())
            {
                throw new Exception("No player found");
            }

            return playerResponses;
        }
     }
 }
