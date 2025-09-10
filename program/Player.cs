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


		// Properties
		public Dictionary<string, int> Discs
		{
			get
			{
				return discs;
			}
			set
			{
				discs["o"] = value["o"];
				if (value.TryGetValue("b", out _)) discs["b"] = value["b"];
				if (value.TryGetValue("m", out _)) discs["m"] = value["m"];
			}
		}
	}
}