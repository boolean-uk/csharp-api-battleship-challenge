using exercise.wwwapi.Controller.Repository;
using exercise.wwwapi.Model.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller.Endpoints
{
    public static class BattleShipEndpoint
    {
        public static void ConfigureBattleShipEndpoint(this WebApplication application)
        {
            var battleShipGroup = application.MapGroup("Battleship");
            battleShipGroup.MapPost("/newgame", CreateGame);
            battleShipGroup.MapGet("/game/{gameId}", GetGame);
            battleShipGroup.MapPost("/placefleet/{gameId}", PlaceFleet);
            battleShipGroup.MapPost("/fire/{gameId}", MakeMove);
            battleShipGroup.MapPost("/reset/{gameId}", ResetGame);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult CreateGame(IBattleShipRepository repository, string player1Name, string player2Name, int gridSize)
        {
            var game = repository.CreateGame(player1Name, player2Name, gridSize);
            return TypedResults.Ok(new { gameId = game.Player1.PlayerId });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetGame(IBattleShipRepository repository, string gameId)
        {
            var game = repository.GetGameById(gameId);
            if (game == null)
                return TypedResults.NotFound();

            return TypedResults.Ok(game);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult PlaceFleet(IBattleShipRepository repository, string gameId, [FromBody] List<ShipPlacementModel> fleetPlacements)
        {
            var game = repository.GetGameById(gameId);
            if (game == null)
                return TypedResults.NotFound();

            var player = game.Player1; 

            foreach (var placement in fleetPlacements)
            {
                var ship = player.Fleet.FirstOrDefault(s => s.Type == placement.ShipType);
                if (ship == null)
                    return TypedResults.BadRequest($"Invalid ship type: {placement.ShipType}");

                if (!player.OceanGrid.IsValidCoordinate(placement.StartX, placement.StartY))
                    return TypedResults.BadRequest("Invalid start coordinate.");

                var coordinates = GenerateCoordinates(placement);
                player.OceanGrid.PlaceShip(ship, coordinates);
            }

            repository.UpdateGame(game);
            return TypedResults.Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult MakeMove(IBattleShipRepository repository, string gameId, string playerId, int x, int y)
        {
            var game = repository.GetGameById(gameId);
            if (game == null)
                return TypedResults.NotFound();

            var player = game.Player1.PlayerId == playerId ? game.Player1 : game.Player2;
            if (player != game.CurrentTurn)
                return TypedResults.BadRequest("It's not your turn.");

            var gameWon = game.TakeTurn(player, x, y);
            repository.UpdateGame(game);

            var hit = player.TargetGrid.Cells[x, y] == CellState.Hit;
            return TypedResults.Ok(new { hit, gameWon });
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult ResetGame(IBattleShipRepository repository, string gameId)
        {
            repository.ResetGame(gameId);
            return TypedResults.Ok();
        }

        private static List<(int X, int Y)> GenerateCoordinates(ShipPlacementModel placement)
        {
            var coordinates = new List<(int X, int Y)>();
            for (int i = 0; i < placement.Size; i++)
            {
                if (placement.Orientation == "Horizontal")
                    coordinates.Add((placement.StartX + i, placement.StartY));
                else
                    coordinates.Add((placement.StartX, placement.StartY + i));
            }
            return coordinates;
        }
    }
}
