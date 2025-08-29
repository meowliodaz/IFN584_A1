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


namespace Game
{
	public class Grid
	{
		// Fields
		private IReadOnlyDictionary<string, string> discs = new Dictionary<string, string>
		{
			{"o", "Ordinary"},
			{"m", "Magnetic"},
			{"b", "Boring"},
			{"e", "Exploding"}
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
			WinCondition = (int)Math.Round(Cols * Rows * 0.1, 0, MidpointRounding.AwayFromZero);

			for (int i = 0; i < Cols; i++)
			{
				// Console.WriteLine(i);
				List<string> row = new();
				for (int j = 0; j < Rows; j++)
				{
					Console.Write($" {j} ");
					row.Add(" ");
				}
				Console.WriteLine();
				Matrix.Add(row);
			}
		}

		// Methods
		public void UpdateGrid(params string[] args) // using param incase no argument is passed
		{
			for (int i = 0; i < args.Length; i++)
			{
				;
			}
		}

		public void DisplayGrid()
		{
			for (int i = 0; i < Cols; i++)
			{
				for (int j = 0; j < Rows; j++)
				{
					Console.Write($"| {Matrix[i][j]} ");
				}
				Console.WriteLine("|");
			}
			for (int i = 0; i < Cols; i++)
			{
				Console.Write($"  {i+1} ");
			}
		}
	}
	
}