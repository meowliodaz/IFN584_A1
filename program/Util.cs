
namespace Connect4
{
	public class Util
	{
		internal static void LogMatrix(List<List<string>> m_)
		{
			string s_ = "";
			for (int r = m_[0].Count - 1; r >= 0; r--)
			// Printing top-down
			{
				s_ += string.Format("{0,-3}", r + 1);
				for (int c = 0; c < m_.Count; c++)
				{
					s_ += $"| {m_[c][r]} ";
				}
				s_ += "|\n";
			}
			s_ += string.Format("{0,-3}", " "); ;
			for (int c = 0; c < m_.Count; c++)
			{
				if (c < 10) s_ += $"  {c + 1} ";
				else s_ += $" {c + 1} ";
			}
			s_ += "\n\n";
			File.AppendAllText("matrix.log", s_);
		}
		
		internal static void LogString(string s_)
		{
			File.AppendAllText("string.log", s_+"\n");
		}
		internal static void LogFile(string file_, string s_)
		{
			File.WriteAllText(file_, s_+"\n");
		}
		internal static void DeleteLog(string filename_)
		{
			if(File.Exists(filename_))
			{
				File.Delete(filename_);
			}	
		}
	}
}