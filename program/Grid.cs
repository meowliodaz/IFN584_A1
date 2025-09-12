namespace Connect4
{
	public class Grid
	{
		// Fields
		private const int DELAY = 500;     // milisecond
		private const int DELAY_TEST = 200; // milisecond
		private IReadOnlyDictionary<string, string>[] discDict =
		[
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
		];
		private List<List<string>> matrix = new();

		// Properties
		public int Cols { get; protected set; }
		public int Rows { get; protected set; }
		public int WinCondition { get; protected set; }
		private int WinCheckCount1 { get; set; }
		private int WinCheckCount2 { get; set; }
		public int moveCount { get; protected set; }
		public Player[] playerList = new Player[2];
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

		public Player[] PlayerList
		{
			get
			{
				return playerList;
			}
			protected set
			{
				playerList[0] = value[0];
				playerList[1] = value[1];
			}
		}

		// Constructor
		public Grid()
		{
			moveCount = 1;
			WinCheckCount1 = 1;
			WinCheckCount2 = 1;
		}

		// Methods
		public void GenerateGrid(int cols_, int rows_)
		{
			Matrix = [];
			moveCount = 1;
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
		public void LoadOldGrid(List<List<string>> grid_, int moveCount_)
		/*
		Load previous game's grid to continue game play
		*/
		{
			// TODO
			Matrix = grid_;
			Cols = grid_.Count;
			Rows = grid_[0].Count;
			WinCondition = (int)(Cols * Rows * 0.1);

			moveCount = moveCount_;

		}
		public void DisplayGrid(bool test = false)
		/*
		Clear console
		Display grid, top-down, with delay
		*/
		{
			Console.Clear();
			Console.WriteLine();
			string displayGrid = "";
			for (int r = Rows - 1; r >= 0; r--)
			// Printing top-down
			{
				displayGrid += string.Format("\t{0,-3}", r + 1);
				for (int c = 0; c < Cols; c++)
				{
					displayGrid += $"| {Matrix[c][r]} ";
				}
				displayGrid += "|\n";
			}
			displayGrid += string.Format("\t{0,-3}", " "); ;
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
			int player = (moveCount - 1) % 2;

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
		Run special ability
		Run disc conversion to ordinary at the end of turn
		*/
		{
			string playedDisc = discDict[player][disc];
			switch (disc)
			{
				case "b":   // Boring Disc
					int disc1 = PlayerList[0].Discs["o"];
					int disc2 = PlayerList[1].Discs["o"];

					// Bore column and return discs to hand
					List<string> newColBore1 = new();
					newColBore1.Add(playedDisc);
					for (int j = 0; j < Rows; j++)
					{
						if (j != 0) newColBore1.Add(" ");
						if (Matrix[playedCol - 1][j] == "@") disc1 += 1;
						if (Matrix[playedCol - 1][j] == "#") disc2 += 1;
					}
					Matrix[playedCol - 1] = newColBore1;
					PlayerList[0].Discs = new Dictionary<string, int>() { { "o", disc1 } };
					PlayerList[1].Discs = new Dictionary<string, int>() { { "o", disc2 } };
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

		public string TestUpdateGrid(string move_inputs = "", bool test = true)
		{
			string[] moveList = move_inputs.ToLower().Split(',');
			for (int i = 0; i < moveList.Length; i++)
			{
				UpdateGrid(moveList[i], test);
				if (CheckWin() != "") break;
			}
			return CheckWin();
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
				{1,0},  {0,1},  {-1,0}, {0,-1},
				{-1,1}, {1,-1}, {1,1},  {-1,-1}
			};
			int longestDistance = WinCondition - 1;
			for (int i = 0; i < 8; i++)
			{
				int dCol = direction8[i, 0] * longestDistance;
				int dRow = direction8[i, 1] * longestDistance;

				for (int c = 0; c < Cols; c++)
				{
					for (int r = 0; r < Rows; r++)
					{
						string rootCell = Matrix[c][r];
						if (c + dCol < 0 | r + dRow < 0 | c + dCol >= Cols | r + dRow >= Rows) continue;
						for (int j = longestDistance - 1; j >= 0; j--)
						{
							string comparedCell = Matrix[c + direction8[i, 0] * (longestDistance - j)][r + direction8[i, 1] * (longestDistance - j)];
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

						if (WinCheckCount1 == WinCondition)
						{
							return "1";

						}
						if (WinCheckCount2 == WinCondition)
						{
							return "2";
						}
					}
				}
			}
			return "";
		}
		public void AddPlayers(Player p1, Player p2)
		{
			PlayerList = [p1, p2];
		}

		public bool OutOfDiscs(int playerId_)
		{
			if (PlayerList[playerId_].Discs["o"] <= 0
				& PlayerList[playerId_].Discs["b"] <= 0
				& PlayerList[playerId_].Discs["m"] <= 0) return true;

			return false;
		}
		public bool OutOf1Disc(string disc_, int playerId_)
		{
			if (PlayerList[playerId_].Discs[disc_] <= 0) return true;
			return false;
		}
		public bool ColumnFull(int playedCol_)
		{
			// If column has no empty cell => Column is full
			if (Matrix[playedCol_ - 1].IndexOf(" ") == -1) return true;
			return false;
		}

	}
}
