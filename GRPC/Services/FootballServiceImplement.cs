using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPC.Protos;
using Repos;

namespace GRPC.Services
{
    public class FootballServiceImplement : FootballService.FootballServiceBase
    {
        private readonly IFootballPlayerService _footballPlayerService;

        public FootballServiceImplement(IFootballPlayerService footballPlayerService)
        {
            _footballPlayerService = footballPlayerService;
        }

        // Implement the GetFootballPlayers RPC
        public override async Task<FootballPlayersResponse> GetFootballPlayers(Empty request, ServerCallContext context)
        {
            var players = await _footballPlayerService.GetFootballPlayers();
            var response = new FootballPlayersResponse();
            response.Players.AddRange(players.Select(p => new FootballPlayer
            {
                FootballPlayerId = p.FootballPlayerId,
                FullName = p.FullName,
                Achievements = p.Achievements,
                Birthday = p.Birthday.HasValue ? Timestamp.FromDateTime(p.Birthday.Value.ToUniversalTime()) : null,
                PlayerExperiences = p.PlayerExperiences,
                Nomination = p.Nomination,
                FootballClubId = p.FootballClubId
            }));
            return response;
        }

        // Implement the GetFootballPlayer RPC
        public override async Task<FootballPlayerResponse> GetFootballPlayer(FootballPlayerIdRequest request, ServerCallContext context)
        {
            var player = await _footballPlayerService.GetFootballPlayer(request.Id);
            if (player == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Football player not found"));
            }

            return new FootballPlayerResponse
            {
                Player = new FootballPlayer
                {
                    FootballPlayerId = player.FootballPlayerId,
                    FullName = player.FullName,
                    Achievements = player.Achievements,
                    Birthday = player.Birthday.HasValue ? Timestamp.FromDateTime(player.Birthday.Value.ToUniversalTime()) : null,
                    PlayerExperiences = player.PlayerExperiences,
                    Nomination = player.Nomination,
                    FootballClubId = player.FootballClubId
                }
            };
        }

        // Implement the AddFootballPlayer RPC
        public override async Task<FootballPlayerResponse> AddFootballPlayer(FootballPlayerRequest request, ServerCallContext context)
        {
            var newPlayer = new BOs.FootballPlayer
            {
                FootballPlayerId = request.FootballPlayerId,
                FullName = request.FullName,
                Achievements = request.Achievements,
                Birthday = request.Birthday.ToDateTime(),
                PlayerExperiences = request.PlayerExperiences,
                Nomination = request.Nomination,
                FootballClubId = request.FootballClubId
            };

            var addedPlayer = await _footballPlayerService.AddFootballPlayer(newPlayer);

            return new FootballPlayerResponse
            {
                Player = new FootballPlayer
                {
                    FootballPlayerId = addedPlayer.FootballPlayerId,
                    FullName = addedPlayer.FullName,
                    Achievements = addedPlayer.Achievements,
                    Birthday = addedPlayer.Birthday.HasValue ? Timestamp.FromDateTime(addedPlayer.Birthday.Value.ToUniversalTime()) : null,
                    PlayerExperiences = addedPlayer.PlayerExperiences,
                    Nomination = addedPlayer.Nomination,
                    FootballClubId = addedPlayer.FootballClubId
                }
            };
        }

        // Implement the UpdateFootballPlayer RPC
        public override async Task<FootballPlayerResponse> UpdateFootballPlayer(FootballPlayerRequest request, ServerCallContext context)
        {
            var updatedPlayer = new BOs.FootballPlayer
            {
                FootballPlayerId = request.FootballPlayerId,
                FullName = request.FullName,
                Achievements = request.Achievements,
                Birthday = request.Birthday.ToDateTime(),
                PlayerExperiences = request.PlayerExperiences,
                Nomination = request.Nomination,
                FootballClubId = request.FootballClubId
            };

            var result = await _footballPlayerService.UpdateFootballPlayer(updatedPlayer);

            return new FootballPlayerResponse
            {
                Player = new FootballPlayer
                {
                    FootballPlayerId = result.FootballPlayerId,
                    FullName = result.FullName,
                    Achievements = result.Achievements,
                    Birthday = result.Birthday.HasValue ? Timestamp.FromDateTime(result.Birthday.Value.ToUniversalTime()) : null,
                    PlayerExperiences = result.PlayerExperiences,
                    Nomination = result.Nomination,
                    FootballClubId = result.FootballClubId
                }
            };
        }

        // Implement the DeleteFootballPlayer RPC
        public override async Task<FootballPlayerResponse> DeleteFootballPlayer(FootballPlayerIdRequest request, ServerCallContext context)
        {
            var deletedPlayer = await _footballPlayerService.DeleteFootballPlayer(request.Id);
            if (deletedPlayer == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Football player not found"));
            }

            return new FootballPlayerResponse
            {
                Player = new FootballPlayer
                {
                    FootballPlayerId = deletedPlayer.FootballPlayerId,
                    FullName = deletedPlayer.FullName,
                    Achievements = deletedPlayer.Achievements,
                    Birthday = deletedPlayer.Birthday.HasValue ? Timestamp.FromDateTime(deletedPlayer.Birthday.Value.ToUniversalTime()) : null,
                    PlayerExperiences = deletedPlayer.PlayerExperiences,
                    Nomination = deletedPlayer.Nomination,
                    FootballClubId = deletedPlayer.FootballClubId
                }
            };
        }

        // Implement the GetFootballClubs RPC
        public override async Task<FootballClubsResponse> GetFootballClubs(Empty request, ServerCallContext context)
        {
            var clubs = await _footballPlayerService.GetFootballClubs();
            var response = new FootballClubsResponse();
            response.Clubs.AddRange(clubs.Select(c => new FootballClub
            {
                FootballClubId = c.FootballClubId,
                ClubName = c.ClubName,
                ClubShortDescription = c.ClubShortDescription,
                SoccerPracticeField = c.SoccerPracticeField,
                Mascos = c.Mascos
            }));
            return response;
        }
    }
}
