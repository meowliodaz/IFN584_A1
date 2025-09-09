## Grid
- Attributes:
	- Size: 6x7 minimum (6 rows, 7 cols). And rows can't be more than cols
	- Use List of List:
		- Inner list: List of cells in 1 column
		- Outer list: List of columns
- Behaviours:
	- Display
	- Update:
		- Ordinary
		- Special
	- Check winning condition:
		- 8 directions
		- Max distance = winning condition - 1 (since 1 cell is already used as base, we only need to check the remaining 3, or win -1)

## Menu (?)
- Load game
- New game
	- Modes
- Change grid size

## Player
- Attributes:
	- ID
	- Number of discs of each type
	- Move log (?)
- Behaviours: Based on player type

## Human: Player
- Behaviours: None

## Computer: Player
- Behaviours:
	- Play to win
	- Play random

## Main
- Game loop
- Input handling
	- Out of bound
	- Weird disc
	- Wrong input format
	- Wrong input data type
- Save game
- Quit game
- Check for end
	- Win
	- Draw: both player run out of disc and noone has won
