using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WyszukiwanieFrazyWTekscie.Tests
{
	[TestClass]
	public class NaiwnySposob
	{
		private const string Foo = "Foo";
		private string _bar;

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
			var correctsOccurences = new List<int> {2, 5};
			CollectionAssert.AreEqual(correctsOccurences,result);
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

		[TestMethod]
		//Podpiac plik xml i baze
		public void WhenGiveStringsWithMachingValues_ShouldReturnNonEmptyList()
		{
			var result = "aaabbabba".NaiwnySposob("ab");
			var correctsOccurences = new List<int> { 2, 5 };
			CollectionAssert.AreEqual(correctsOccurences, result);
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
			Assert.AreEqual(true,true);
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
