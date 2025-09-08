# IFN584 A1 - C# .NET 8 Console application
## Brief - Connect Four, the game
- Individual graded
- 25% weight
- No AI

## Task description
### Basics
1. Basic Connect Four rules
2. Extras:
	- Default grid is 6x7 => User can change grid => Different win condition
	- Special discs

### Features:
1. Menu:
	- New game: Choose mode, play with human or computer.
	- Load game: load previously saved game
	- Test: Same rule, input is a long string of multiple moves
	- Config: Change grid size
2. Dics:
	- Ordinary discs: each player has half of the number of cells, rounded down
		- Example: 6x7 grid = 42 cells => 21 ordinary discs
	- Special discs: 3 types. Implement 2 for the game
		- Boring: return discs to hands. Turn to ordinary at the end of turn
		- Magnetic: turn to ordinary at the end of turn
		- Exploding: destroy and disappear
3. Grid
	- Columns: start from 1, from the left
	- Rows: start from 1, from the bottom
4. Ending game:
	- Winning: [ROWs] x [COLs] x 0.1 continuous disc, rounded down
	- Draw: running out of discs