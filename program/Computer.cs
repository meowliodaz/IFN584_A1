namespace Connect4
{
	public class Computer : Player
	{
		private List<List<string>> GridCopy = new();
		private int GridCols = 0;
		private int GridRows = 0;
		public Computer(int start_discs) : base(start_discs)
		{
			Util.LogString($"Computer: disc {Discs["o"]}");
		}

		public void setGridCopy(List<List<string>> grid_)
		{
			GridCopy = grid_;
			GridCols = GridCopy.Count;
			GridRows = GridCopy[0].Count;
		}
		public void logGridCopy()
		{
			Util.LogMatrix(GridCopy);
		}
		public string PlayToWin()
		{
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
			foreach (KeyValuePair<string, int> disc in Discs)
			{
				if (disc.Value > 0) discList.Add(disc.Key);
			}
			move += discList[rnd.Next(0, discList.Count)];
			List<int> colList = new();
			for (int c = 0; c < GridCols; c++)
			{
				if (GridCopy[c].IndexOf(" ") != -1) colList.Add(c);
			}
			move += $"{colList[rnd.Next(0, colList.Count)]}";

			return move;
		}
    }
}