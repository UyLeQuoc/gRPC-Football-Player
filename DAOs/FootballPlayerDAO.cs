
using BOs;
using Microsoft.EntityFrameworkCore;

namespace DAOs
{
    public class FootballPlayerDAO
    {
        private static FootballPlayerDAO instance = null;

        private FootballPlayerDAO()
        {
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

        public async Task<List<FootballPlayer>> GetPlayers()
        {
            using (var context = new EnglishPremierLeague2024DbContext())
            {
                var players = await context.FootballPlayers.Include(p => p.FootballClub).ToListAsync();
                return players;
            }
        }

        public async Task<FootballPlayer> GetPlayerById(string id)
        {
            using (var context = new EnglishPremierLeague2024DbContext())
            {
                var player = await context.FootballPlayers.Include(p => p.FootballClub).FirstOrDefaultAsync(player => player.FootballPlayerId == id);
                return player;
            }
        }

        private string GeneratePlayerId()
        {
            var random = new Random();
            var id = random.Next(10000, 99999);
            return "PL" + id.ToString();
        }

        public async Task<FootballPlayer> AddPlayer(FootballPlayer player)
        {
            try
            {
                using (var context = new EnglishPremierLeague2024DbContext())
                {
                    player.FootballPlayerId = GeneratePlayerId();
                    await context.FootballPlayers.AddAsync(player);
                    await context.SaveChangesAsync();
                    return player;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task<FootballPlayer> UpdatePlayer(FootballPlayer player)
        {
            using (var context = new EnglishPremierLeague2024DbContext())
            {
                var playerToUpdate = await context.FootballPlayers.FirstOrDefaultAsync(p => p.FootballPlayerId == player.FootballPlayerId);
                if (playerToUpdate == null)
                {
                    throw new Exception("Player not found");
                }
                playerToUpdate.FullName = player.FullName;
                playerToUpdate.Achievements = player.Achievements;
                playerToUpdate.Birthday = player.Birthday;
                playerToUpdate.PlayerExperiences = player.PlayerExperiences;
                playerToUpdate.Nomination = player.Nomination;
                playerToUpdate.FootballClubId = player.FootballClubId;

                context.Update(playerToUpdate);

                await context.SaveChangesAsync();
                return playerToUpdate;
            }
        }

        public async Task<FootballPlayer> DeletePlayer(string id)
        {
            try
            {
                using (var context = new EnglishPremierLeague2024DbContext())
                {
                    var playerToDelete = await context.FootballPlayers.FirstOrDefaultAsync(p => p.FootballPlayerId.Equals(id));
                    if (playerToDelete == null)
                    {
                        throw new Exception("Player not found");
                    }

                    context.FootballPlayers.Remove(playerToDelete);
                    await context.SaveChangesAsync();

                    return playerToDelete;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FootballClub>> GetFootballClubs()
        {
            using (var context = new EnglishPremierLeague2024DbContext())
            {
                var clubs = await context.FootballClubs.ToListAsync();
                return clubs;
            }
        }
    }
}
