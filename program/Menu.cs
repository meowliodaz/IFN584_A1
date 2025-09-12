using System.Text.Json;

namespace Connect4
{
	public class Menu
	{
		// Fields


		// Properties
		public string SavedGrid { get; private set; }
		public string SavedP1 { get; private set; }
		public string SavedP2 { get; private set; }
		public string SavedGameMode { get; private set; }
		public string SavedMoveCountMode { get; private set; }

		// Methods
		public Menu()
		{
			SavedGrid = "saved_grid.json";
			SavedP1 = "saved_p1.json";
			SavedP2 = "saved_p2.json";
			SavedGameMode = "saved_game_mode.json";
			SavedMoveCountMode = "saved_move_count.json";
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
				Console.WriteLine(string.Format("{0,-48}|", "|\t\t 5.Quit game"));
				Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

				if (loopCount != 1) Console.WriteLine($"{ErrorMessage}");

				Console.WriteLine();
				Console.Write("Type in the number of your option, and press Enter: ");
				string? MainMenuChoice = Console.ReadLine();

				if (MainMenuChoice == null | !"12345".Contains(MainMenuChoice.ToLower()))
				{
					ErrorMessage = $"\n[ERROR]\t \"{MainMenuChoice}\" is not a valid input.\n\t Please input 1, 2, 3, 4, or 5, and press Enter again!";
					continue;
				}
				else if (MainMenuChoice.ToLower() == "1" )
				{
					LoadFile OldGame = LoadGame();
					ErrorMessage = $"\n[ERROR]\t \"Load file does not exist. Please start a New Game!";
				}
				else return MainMenuChoice;
			}
			while (true);
		}

