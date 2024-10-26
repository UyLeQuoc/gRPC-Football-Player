using BOs;
using Repos;

namespace Services
{
    public class FootballPlayerService : IFootballPlayerService
    {
        private readonly IFootballPlayerRepo _footballPlayerRepo;
        public FootballPlayerService(IFootballPlayerRepo footballPlayerRepo)
        {
            _footballPlayerRepo = footballPlayerRepo;
        }

        public async Task<FootballPlayer> AddFootballPlayer(FootballPlayer footballPlayer)
        {
            return await _footballPlayerRepo.AddFootballPlayer(footballPlayer);
        }

        public async Task<FootballPlayer> DeleteFootballPlayer(string id)
        {
            return await _footballPlayerRepo.DeleteFootballPlayer(id);
        }

        public async Task<List<FootballClub>> GetFootballClubs()
        {
            return await _footballPlayerRepo.GetFootballClubs();
        }

        public async Task<FootballPlayer> GetFootballPlayer(string id)
        {
            return await _footballPlayerRepo.GetFootballPlayer(id);
        }

        public async Task<List<FootballPlayer>> GetFootballPlayers()
        {
            return await _footballPlayerRepo.GetFootballPlayers();
        }

        public async Task<FootballPlayer> UpdateFootballPlayer(FootballPlayer footballPlayer)
        {
            return await _footballPlayerRepo.UpdateFootballPlayer(footballPlayer);
        }
    }
}
