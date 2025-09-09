// PROGRESS
/* TODO:
	- Update grid
	- Implement disc behavior? 
*/



// Built-in libraries


// 3rd party libraries


// Namespace


using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Connect4
{
	public class Grid
	{
		// Fields
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
			UpdateGrid(1,"o1");
			UpdateGrid(2,"o1");
			UpdateGrid(1,"o1");
			UpdateGrid(2,"o1");
			UpdateGrid(1,"b1");
			UpdateGrid(2,"b1");
		}

		// Methods
		public void DisplayGrid(bool test=false)
		{
			for (int r = Rows-1; r >= 0; r--)
			// Printing top-down
			{
				for (int c = 0; c < Cols; c++)
				{
					Console.Write($"| {Matrix[c][r]} ");
				}
				Console.WriteLine("|");
			}
			for (int c = 0; c < Cols; c++)
			{
				Console.Write($"  {c + 1} ");
			}
			Console.WriteLine("\n");
		}

		public void UpdateGrid(int player, string move = "", bool test=false)
		/*
		
		*/
		{
			string disc = move.Substring(0, 1).ToLower();
			int playedCol = int.Parse(move.Substring(1));
			// Console.WriteLine(disc);
			// Console.WriteLine(playedCol);

			if (disc != "o") UpdateGridSpecial(player, disc, playedCol);
			else
			{
				for (int r = 0; r < Rows; r++)
				{
					if (Matrix[playedCol - 1][r] != " ") continue;
					Matrix[playedCol - 1][r] = discDict[player-1][disc];
					break;
				}
				DisplayGrid(test);
			}
		}
		public void UpdateGridSpecial(int player, string disc, int playedCol, bool test=false)
		/*
		
		*/
		{
			string playedDisc = discDict[player-1][disc];
			switch (disc)
			{
				case "b":   // Boring Disc
					{
						for (int r = 0; r < Rows; r++)
						{
							if (Matrix[playedCol - 1][r] != " ") continue;
							Matrix[playedCol - 1][r] = playedDisc;
							break;
						}
						DisplayGrid(test);

						List<string> newCol = new();
						newCol.Add(playedDisc);
						for (int j = 1; j < Rows; j++)
						{
							newCol.Add(" ");
						}
						Matrix[playedCol - 1] = newCol;
						DisplayGrid(test);

						List<string> newCol2 = new();
						newCol2.Add(discDict[player - 1]["o"]);
						for (int j = 1; j < Rows; j++)
						{
							newCol2.Add(" ");
						}
						Matrix[playedCol - 1] = newCol2;
						DisplayGrid(test);

						break;
					}
				case "m":   // Magnetic Disc
					{
						break;
					}
				case "e":   // Exploding Disc
					{
						break;
					}
				default: break;
			}
		}

		public void TestUpdateGrid(string move_inputs="", bool test=true)
		{

		}
	}
	
}