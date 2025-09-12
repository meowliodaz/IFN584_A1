using System.Text.Json;

namespace Connect4
{
	public class Computer : Player
	{
		// Fields
		private List<List<string>> GridCopy = new();
		private int GridCols = 0;
		private int GridRows = 0;
		private int WinCondition = 0;
		private int WinCheckCount = 0;

		// Constructor
		

		// Methods
		public void setGridCopy(List<List<string>> grid_)
		{
			for (int c = 0; c < grid_.Count; c++)
			{
				GridCopy.Add(grid_[c]);
			}
			GridCols = GridCopy.Count;
			GridRows = GridCopy[0].Count;
			WinCondition = (int)(GridCols * GridRows * 0.1);
		}
		public void logGridCopy()
		{
			Util.LogMatrix(GridCopy);
		}
		public string PlayToWin()
		/*
		Run through all columns, check for viable move
		Check if that move leads to an immediate win
		If no win move exists => Play random
		*/
		{
			for (int c = 0; c < GridCols; c++)
			{
				for (int r = 0; r < GridRows; r++)
				{
					if (GridCopy[c][r] != " ") continue;
					GridCopy[c][r] = discDict["o"];
					
					if (CheckWinComputer())
					{
						GridCopy[c][r] = " ";
						return $"o{c + 1}";
					}
					GridCopy[c][r] = " ";
					break;
				}

			}
			return PlayRandom();
		}
		public string PlayRandom()
		/*
		Check for remaining discs of each type => randomly choose 1 viable disc
		Check for viable columns => Randomly choose 1 column for placement
		*/
		{
			Random rnd = new();
			string move = "";
			List<string> discList = new();
			for (int d = 0; d < Discs["o"]; d++)
			{
				discList.Add("o");
			}
			for (int d = 0; d < Discs["b"]; d++)
			{
				discList.Add("b");
			}
			for (int d = 0; d < Discs["m"]; d++)
			{
				discList.Add("m");
			}
			move += discList[rnd.Next(0, discList.Count)];

			List<int> colList = new();
			for (int c = 0; c < GridCols; c++)
			{
				if (GridCopy[c].IndexOf(" ") != -1) colList.Add(c+1);
			}
			move += $"{colList[rnd.Next(0, colList.Count)]}";

			return move;
		}
		private bool CheckWinComputer()
		{
			int[,] direction8 = {
				{1,0},	{0,1},	{-1,0},	{0,-1},
				{-1,1},	{1,-1},	{1,1},	{-1,-1}
			};
			int longestDistance = WinCondition - 1;
			for (int i = 0; i < 8; i++)
			{
				int dCol = direction8[i, 0] * longestDistance;
				int dRow = direction8[i, 1] * longestDistance;

				for (int c = 0; c < GridCols; c++)
				{
					for (int r = 0; r < GridRows; r++)
					{
						if (c + dCol < 0 | r + dRow < 0 | c + dCol >= GridCols | r + dRow >= GridRows) continue;
						string rootCell = GridCopy[c][r];
						for (int j = longestDistance - 1; j >= 0; j--)
						{
							int cellCol = c + direction8[i, 0] * (longestDistance - j);
							int cellRow = r + direction8[i, 1] * (longestDistance - j);
							string comparedCell = GridCopy[cellCol][cellRow];
							if (rootCell == " " | rootCell != comparedCell)
							{
								WinCheckCount = 1;
								break;
							}
							if (rootCell == "#") WinCheckCount += 1;
						}
						if (WinCheckCount == WinCondition) return true;
					}
				}
			}
			return false;
		}
    }
}