using System.Text.Json;

namespace Connect4
{
	public partial class Program
	{
		static void Main()
		{
			bool run = true;
			do
			{
				run = Run();
			}
			while (run);
		}

		static bool Run()
		{
			Util.DeleteLog("string.log");
			int rows = 6;
			int cols = 7;

			Grid grid = new();
			Computer[] GamePlayers = [new Computer(), new Computer()];
			int gameMode = 0;
			string winner = "";

			// Menu
			Menu GameMenu = new();
			string? UserSubMenuChoice;
			string? UserMainMenuChoice;
			// Menu loop
			do
			{
				UserMainMenuChoice = GameMenu.MenuFirst();
				UserSubMenuChoice = GameMenu.MenuSecond(UserMainMenuChoice);
				Util.LogString($"|{UserSubMenuChoice}|");
				if (UserMainMenuChoice == "3")
				{
					string[] RxC = UserSubMenuChoice.Split("x");
					rows = int.Parse(RxC[0]);
					cols = int.Parse(RxC[1]);
				}
			}
			while (UserSubMenuChoice == "4" | UserSubMenuChoice == null | UserSubMenuChoice == "" | UserMainMenuChoice == "3");

			// Load game
			if (UserSubMenuChoice == "Load")
			{
				LoadFile OldGame = GameMenu.LoadGame();

				// Load players
				GamePlayers[0].InitiatePlayer(OldGame.p1.Discs.o, OldGame.p1.ID);
				Dictionary<string, int> discs1 = new()
				{
					{"o", OldGame.p1.Discs.o},
					{"b", OldGame.p1.Discs.b},
					{"m", OldGame.p1.Discs.m}
				};
				GamePlayers[0].Discs = discs1;

				GamePlayers[1].InitiatePlayer(OldGame.p2.Discs.o, OldGame.p2.ID);
				Dictionary<string, int> discs2 = new()
				{
					{"o", OldGame.p2.Discs.o},
					{"b", OldGame.p2.Discs.b},
					{"m", OldGame.p2.Discs.m}
				};
				GamePlayers[1].Discs = discs2;

				// Load grid
				grid.LoadOldGrid(OldGame.grid, OldGame.moveCount);
				grid.AddPlayers(GamePlayers[0], GamePlayers[1]);

				// Load game mode
				gameMode = OldGame.gameMode;
			}
			// New game => Configure game mode
			else if ("123".Contains(UserSubMenuChoice.ToLower()))
			{
				gameMode = int.Parse(UserSubMenuChoice);

				// Initiate players
				GamePlayers[0].InitiatePlayer((int)(cols * rows * 0.5), 1);
				GamePlayers[1].InitiatePlayer((int)(cols * rows * 0.5), 2);

				// Generate grid
				grid.GenerateGrid(cols, rows);
				grid.AddPlayers(GamePlayers[0], GamePlayers[1]);

			}

			// Game loop
			bool playAgain = true;
			do
			{
				grid.DisplayGrid();
				string ErrorMessage = "";
				do
				{
					// Test mode
					bool testMode = false;
					string ErrorTestMessage = "";
					if (UserSubMenuChoice.ToLower() == "3")
					{
						testMode = true;
						do
						{
							if (ErrorTestMessage != "")
							{
								Console.WriteLine();
								Console.WriteLine(ErrorTestMessage);
								ErrorTestMessage = "";
							}
							Console.WriteLine();
							Console.WriteLine("This is test mode.");
							Console.WriteLine("Input a string of multiple moves to see a series of play.");
							Console.WriteLine("Example sequence: o1,o5,o3,m4,b7");
							Console.WriteLine();
							Console.Write("Your sequence: ");
							string? sequence = Console.ReadLine();

							if (sequence == null | sequence == "")
							{
								ErrorTestMessage = "[ERROR] Test sequence is empty. Please input again.\n";
								continue;
							}

							sequence = sequence.Replace(" ", string.Empty).ToLower();

							foreach (string testMove in sequence.Split(","))
							{
								if (!"sqobmm".Contains(testMove.Substring(0, 1).ToLower()))
									ErrorTestMessage = $"[ERROR] Wrong input format. 1st character need to be \"o\",\"b\", or \"m\". Please input a new move!\n";
								else if (!int.TryParse(testMove.Substring(1), out _))
									ErrorTestMessage = $"[ERROR] Wrong input format. From the 2nd character onwards, there can only be number. Please input a new move!\n";
								else if (grid.OutOf1Disc(testMove.Substring(0, 1), (grid.moveCount - 1) % 2))
									ErrorTestMessage += $"[ERROR] Invalid move. Your disc {testMove[0]} is 0. Please play a different disc!\n";
								// 		Out of bound
								else if (int.Parse(testMove.Substring(1)) <= 0 | int.Parse(testMove.Substring(1)) > grid.Cols)
									ErrorTestMessage += $"[ERROR] Invalid move. Played column is out of bound. Please play within column 1 and {grid.Cols}!\n";
								// 		Column filled
								else if (grid.ColumnFull(int.Parse(testMove.Substring(1))))
									ErrorTestMessage += $"[ERROR] Invalid move. Column is full. Please play a different column within 1 and {grid.Cols}!\n";
							}
							if (ErrorTestMessage != "")
							{
								grid.DisplayGrid();
								continue;
							}
							winner = grid.TestUpdateGrid(sequence);
							break;
						}
						while (true);
						
					}
					if (testMode) break;

					// Get move, from player or computer
					string? move = "";
					int PlayerID = (grid.moveCount - 1) % 2;
					Util.LogString($"\nPlayerID: {PlayerID}");
					if (PlayerID == 1 & gameMode == 2)
					{
						// Computer move
						Console.WriteLine();
						Console.WriteLine("Computer is thinking...");
						Thread.Sleep(500);
						GamePlayers[1].setGridCopy(grid.Matrix);
						move = GamePlayers[1].PlayToWin();
						Console.WriteLine($"Computer played: {move}");
						Thread.Sleep(1000);
					}
					else
					{
						// Player move
						Dictionary<string, int> currentDiscs = GamePlayers[PlayerID].Discs;

						if (ErrorMessage != "")
						{
							Console.WriteLine();
							Console.WriteLine(ErrorMessage);
							ErrorMessage = "";
						}

						Console.WriteLine();
						Console.WriteLine($"Winning condition: {grid.WinCondition} continuous discs");
						Console.WriteLine();
						Console.WriteLine($"Player {PlayerID + 1} remaining discs:");
						Console.Write($"Ordinary (o): {currentDiscs["o"]}    ");
						Console.Write($"Boring (b): {currentDiscs["b"]}    ");
						Console.WriteLine($"Magnetic (o): {currentDiscs["m"]}");
						Console.WriteLine();
						Console.WriteLine($"Example move: o7 - \"o\" is the disc type, \"7\" is the column.");

						Console.WriteLine();
						Console.Write("Your move: ");
						move = Console.ReadLine();
						// Process weird input
						if (move == null | move == "")
						{
							ErrorMessage = "[ERROR] Empty input. Move cannot be empty. Please input a new move!\n";
							grid.DisplayGrid();
							continue;
						}
						move = move.ToLower();
						//		Player save
						if (move == "s")
						{
							GameMenu.SaveGame(grid.Matrix, GamePlayers[0], GamePlayers[1], gameMode, grid.moveCount);
							ErrorMessage = "[INFO] Game saved.";
						}
						//		Player back to main menu
						else if (move == "mm")
						{
							return true;
						}
						//		Player quit
						else if (move == "q")
						{
							Environment.Exit(0);
							return false;
						}
						//		Player save and quit
						else if (move == "sq")
						{
							GameMenu.SaveGame(grid.Matrix, GamePlayers[0], GamePlayers[1], gameMode, grid.moveCount);
							Environment.Exit(0);
							return false;
						}
						// 		Check 1st character is letter
						if (move.Length <= 1)
						{
							if (!"s".Contains(move.Substring(0, 1).ToLower()))
								ErrorMessage = $"[ERROR] Wrong input format. Missing column. Please input a new move!\n";
						}
						else if (!"sqobmm".Contains(move.Substring(0, 1).ToLower()))
							ErrorMessage = $"[ERROR] Wrong input format. 1st character need to be \"o\",\"b\", or \"m\". Please input a new move!\n";
						// 		Check remaining characters are numbers
						else if (!int.TryParse(move.Substring(1), out _))
							ErrorMessage = $"[ERROR] Wrong input format. From the 2nd character onwards, there can only be number. Please input a new move!\n";

						// Process invalid moves
						// 		Out of played type of disc
						else if (grid.OutOf1Disc(move.Substring(0, 1), PlayerID))
							ErrorMessage += $"[ERROR] Invalid move. Your disc {move[0]} is 0. Please play a different disc!\n";
						// 		Out of bound
						else if (int.Parse(move.Substring(1)) <= 0 | int.Parse(move.Substring(1)) > grid.Cols)
							ErrorMessage += $"[ERROR] Invalid move. Played column is out of bound. Please play within column 1 and {grid.Cols}!\n";
						// 		Column filled
						else if (grid.ColumnFull(int.Parse(move.Substring(1))))
							ErrorMessage += $"[ERROR] Invalid move. Column is full. Please play a different column within 1 and {grid.Cols}!\n";

						if (ErrorMessage != "")
						{
							grid.DisplayGrid();
							continue;
						}
					}

					// Update gameplay
					// 		Update grid
					Util.LogString($"move: {move}");
					grid.UpdateGrid(move);

					// 		Update discs
					Dictionary<string, int> newDiscs = new()
					{
						{ move.Substring(0,1), GamePlayers[PlayerID].Discs[move.Substring(0,1)]-1 }
					};
					GamePlayers[PlayerID].Discs = newDiscs;
					// Thread.Sleep(1000);

					winner = grid.CheckWin();
					Util.LogString($"winner: {winner}");
					// Thread.Sleep(1000);
				}
				while (!grid.OutOfDiscs(0) & !grid.OutOfDiscs(1) & winner == "");

				string winnerString;
				if (winner == "1") winnerString = "Player 1";
				else if (gameMode == 1 | gameMode == 3) winnerString = "Player 2";
				else winnerString = "Computer";
				Console.WriteLine();
				Console.WriteLine($"The winner is: {winnerString}");
				Console.WriteLine();

				Console.WriteLine("Do you want to play again?\n");
				Console.WriteLine("y: Yes \t mm: Back to main menu \t q: Quit game");
				Console.WriteLine("\nAny input not mentioned above will quit the game");

				Console.Write("\tYour decision: ");
				string? command = Console.ReadLine();
				if (command == null)
				{
					Environment.Exit(0);
					return false;
				}
				command = command.ToLower();
				if (command == "q")
				{
					Environment.Exit(0);
					return false;
				}
				else if (command == "y" | command == "yes")
				{
					playAgain = true;

					// Initiate players
					Dictionary<string, int> discs1 = new(){
						{"o", (int)(cols * rows * 0.5)},
						{"b", 2},
						{"m", 2}
					};
					
					Dictionary<string, int> discs2 = new(){
						{"o", (int)(cols * rows * 0.5)},
						{"b", 2},
						{"m", 2}
					};
					GamePlayers[0].Discs = discs1;
					GamePlayers[1].Discs = discs2;

					// Generate grid
					grid.GenerateGrid(cols, rows);
					grid.AddPlayers(GamePlayers[0], GamePlayers[1]);
				}
				else if (command == "mm")
				{
					return true;
				}
				else
				{
					Environment.Exit(0);
					return false;
				}

			}
			while (playAgain);
			return false;
		}
    }
}