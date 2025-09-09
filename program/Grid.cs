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
			UpdateGrid(2,"o2");
			UpdateGrid(1,"m1");
		}

		// Methods
		public void DisplayGrid(bool test=false)
		{
			string displayGrid = "";
			for (int r = Rows - 1; r >= 0; r--)
			// Printing top-down
			{
				displayGrid += string.Format("{0,-3}", r+1);
				for (int c = 0; c < Cols; c++)
				{
					displayGrid += $"| {Matrix[c][r]} ";
				}
				displayGrid += "|\n";
			}
			displayGrid += string.Format("{0,-3}", " ");;
			for (int c = 0; c < Cols; c++)
			{
				if (c < 10) displayGrid += $"  {c + 1} ";
				else displayGrid += $" {c + 1} ";
			}
			Console.WriteLine($"{displayGrid}\n");
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
					// Drop disc
					for (int r = 0; r < Rows; r++)
					{
						if (Matrix[playedCol - 1][r] != " ") continue;
						Matrix[playedCol - 1][r] = playedDisc;
						break;
					}
					DisplayGrid(test);

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
					newColBore2.Add(discDict[player - 1]["o"]);
					for (int j = 1; j < Rows; j++)
					{
						newColBore2.Add(" ");
					}
					Matrix[playedCol - 1] = newColBore2;
					DisplayGrid(test);

					break;
				case "m":   // Magnetic Disc
							// Drop disc
					int playedRow = 0;
					for (int r = 0; r < Rows; r++)
					{
						if (Matrix[playedCol - 1][r] != " ") continue;
						Matrix[playedCol - 1][r] = playedDisc;
						playedRow = r+1;
						break;
					}
					DisplayGrid(test);

					// Swap nearest ally 1 position up
					for (int r = Rows-1; r >= 0; r--)
					{
						if (Matrix[playedCol - 1][r] == " ") continue;

						if (
							(Matrix[playedCol - 1][r] == discDict[player - 1]["o"])
							& Matrix[playedCol - 1][r+1] != " "
							& Matrix[playedCol - 1][r+1] != playedDisc
						)
						{
							string temp = Matrix[playedCol - 1][r + 1];
							Matrix[playedCol - 1][r + 1] = Matrix[playedCol - 1][r];
							Matrix[playedCol - 1][r] = temp;
							break;
						}
					}
					DisplayGrid(test);

					// Convert to ordinary
					Matrix[playedCol - 1][Matrix[playedCol - 1].IndexOf(playedDisc)] = discDict[player - 1]["o"];
					DisplayGrid(test);
					
					break;
				case "e":   // Exploding Disc
					break;
				default: break;
			}
		}

		public void TestUpdateGrid(string move_inputs="", bool test=true)
		{

		}
	}
	
}