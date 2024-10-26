using BOs;

namespace Repos
{
    public interface IFootballPlayerService
    {
        Task<List<FootballPlayer>> GetFootballPlayers();
        Task<FootballPlayer> GetFootballPlayer(string id);
        Task<FootballPlayer> AddFootballPlayer(FootballPlayer footballPlayer);
        Task<FootballPlayer> UpdateFootballPlayer(FootballPlayer footballPlayer);
        Task<FootballPlayer> DeleteFootballPlayer(string id);
        Task<List<FootballClub>> GetFootballClubs();
    }
}
