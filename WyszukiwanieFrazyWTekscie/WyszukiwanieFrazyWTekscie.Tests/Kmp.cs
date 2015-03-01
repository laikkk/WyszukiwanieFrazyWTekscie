using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WyszukiwanieFrazyWTekscie.Tests
{
	[TestClass]
	public class Kmp
	{
		private TestContext m_testContext;
		public TestContext TestContext
		{
			get { return m_testContext; }
			set { m_testContext = value; }
		}

		private const string Foo = "Foo";
		private string _bar;

		[TestInitialize]
		public void SetUp()
		{
			//Tak aby pokazac, ze wiem i potrafie zrobic setup 
			_bar = "Bar";
		}

		[TestMethod]
		[DataSource("System.Data.OleDB",
					@"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=.\\data.xlsx; 
                    Extended Properties='Excel 12.0;HDR=yes';",
					"Sheet1$",
					DataAccessMethod.Sequential)]
		public void WhenGiveProperStringAndPattern_ShouldReturnListWithOccurences()
		{
			// Arrange
			var text = m_testContext.DataRow["text"].ToString();
			var pattern = m_testContext.DataRow["pattern"].ToString();
			var occurences = m_testContext.DataRow["occurences"].ToString();
			List<int> convertedItems = new List<int>();
			if (!string.IsNullOrEmpty(occurences))
			{
				string[] tokens = occurences.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
				convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse).ToList();
			}
			// Act
			var result = text.KMP(pattern);

			string resultOutput = string.Join(", ", result);
			string convertedItemstOutput = string.Join(", ", convertedItems);

			//Assert
			CollectionAssert.AreEqual(convertedItems, result,
				@"Invalid occurences of pattern: 
				text :'{0}'
				pattern :'{1}' 
				found occurences: {2}
				should be: {3}" + Environment.NewLine, new object[] { text, pattern, resultOutput, convertedItemstOutput });
		}

		[TestMethod]
		public void WhenMatcherIsOnBeginigOfText_ShouldBeFoundAndReturned()
		{
			var result = "aacbbabba".KMP("aa");
			var correctsOccurences = new List<int> { 0 };
			CollectionAssert.AreEqual(correctsOccurences, result);
		}

		[TestMethod]
		public void WhenMatcherIsAtTheEndOfText_ShouldBeFoundAndReturned()
		{
			var result = "aaba".KMP("ba");
			var correctsOccurences = new List<int> { 2 };
			CollectionAssert.AreEqual(correctsOccurences, result);
		}

		[TestMethod]
		public void WhenGiveTwoEmptyStrings_ShouldReturnEmptyList()
		{
			var result = String.Empty.KMP(String.Empty);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveFooAndEmptyString_ShouldReturnEmptyList()
		{
			var result = Foo.KMP(String.Empty);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveEmptyStringAndFoo_ShouldReturnEmptyList()
		{
			var result = String.Empty.KMP(Foo);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveBarAndFoo_ShouldReturnEmptyList()
		{
			var result = "Bar".KMP(Foo);
			Assert.AreEqual(result.Count, 0);
		}


		[TestMethod]
		public void WhenGiveTwoEmptyStrings_ShouldNotThrowExeptions()
		{
			try
			{
				String.Empty.KMP(String.Empty);
			}
			catch
			{
				Assert.Fail();
			}
			Assert.AreEqual(true, true);
		}

		[TestMethod]
		public void WhenGiveFooAndEmptyString_ShouldNotThrowExeptions()
		{
			try
			{
				Foo.KMP(String.Empty);
			}
			catch
			{
				Assert.Fail();
			}
			Assert.AreEqual(true, true);
		}

		[TestMethod]
		public void WhenGiveEmptyStringAndFoo_ShouldNotThrowExeptions()
		{
			try
			{
				String.Empty.KMP(Foo);
			}
			catch
			{
				Assert.Fail();
			}
			Assert.AreEqual(true, true);
		}

		[TestMethod]
		public void WhenGiveFooAndBar_ShouldNotThrowExeptions()
		{
			try
			{
				Foo.KMP(_bar);
			}
			catch
			{
				Assert.Fail();
			}
			Assert.AreEqual(true, true);
		}

	}
}
