
namespace Connect4
{
	public class Player
	{
		// Fields
		protected Dictionary<string, int> discs = new Dictionary<string, int>
		{
			{"o", 0},
			{"b", 2},
			{"m", 2}
		};
		protected IReadOnlyDictionary<string, string>[] discTypes =
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
		protected IReadOnlyDictionary<string, string> discDict = new Dictionary<string, string>();


		// Properties
		public Dictionary<string, int> Discs
		{
			get
			{
				return discs;
			}
			set
			{
				if (value.TryGetValue("o", out _))
				{
					discs["o"] = value["o"];
				}
				if (value.TryGetValue("b", out _))
				{
					discs["b"] = value["b"];
				}
				if (value.TryGetValue("m", out _))
				{
					discs["m"] = value["m"];
				}
			}
		}
		public int ID { get; protected set; }

		// Constructor
		public Player()
		{
		}

		// Methods
		public void InitiatePlayer(int start_discs, int player_position)
		{
			Discs = new Dictionary<string, int>() { { "o", start_discs } };
			ID = player_position;
			discDict = discTypes[ID - 1];
		}
    }
}