namespace Connect4
{
	public class Menu
	{
		// Fields


		// Properties
		public string SaveFile { get; private set; }

		// Methods
		public Menu()
		{
			SaveFile = "saved_game.json";
		}
		public string MenuFirst()
		{
			int boxWidth = 60;
			int loopCount = 0;
			string? ErrorMessage = "";
			do
			{
				// Main Menu
				loopCount += 1;
				Console.Clear();
				Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
				Console.WriteLine(string.Format("{0,-55}|", "|\t Welcome to Connect4 - The expanded edition"));
				Console.WriteLine(string.Format("{0,-48}|", "|\t\t 1.Load game"));
				Console.WriteLine(string.Format("{0,-48}|", "|\t\t 2.New game"));
				Console.WriteLine(string.Format("{0,-48}|", "|\t\t 3.Change grid size"));
				Console.WriteLine(string.Format("{0,-48}|", "|\t\t 4.Instructions"));
				Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

				if (loopCount != 1) Console.WriteLine($"{ErrorMessage}");

				Console.WriteLine();
				Console.Write("Type in the number of your option, and press Enter: ");
				string? MainMenuChoice = Console.ReadLine();

				if (MainMenuChoice == null | !"1234".Contains(MainMenuChoice.ToLower()))
				{
					ErrorMessage = $"\n[ERROR]\t \"{MainMenuChoice}\" is not a valid input.\n\t Please input 1, 2, 3, or 4, and press Enter again!";
					continue;
				}
				else return MainMenuChoice;
			}
			while (true);
		}

		public string MenuSecond(string main_menu_choice)
		{
			int boxWidth = 60;
			switch (main_menu_choice)
			{
				case "1": // Load game
					Console.WriteLine("1");
					return SaveFile;
				case "2": // New game
					Console.WriteLine("2");
					int loopCount2 = 0;
					string? ErrorMessage2 = "";
					do
					{
						// Sub Menu 2
						loopCount2 += 1;
						Console.Clear();
						Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
						Console.WriteLine(string.Format("{0,-55}|", "|\t New game. What game mode do you want?"));
						Console.WriteLine(string.Format("{0,-48}|", "|\t\t 1.Player vs Player"));
						Console.WriteLine(string.Format("{0,-48}|", "|\t\t 2.Player vs Computer"));
						Console.WriteLine(string.Format("{0,-48}|", "|\t\t 3.Test Mode"));
						Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

						if (loopCount2 != 1) Console.WriteLine($"{ErrorMessage2}");

						Console.WriteLine();
						Console.Write("Type in the number of your option, and press Enter: ");
						string? SubMenuChoice = Console.ReadLine();

						if (SubMenuChoice == null | !"123".Contains(SubMenuChoice.ToLower()))
						{
							ErrorMessage2 = $"\n[ERROR]\t \"{SubMenuChoice}\" is not a valid input.\n\t Please input 1, 2, or 3, and press Enter again!";
							continue;
						}
						else return SubMenuChoice;
					}
					while (true);
				case "3": // Change grid size
					Console.WriteLine("3");
					int loopCount3 = 0;
					string? ErrorMessage3 = "";
					do
					{
						// Sub Menu 3
						loopCount3 += 1;
						Console.Clear();
						Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Changing grid size."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Default size is 6x7, 6 Rows and 7 Columns."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t New size can't be smaller than default."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t New size can be a square."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Number of Rows can't be bigger than of Columns."));
						Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

						if (loopCount3 != 1) Console.WriteLine($"{ErrorMessage3}");

						Console.WriteLine();
						Console.Write("New row number: ");
						string? InputRows = Console.ReadLine();
						Console.Write("New column number: ");
						string? InputCols = Console.ReadLine();

						int Rows, Cols;
						bool RowIsNum = int.TryParse(InputRows, out Rows);
						bool ColIsNum = int.TryParse(InputCols, out Cols);

						if (InputRows == null | !RowIsNum)
						{
							ErrorMessage3 = $"\n[ERROR]\t \"{InputRows}\" for row is not a valid input.\n\t Please input a number following the rule above and press Enter again!";
							continue;
						}
						else if (InputCols == null | !ColIsNum)
						{
							ErrorMessage3 = $"\n[ERROR]\t \"{InputCols}\" for column is not a valid input.\n\t Please input a number following the rule above and press Enter again!";
							continue;
						}
						else if (Rows > Cols)
						{
							ErrorMessage3 = $"\n[ERROR]\t Number of Rows is bigger than number of Columns.\n\t Please input a number following the rule above and press Enter again!";
							continue;
						}
						else if (Rows < 6 | Cols < 7)
						{
							ErrorMessage3 = $"\n[ERROR]\t New size is smaller than default.\n\t Please input a number following the rule above and press Enter again!";
							continue;
						}
						else return $"{Rows}x{Cols}";
					}
					while (true);
				case "4": // Instructions
					Console.WriteLine("4");
					// Sub Menu 3
					boxWidth = 90;
					Console.Clear();
					Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t Instructions"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t The game follows basic rule of Connect4, except..."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t SPECIAL DISCS!!"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t There are 2 types of special discs: Boring, and Magnetic."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t Boring Disc drills through the column, lands on the lowest row,"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t      and turn into an ordinary disc."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t      All discs that is bored through will return to the respective player."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t Magnetic Disc seek its highest ally in the column,"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t      exchange the ally with the disc right above said ally,"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t      and turn itself into an ordinary disc."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t To play a normal, ordinary disc: o7 => o is the disc, 7 is the column."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t To play a boring disc: b1 => b is the disc, 1 is the column."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t To play a magnetic disc: m14 => m is the disc, 14 is the column."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t Discs display in game for PLAYER 1:"));
					Console.WriteLine(string.Format("{0,-77}|", "|\t Ordinary: @\t Boring: B\t Magnetic: M"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t Discs display in game for PLAYER 2:"));
					Console.WriteLine(string.Format("{0,-77}|", "|\t Ordinary: #\t Boring: b\t Magnetic: m"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t Winning condition changes depending on grid size."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t      [Winning condition] = [Number of Rows] x [Number of Columns] x 0.1"));
					Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

					Console.Write("Press Enter to return to Main Menu!");
					Console.Read();

					return "4";
				default:
					return "4";
			}
		}
	}
}