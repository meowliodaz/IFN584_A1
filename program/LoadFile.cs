namespace Connect4
{
    public class LoadFile
    {
        // Fields
        public List<List<string>> grid;
        public PlayerLoadFile p1;
        public PlayerLoadFile p2;
        public int gameMode;
        public int moveCount;

	}
	    public class Discs
    {
        public int o { get; set; }
        public int b { get; set; }
        public int m { get; set; }
    }
    public class PlayerLoadFile
    {
        public Discs Discs { get; set; }
        public int ID { get; set; }
    }
}