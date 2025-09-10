namespace Connect4
{
	public class Program
	{
		static void Main()
		{
			Util.DeleteLog("string.log");
			Computer P1 = new(99);
			Player P2 = new(1);

			Grid grid = new(7, 6, P1, P2);



			// Test case 1
			string play1 = "o1,o1,o1,o2,m1,b1,o1,o3,o1,o4";
			// Test case 2
			string play2 = "o1,o2,o1,o2,o1,o2,o1,o2";
			// Test case 3
			string play3 = "o1,o2,o2,o3,o3,o4,o3,o4,o5,o4,o4";
			// Test case 4
			string play4 = "o2,o2,o2,o2,o3,o4,o3,o3,o1,o4,o1,o5";
			grid.TestUpdateGrid(play2);
			
			P1.setGridCopy(grid.Matrix);
			P1.logGridCopy();
		}
	}
}