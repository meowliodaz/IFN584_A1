namespace Connect4
{
	public class Computer : Player
	{
		private List<List<string>> GridCopy = new();
		public Computer(int start_discs) : base(start_discs)
		{
			Util.LogString($"Computer: disc {Discs["o"]}");
		}

		public void setGridCopy(List<List<string>> grid_)
		{
			GridCopy = grid_;
		}
		public void logGridCopy()
		{
			Util.LogMatrix(GridCopy);
		}
    }
}