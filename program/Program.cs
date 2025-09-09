namespace Connect4
{
	public class Program
	{
		static void Main()
		{
			Grid grid = new(10, 9);
			string play = "o1,o1,o1,o2,m1,b1";
			grid.TestUpdateGrid(play);
		}
	}
}