		public string? MenuSecond(string main_menu_choice)
		{
			int boxWidth = 60;
			switch (main_menu_choice)
			{
				case "1": // Load game
					return "Load";
				case "2": // New game
					int loopCount2 = 0;
					string? ErrorMessage2 = "";
					do
					{
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
					int loopCount3 = 0;
					string? ErrorMessage3 = "";
					do
					{
						loopCount3 += 1;
						Console.Clear();
						Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Changing grid size."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Default size is 6x7, 6 Rows and 7 Columns."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t New size can't be smaller than default."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t New size can be a square."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t Number of Rows can't be bigger than of Columns."));
						Console.WriteLine(string.Format("{0,-55}|", "|\t If you want to change grid size to default,"));
						Console.WriteLine(string.Format("{0,-55}|", "|\t      don't type in anything and press Enter."));
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
						
						if (InputRows == null | InputCols == null | InputRows == "" | InputCols == "")
						{
							return $"6x7";
						}
						else if (!RowIsNum)
						{
							ErrorMessage3 = $"\n[ERROR]\t \"{InputRows}\" for row is not a valid input.\n\t Please input a number following the rule above and press Enter again!";
							continue;
						}
						else if (!ColIsNum)
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
					boxWidth = 90;
					Console.Clear();
					Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t Instructions"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t The game follows basic rule of Connect4, except..."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t SPECIAL DISCS!!"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t There are 2 types of special discs: Boring, and Magnetic."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-89}|", "|Boring Disc \t Drills through the column, lands on the lowest row,"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t and turn into an ordinary disc."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t All discs that is bored through will return to the respective player."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-91}|", "|Magnetic Disc \t Seek its highest ally in the column,"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t exchange the ally with the disc right above said ally,"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t and turn itself into an ordinary disc."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-86}|", "|Gameplay \t To play a normal, ordinary disc: o7 => o is the disc, 7 is the column."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t To play a boring disc: b1 => b is the disc, 1 is the column."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t To play a magnetic disc: m14 => m is the disc, 14 is the column."));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|Display \t Discs display in game for PLAYER 1:"));
					Console.WriteLine(string.Format("{0,-70}|", "|\t\t Ordinary: @\t Boring: B\t Magnetic: M"));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t Discs display in game for PLAYER 2:"));
					Console.WriteLine(string.Format("{0,-70}|", "|\t\t Ordinary: #\t Boring: b\t Magnetic: m"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-85}|", "|Winning \t Winning condition changes depending on grid size."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t [Winning condition] = [Number of Rows] x [Number of Columns] x 0.1"));
					Console.WriteLine(string.Format("{0,-85}|", "|\t"));
					Console.WriteLine(string.Format("{0,-90}|", "|Game options \t To save game, type \"s\" then Enter."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t to quit game, type \"q\" then Enter."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t to save and quit, type \"sq\" then Enter."));
					Console.WriteLine(string.Format("{0,-78}|", "|\t\t to go back to main menu, type \"mm\" then Enter."));
					Console.WriteLine(string.Format("o{0}o", new string('-', boxWidth)));

					Console.Write("Press Enter to return to Main Menu!");
					Console.Read();

					return "4";
				case "5": // Quit
					Environment.Exit(0);
					return "Quit";
				default:
					return null;
			}
		}

		public void SaveGame(List<List<string>> grid, Player p1, Player p2, int gameMode_, int moveCount_)
		/*
		Saving game stat into 3 separate files
		*/
		{
			// Save grid
			var SavedGridJson = JsonSerializer.Serialize(grid);
			Util.LogFile(SavedGrid, SavedGridJson);

			// Save player 1
			var SavedP1Json = JsonSerializer.Serialize(p1);
			Util.LogFile(SavedP1, SavedP1Json);

			// Save player 2
			var SavedP2Json = JsonSerializer.Serialize(p2);
			Util.LogFile(SavedP2, SavedP2Json);

			// Save game mode
			var SavedGameModeJson = JsonSerializer.Serialize(gameMode_);
			Util.LogFile(SavedGameMode, SavedGameModeJson);

			// Save move count
			var SavedMoveCountJson = JsonSerializer.Serialize(moveCount_);
			Util.LogFile(SavedMoveCountMode, SavedMoveCountJson);
		}

		public LoadFile LoadGame()
		/*
		Load previous game from 3 seperate files
		*/
		{
			LoadFile OldGame = new();
			string LoadGridJson;
			string LoadP1Json;
			string LoadP2Json;
			string LoadGameModeJson;
			string LoadMoveCountJson;

			// Check files existence
			string[] SavedFiles = [SavedGrid, SavedP1, SavedP2, SavedGameMode, SavedMoveCountMode];
			foreach (string filename in SavedFiles)
			{
				if (!File.Exists(filename))
				{
					LoadFile NonExistenceGame = new();
					NonExistenceGame.FileExist = false;
					return NonExistenceGame;
				}
			}
			
			// Load grid
			using (StreamReader LoadGrid = new StreamReader(SavedGrid))
			{
				LoadGridJson = LoadGrid.ReadToEnd();
				LoadGrid.Close();
			}
			OldGame.grid = JsonSerializer.Deserialize<List<List<string>>>(LoadGridJson);

			// Load player 1
			using (StreamReader LoadP1 = new StreamReader(SavedP1))
			{
				LoadP1Json = LoadP1.ReadToEnd();
				LoadP1.Close();
			}
			OldGame.p1 = JsonSerializer.Deserialize<PlayerLoadFile>(LoadP1Json);

			// Load player 2
			using (StreamReader LoadP2 = new StreamReader(SavedP2))
			{
				LoadP2Json = LoadP2.ReadToEnd();
				LoadP2.Close();
			}
			OldGame.p2 = JsonSerializer.Deserialize<PlayerLoadFile>(LoadP2Json);

			// Load game mode
			using (StreamReader LoadGameMode = new StreamReader(SavedGameMode))
			{
				LoadGameModeJson = LoadGameMode.ReadToEnd();
				LoadGameMode.Close();
			}
			OldGame.gameMode = JsonSerializer.Deserialize<int>(LoadGameModeJson);

			// Load game mode
			using (StreamReader LoadMoveCount = new StreamReader(SavedMoveCountMode))
			{
				LoadMoveCountJson = LoadMoveCount.ReadToEnd();
				LoadMoveCount.Close();
			}
			OldGame.moveCount = JsonSerializer.Deserialize<int>(LoadMoveCountJson);

			return OldGame;
		}
		
	}
}