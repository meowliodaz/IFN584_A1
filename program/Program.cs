namespace Connect4
{
	public class Program
	{
		static void Main()
		{
			Util.DeleteLog("string.log");
			Grid grid = new(7, 6);
			// Test case 1
			string play1 = "o1,o1,o1,o2,m1,b1,o1,o3,o1,o4";
			// Test case 2
			string play2 = "o1,o2,o1,o2,o1,o2,o1,o2";
			// Test case 3
			string play3 = "o1,o2,o2,o3,o3,o4,o3,o4,o5,o4,o4";
			// Test case 4
			string play4 = "o2,o2,o2,o2,o3,o4,o3,o3,o1,o4,o1,o5";
			// Test case 5
			string play5 = "o1,o2,o1,o2,o1,o2,o1,o2";
			// Test case 6
			string play6 = "o1,o2,o1,o2,o1,o2,o1,o2";
			// Test case 7
			string play7 = "o1,o2,o1,o2,o1,o2,o1,o2";
			grid.TestUpdateGrid(play4);
		}
	}
}