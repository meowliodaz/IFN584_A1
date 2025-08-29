using System;
using System.Runtime.InteropServices;


public class Program
{
	public static string updateGrid(string[] grid)
	{
		return "";
	}
	static void Main(string[] args)
	{
		string battleField = "\n";
		int columns = 7;
		int rows = 7;
		int winningCondition = (int)Math.Round(columns * rows * 0.1, 0, MidpointRounding.AwayFromZero);
		battleField += String.Concat(Enumerable.Repeat("|   ", columns)) + "|";
		battleField = String.Concat(Enumerable.Repeat(battleField, rows));
		battleField += "\n";
		for (int i = 1; i <= columns; i++)
		{
			battleField += $"  {i} ";
		}
		Console.WriteLine(battleField);
		
	}
}