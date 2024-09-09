# C# API Battleship Challenge

## Learning Objectives

- Use all the API skills to create an API to play the game Battleships

## Instructions

1. Fork this repository
2. Clone your fork to your machine
3. Open the project in Visual Studio

## Rules

- Official game rules can be found [here](battleship.pdf)
- A player has two boards, the OCEAN GRID and the TARGET GRID.
- Grids are 10 holes by 10 holes
- A player places their fleet of 5 ships on the OCEAN GRID in a valid horizontal or vertical position which is inside the OCEAN GRID

  - CARRIER SHIP (5 holes)
  - BATTLESHIP (4 holes)
  - CRUISER SHIP (3 holes)
  - SUBMARINE (2 holes)
  - DESTROYER (1 hole)

- Ensure that the fleet item fits on the axis of the OCEAN GRID in the selected co-ordinate
- Players then take it in turns to pick a single square co-ordinate on the TARGET GRID. The target GRID represents the oppositions OCEAN GRID, so if they pick a co-ordinate with the oppositions FLEET on it, they are awarded a HIT!
- To sink a SHIP you must HIT that item in each of its holes on the grid
- The winner is the one who sings all of the opponents FLEET, sinking all of their FLEET

## Core Activity

- build this game as an API
- your game should:
	- have an endpoint that allows a player to join the game
	- have a reset game endpoint

## Extension Activity

- Allow more than 2 people to play this game
- Customize the size of grid to be more than 10 holes
- Allow the expansion of the FLEET 
