using exercise.wwwapi.DTOs;
using exercise.wwwapi.Models;
using exercise.wwwapi.Service;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.EndPoints
{
    public static class BattleshipAPI
    {
        public static void ConfigureBattleshipAPIs(this WebApplication app)
        {
            var battleshipAPIs = app.MapGroup("/battleship");
            battleshipAPIs.MapPost("battleship/joinGame/{name}", JoinGame);
            battleshipAPIs.MapPost("battleship/makeMove/{id}/{x}/{y}", MakeMove);
            battleshipAPIs.MapPost("battleship/resetGame",ResetGame);
            battleshipAPIs.MapPost("battleship/placeShip", PlaceShip);
            battleshipAPIs.MapPost("battleShip/InitializeNewGame", InitializeNewGame);

            }


        public static async Task<IResult> InitializeNewGame(IGameService service)
        {
            if (service.canInitializeNewGame())
            {
                service.InitializeGame(); 
                return Results.Ok(new
                {
                    Message = "A new game has been initialized successfully."
                });
            }
            else
            {
                return Results.BadRequest(new
                {
                    Message = "Cannot initialize a new game. Ensure both players are set and all ships are placed."
                });
            }
        }

        public static async Task<IResult> JoinGame(IGameService service, string name)
        {
            Game game = service.getGame();
            if (string.IsNullOrEmpty(game.Player1.name))
            {
                game.Player1.name = name;
                service.InitializeFleet(game.Player1);
                return Results.Ok(new CustomBattleShipResponse
                {
                    identity= "Player1",
                    action = $"{name} has entered the game as Player 1, fleet initialized",
                    gamestate = "Slot for player1 is filled, game will begin once second slot is filled"
                    
                });
            }
            else if (string.IsNullOrEmpty(game.Player2.name))
            {
                game.Player2.name = name;
                service.InitializeFleet(game.Player2);
                return Results.Ok(new CustomBattleShipResponse
                {
                    identity = "Player 2",
                    action = $"{name} has entered the game as Player 2, fleet initialized",
                    gamestate = "Both player slots filled, game will now begin"
                });
            }

            else
            {
                return Results.BadRequest(new
                {
                    Message = "Both slots are already filled, please try again later."
                });
            }
        }


        public static async Task<IResult> MakeMove(IGameService servce, int id, int x, int y)
        {
            var game = servce.getGame();
            var coordinate = new Coordinate(x, y);
            var player = servce.getPlayer(id);
            if (player != null) {
                return TypedResults.BadRequest(new CustomBattleShipResponse
                {
                    identity = "PlayerID is invalid",
                    action = "invalid",
                        });
                    }
            if ((id ==1 &&
                servce.IsValidCoordinate(coordinate) &&
                game.IsPlayer1Turn) ||
                (id ==2 &&
                servce.IsValidCoordinate(coordinate) &&
                !game.IsPlayer1Turn
                ))
            {
                return TypedResults.Ok(new CustomBattleShipResponse
                {
                    identity = player.name,
                    action = servce.MakeMove(player, coordinate),
                    gamestate = servce.CheckGameStatus()
                }

                );
            }
            else 
            {
                return TypedResults.Ok(new CustomBattleShipResponse
                {
                    identity = player.name,
                    action = "Either it isnt your turn, or the coordinates are invalid !"

                }
                );
            }
        }


        public static async Task<IResult> PlaceShip(IGameService service, PlaceShipDTO dto)
        {
            var game = service.getGame();
            var player = service.getPlayer(dto.playerId);
            var ship = player.Fleet.FirstOrDefault(x => x.Name == dto.shipName);
            var Xcoordinate = new Coordinate(dto.xAxis, dto.yAxis);
            if (player == null || ship == null) {
                return TypedResults.Ok("Player or ship not found");

            }
            else if (service.CanPlaceShip(player, ship, Xcoordinate, dto.isVertical))
            {
                service.PlaceShip(player, ship, Xcoordinate, dto.isVertical);
                return TypedResults.Ok("Ship" + ship.Name + " has been placed at desired location");
            }

            else
            {
                return TypedResults.BadRequest("Coordinates invalid, placement failed !");
            }
        }

        public static async Task<IResult> ResetGame(IGameService service)
        {
            service.ResetGame();
            return TypedResults.Ok("Game has been reset successfully!");
        }

        } 
    }

