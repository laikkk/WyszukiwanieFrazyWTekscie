using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WyszukiwanieFrazyWTekscie
{
	public static class StringExtensions
	{
		public static List<int> NaiwnySposob(this string text, string pattern)
		{
			if (text == string.Empty || pattern == string.Empty)
			{
				return new List<int>();
			}

			List<int> result = new List<int>();
			for (int s = 0; s < text.Length - pattern.Length + 1; s++)
			{
				string aktualnieWyciety = text.Substring(s, pattern.Length);
				if (aktualnieWyciety == pattern)
				{
					result.Add(s);
				}
			}

			return result;
		}
	}
}
