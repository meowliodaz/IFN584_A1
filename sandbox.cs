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
    private List<List<string>> matrix;

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
            for (int j = 0; j < Rows; i++)
            {
                Matrix[i][j] = " ";
            }
        }
    }
}

public class Program
{
    public void main()
    {
        Grid grid = new();
        print(grid.Matrix);
    }
}