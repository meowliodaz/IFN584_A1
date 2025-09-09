
namespace Connect4
{
	public class Util
	{
		internal static void LogMatrix(List<List<string>> m_)
		{
			string s_ = "";
			for (int i = 0; i < m_.Count; i++)
			{
				s_ += string.Join(",", m_[i].ToArray()) + "\n";
			}
			s_ += "\n";
			File.AppendAllText("matrix.log", s_);
		}
		
		internal static void LogString(string s_)
		{
			File.AppendAllText("string.log", s_+"\n");
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