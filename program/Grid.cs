// PROGRESS
/* TODO:
	- Update grid
	- Implement disc behavior? 
*/



// Built-in libraries


// 3rd party libraries


// Namespace


using System.Collections.ObjectModel;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;


namespace Connect4
{
	public class Grid
	{
		// Fields
		private const int DELAY = 1000;     // milisecond
		private const int DELAY_TEST = 500; // milisecond

		private IReadOnlyDictionary<string, string>[] discDict = new Dictionary<string, string>[2]
		{
			new Dictionary<string, string>
			{
				{"o", "@"},
				{"m", "M"},
				{"b", "B"},
				{"e", "E"}
			},
			new Dictionary<string, string>
			{
				{"o", "#"},
				{"m", "m"},
				{"b", "b"},
				{"e", "e"}
			},
		};
		private List<List<string>> matrix = new();

		// Properties
		public int Cols { get; protected set; }
		public int Rows { get; protected set; }
		public int WinCondition { get; protected set; }
		private int WinCheckCount1 { get; set; }
		private int WinCheckCount2 { get; set; }
		public int moveCount { get; protected set; }
		public List<List<string>> Matrix
		{
			get
			{
				return matrix;
			}
			protected set
			{
				matrix = value;
			}
		}
		private Player P1 { get; set; }
		private Player P2 { get; set; }

		// Constructor
		public Grid(int cols_, int rows_, Player p1, Player p2)
		{
			Cols = cols_;
			Rows = rows_;
			WinCondition = (int)(Cols * Rows * 0.1);
			P1 = p1;
			P2 = p2;
			moveCount = 0;
			WinCheckCount1 = 1;
			WinCheckCount2 = 1;

			for (int i = 0; i < Cols; i++)
			{
				List<string> newCol = new();
				for (int j = 0; j < Rows; j++)
				{
					newCol.Add(" ");
				}
				Matrix.Add(newCol);
			}

			Util.LogString($"WinCondition: {WinCondition}");
		}

		// Methods
		public void DisplayGrid(bool test = false)
		/*
		Clear console
		Display grid, top-down, with delay
		*/
		{
			Console.Clear();
			string displayGrid = "";
			for (int r = Rows - 1; r >= 0; r--)
			// Printing top-down
			{
				displayGrid += string.Format("{0,-3}", r + 1);
				for (int c = 0; c < Cols; c++)
				{
					displayGrid += $"| {Matrix[c][r]} ";
				}
				displayGrid += "|\n";
			}
			displayGrid += string.Format("{0,-3}", " "); ;
			for (int c = 0; c < Cols; c++)
			{
				if (c < 10) displayGrid += $"  {c + 1} ";
				else displayGrid += $" {c + 1} ";
			}
			Console.WriteLine($"{displayGrid}\n");
			if (test)
			{
				Thread.Sleep(DELAY_TEST);
			}
			else
			{
				Thread.Sleep(DELAY);
			}
		}

