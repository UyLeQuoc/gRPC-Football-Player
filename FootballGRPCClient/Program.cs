using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GRPC.Protos;

namespace FootballClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a channel to connect to the gRPC server
            using var channel = GrpcChannel.ForAddress("http://localhost:5127");
            var client = new FootballService.FootballServiceClient(channel);

            // Call the gRPC methods
            await GetAllFootballPlayers(client);
            //await GetSingleFootballPlayer(client, "1"); // Replace "1" with a valid player ID
            //await AddFootballPlayer(client);
            //await UpdateFootballPlayer(client);
            //await DeleteFootballPlayer(client, "1"); // Replace "1" with a valid player ID
            //await GetAllFootballClubs(client);
        }

        private static async Task GetAllFootballPlayers(FootballService.FootballServiceClient client)
        {
            Console.WriteLine("Fetching all football players...");
            var response = await client.GetFootballPlayersAsync(new Empty());
            foreach (var player in response.Players)
            {
                Console.WriteLine($"ID: {player.FootballPlayerId}, Name: {player.FullName}, Birthday: {player.Birthday?.ToDateTime()}");
            }
        }

        private static async Task GetSingleFootballPlayer(FootballService.FootballServiceClient client, string playerId)
        {
            Console.WriteLine($"Fetching football player with ID: {playerId}...");
            var request = new FootballPlayerIdRequest { Id = playerId };
            try
            {
                var response = await client.GetFootballPlayerAsync(request);
                Console.WriteLine($"ID: {response.Player.FootballPlayerId}, Name: {response.Player.FullName}, Birthday: {response.Player.Birthday?.ToDateTime()}");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"Error: {e.Status.Detail}");
            }
        }

        private static async Task AddFootballPlayer(FootballService.FootballServiceClient client)
        {
            Console.WriteLine("Adding a new football player...");
            var request = new FootballPlayerRequest
            {
                FootballPlayerId = "2", // Use a unique ID
                FullName = "John Doe",
                Achievements = "Championship Winner",
                Birthday = Timestamp.FromDateTime(DateTime.UtcNow.AddYears(-25).ToUniversalTime()),
                PlayerExperiences = "5 years",
                Nomination = "Best Forward",
                FootballClubId = "Club123" // Use a valid club ID
            };
            var response = await client.AddFootballPlayerAsync(request);
            Console.WriteLine($"Added player with ID: {response.Player.FootballPlayerId}");
        }

        private static async Task UpdateFootballPlayer(FootballService.FootballServiceClient client)
        {
            Console.WriteLine("Updating a football player...");
            var request = new FootballPlayerRequest
            {
                FootballPlayerId = "2", // Use the ID of an existing player
                FullName = "John Doe Updated",
                Achievements = "MVP 2021",
                Birthday = Timestamp.FromDateTime(DateTime.UtcNow.AddYears(-26).ToUniversalTime()),
                PlayerExperiences = "6 years",
                Nomination = "Best Forward",
                FootballClubId = "Club123"
            };
            var response = await client.UpdateFootballPlayerAsync(request);
            Console.WriteLine($"Updated player with ID: {response.Player.FootballPlayerId}");
        }

        private static async Task DeleteFootballPlayer(FootballService.FootballServiceClient client, string playerId)
        {
            Console.WriteLine($"Deleting football player with ID: {playerId}...");
            var request = new FootballPlayerIdRequest { Id = playerId };
            try
            {
                var response = await client.DeleteFootballPlayerAsync(request);
                Console.WriteLine($"Deleted player with ID: {response.Player.FootballPlayerId}");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"Error: {e.Status.Detail}");
            }
        }

        private static async Task GetAllFootballClubs(FootballService.FootballServiceClient client)
        {
            Console.WriteLine("Fetching all football clubs...");
            var response = await client.GetFootballClubsAsync(new Empty());
            foreach (var club in response.Clubs)
            {
                Console.WriteLine($"ID: {club.FootballClubId}, Name: {club.ClubName}");
            }
        }
    }
}
