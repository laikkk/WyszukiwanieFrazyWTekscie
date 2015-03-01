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


		public static List<int> KMP(this string text, string pattern)
		{
			if (text == string.Empty || pattern == string.Empty)
			{
				return new List<int>();
			}

			int n = text.Length;
			int m = pattern.Length;
			int[] pi = computePrefixFunction(pattern);
			int q = 0;
			List<int> result  = new List<int>();
			for (int i = 1; i <= n; i++)
			{
				while (q > 0 && pattern[q] != text[i - 1])
				{
					q = pi[q - 1];
				}
				if (pattern[q] == text[i - 1]) { q++; }
				if (q == m)
				{
					//Record a match was found here  
					result.Add(i-m);
					q = pi[q - 1];
				}
			}

			return result;
		}

		static int[] computePrefixFunction(string pattern)
		{
			int m = pattern.Length;
			int[] pi = new int[m];
			int k = 0;


			for (int q = 1; q < m; q++)
			{
				while (k > 0 && pattern[k] != pattern[q]) { k = pi[k]; }

				if (pattern[k] == pattern[q]) { k++; }
				pi[q] = k;
			}
			return pi;
		}  

	}
}