		public void UpdateGrid(string move = "", bool test = false)
		/*
		Separate disc type and column number from input
		Find column and empty cell to place disc
		Run Special Update if it's special disc
		*/
		{
			moveCount += 1;
			int player = (moveCount - 1) % 2;
			Util.LogString($"moveCount: {moveCount}");

			string disc = move.Substring(0, 1).ToLower();
			int playedCol = int.Parse(move.Substring(1));

			// Drop disc
			for (int r = 0; r < Rows; r++)
			{
				if (Matrix[playedCol - 1][r] != " ") continue;
				Matrix[playedCol - 1][r] = discDict[player][disc];
				break;
			}
			DisplayGrid(test);
			if (disc != "o") UpdateGridSpecial(player, disc, playedCol, test);
			Util.LogString($"{CheckWin()}: {WinCheckCount1}, {WinCheckCount2}");
		}
		public void UpdateGridSpecial(int player, string disc, int playedCol, bool test = false)
		/*
		Run special ability
		Run disc conversion to ordinary at the end of turn
		*/
		{
			string playedDisc = discDict[player][disc];
			switch (disc)
			{
				case "b":   // Boring Disc
					int disc1 = 0;
					int disc2 = 0;
					
					// Bore column and return discs to hand
					List<string> newColBore1 = new();
					newColBore1.Add(playedDisc);
					for (int j = 0; j < Rows; j++)
					{
						if (j != 0) newColBore1.Add(" ");
						if (Matrix[playedCol][j] == "@") disc1 += 1;
						if (Matrix[playedCol][j] == "#") disc2 += 1;
					}
					Matrix[playedCol - 1] = newColBore1;
					P1.DiscCount += disc1;
					P2.DiscCount += disc2;
					DisplayGrid(test);

					// Convert to ordinary
					Matrix[playedCol - 1][Matrix[playedCol - 1].IndexOf(playedDisc)] = discDict[player]["o"];
					DisplayGrid(test);

					break;
				case "m":   // Magnetic Disc
					// Swap nearest ally 1 position up
					for (int r = Rows - 1; r >= 0; r--)
					{
						if (Matrix[playedCol - 1][r] == " ") continue;

						if (Matrix[playedCol - 1][r] == discDict[player]["o"])
						{
							if (Matrix[playedCol - 1][r + 1] != " " & Matrix[playedCol - 1][r + 1] != playedDisc)
							{
								string temp = Matrix[playedCol - 1][r + 1];
								Matrix[playedCol - 1][r + 1] = Matrix[playedCol - 1][r];
								Matrix[playedCol - 1][r] = temp;
							}
							break;
						}
					}
					DisplayGrid(test);

					// Convert to ordinary
					Matrix[playedCol - 1][Matrix[playedCol - 1].IndexOf(playedDisc)] = discDict[player]["o"];
					DisplayGrid(test);

					break;
				case "e":   // Exploding Disc
					// TODO: if there's time
					break;

				default: break;
			}
		}

		public void TestUpdateGrid(string move_inputs = "", bool test = true)
		{
			string[] moveList = move_inputs.ToLower().Split(',');
			for (int i = 0; i < moveList.Length; i++)
			{
				UpdateGrid(moveList[i], test);
			}
		}

		public string CheckWin()
		/*
		Define the 8 directions
		Loop through 8 directions, calculate longest path of each direction
		Go through all cells
		Check winning of each cell through 8 paths/distances
		*/
		{
			int[,] direction8 = {
				{1,0},	{0,1},	{-1,0},	{0,-1},
				{-1,1},	{1,-1},	{1,1},	{-1,-1}
			};
			int longestDistance = WinCondition - 1;
			for (int i = 0; i < 8; i++)
			{
				Util.LogString($"Direction {direction8[i,0]},{direction8[i,1]}");
				int dCol = direction8[i, 0] * longestDistance;
				int dRow = direction8[i, 1] * longestDistance;

				for (int c = 0; c < Cols; c++)
				{
					for (int r = 0; r < Rows; r++)
					{
						string rootCell = Matrix[c][r];
						Util.LogString($"rootCell {c+1},{r+1}: \"{rootCell}\"");
						if (c + dCol < 0 | r + dRow < 0 | c + dCol > Cols | r + dRow > Rows) continue;
						for (int j = longestDistance - 1; j >= 0; j--)
						{
							Util.LogString($"\tDistance {longestDistance - j}");
							string comparedCell = Matrix[c + direction8[i, 0] * (longestDistance - j)][r + direction8[i, 1] * (longestDistance - j)];
							Util.LogString($"comparedCell: \"{comparedCell}\"");
							if (rootCell == " " | rootCell != comparedCell)
							{
								WinCheckCount1 = 1;
								WinCheckCount2 = 1;
								break;
							}
							if (rootCell == "@")
							{
								WinCheckCount1 += 1;
								WinCheckCount2 = 0;
							}
							if (rootCell == "#")
							{
								WinCheckCount1 = 0;
								WinCheckCount2 += 1;
							}
						}
						Util.LogString($"\tCheck done. (small)");
						Util.LogString($"\tWinCheckCount1: {WinCheckCount1}");
						Util.LogString($"\tWinCheckCount2: {WinCheckCount2}");

						if (WinCheckCount1 == WinCondition)
						{
							Console.WriteLine($"Player 1 won: \"{WinCheckCount1}\"");
							return "1";

						}
						if (WinCheckCount2 == WinCondition) 
						{
							Console.WriteLine($"Player 2 won: \"{WinCheckCount2}\"");
							return "2";

						}
					}

				}
			}
			return "";
		}
	}
	
}
