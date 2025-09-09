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
using System.Runtime.InteropServices;


namespace Connect4
{
	public class Grid
	{
		// Fields
		private const int DELAY = 1000;			// milisecond
		private const int DELAY_TEST = 500;	// milisecond
		
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

		// Constructor
		public Grid(int cols_, int rows_)
		{
			moveCount = 0;
			Cols = cols_;
			Rows = rows_;
			WinCondition = (int)(Cols * Rows * 0.1);

			for (int i = 0; i < Cols; i++)
			{
				List<string> newCol = new();
				for (int j = 0; j < Rows; j++)
				{
					newCol.Add(" ");
				}
				Matrix.Add(newCol);
			}
		}

		// Methods
		public void DisplayGrid(bool test = false)
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
				Util.LogString("Test");
				Thread.Sleep(DELAY_TEST);
			}
			else
			{
				Util.LogString("Play");
				Thread.Sleep(DELAY);
			}
		}

		public void UpdateGrid(string move = "", bool test = false)
		/*
		
		*/
		{
			int player = moveCount % 2;
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
			moveCount += 1;
		}
		public void UpdateGridSpecial(int player, string disc, int playedCol, bool test = false)
		/*
		
		*/
		{
			string playedDisc = discDict[player][disc];
			switch (disc)
			{
				case "b":   // Boring Disc
							// Bore column
					List<string> newColBore1 = new();
					newColBore1.Add(playedDisc);
					for (int j = 1; j < Rows; j++)
					{
						newColBore1.Add(" ");
					}
					Matrix[playedCol - 1] = newColBore1;
					DisplayGrid(test);

					// Convert to ordinary
					List<string> newColBore2 = new();
					newColBore2.Add(discDict[player]["o"]);
					for (int j = 1; j < Rows; j++)
					{
						newColBore2.Add(" ");
					}
					Matrix[playedCol - 1] = newColBore2;
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
	}
	
}