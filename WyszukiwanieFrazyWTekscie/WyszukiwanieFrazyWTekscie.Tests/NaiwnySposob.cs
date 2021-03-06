﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WyszukiwanieFrazyWTekscie.Tests
{
	[TestClass]
	public class NaiwnySposob
	{
		private const string Foo = "Foo";
		private string _bar;

		private TestContext m_testContext;
		public TestContext TestContext
		{
			get { return m_testContext; }
			set { m_testContext = value; }
		}

		[TestInitialize]
		public void SetUp()
		{
			//Tak aby pokazac, ze wiem i potrafie zrobic setup 
			_bar = "Bar";
		}

		[TestMethod]
		public void WhenGiveProperStringAndPattern_ShouldReturnListWithOccurences()
		{
			var result = "aaabbabba".NaiwnySposob("ab");
			var correctsOccurences = new List<int> { 2, 5 };
			CollectionAssert.AreEqual(correctsOccurences, result);
		}

		[TestMethod]
		public void WhenMatcherIsOnBeginigOfText_ShouldBeFoundAndReturned()
		{
			var result = "aacbbabba".NaiwnySposob("aa");
			var correctsOccurences = new List<int> { 0 };
			CollectionAssert.AreEqual(correctsOccurences, result);
		}

		[TestMethod]
		public void WhenMatcherIsAtTheEndOfText_ShouldBeFoundAndReturned()
		{
			var result = "aaba".NaiwnySposob("ba");
			var correctsOccurences = new List<int> { 2 };
			CollectionAssert.AreEqual(correctsOccurences, result);
		}

		[TestMethod]
		public void WhenGiveTwoEmptyStrings_ShouldReturnEmptyList()
		{
			var result = String.Empty.NaiwnySposob(String.Empty);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveFooAndEmptyString_ShouldReturnEmptyList()
		{
			var result = Foo.NaiwnySposob(String.Empty);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveEmptyStringAndFoo_ShouldReturnEmptyList()
		{
			var result = String.Empty.NaiwnySposob(Foo);
			Assert.AreEqual(result.Count, 0);
		}

		[TestMethod]
		public void WhenGiveBarAndFoo_ShouldReturnEmptyList()
		{
			var result = "Bar".NaiwnySposob(Foo);
			Assert.AreEqual(result.Count, 0);
		}

		// How to lounch localdb
		http://stackoverflow.com/questions/21563940/how-to-connect-to-localdb-in-visual-studio-server-explorer
		[TestMethod]
		[DataSource("System.Data.SqlClient",
		@"Data Source=np:\\.\pipe\LOCALDB#4B1463FB\tsql\query;Initial Catalog=master;Integrated Security=True",
		"dbo.TestDataSource",
		DataAccessMethod.Sequential)]
		public void WhenGiveStringsWithMachingValues_ShouldReturnNonEmptyList()
		{
			// Arrange
			var text = m_testContext.DataRow["text"].ToString();
			var pattern = m_testContext.DataRow["pattern"].ToString();
			var occurences = m_testContext.DataRow["occurences"].ToString();
			List<int> convertedItems = new List<int>();
			if (!string.IsNullOrEmpty(occurences))
			{
				string[] tokens = occurences.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse).ToList();
			}
			// Act
			var result = text.NaiwnySposob(pattern);

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
		public void WhenGiveTwoEmptyStrings_ShouldNotThrowExeptions()
		{
			try
			{
				String.Empty.NaiwnySposob(String.Empty);
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
				Foo.NaiwnySposob(String.Empty);
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
				String.Empty.NaiwnySposob(Foo);
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
				Foo.NaiwnySposob(_bar);
			}
			catch
			{
				Assert.Fail();
			}
			Assert.AreEqual(true, true);
		}


	}
}
