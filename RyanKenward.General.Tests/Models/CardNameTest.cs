using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using RyanKenward.General.Models;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Tests.Models
{
    [TestFixture]
    public class CardNameTest
    {
        public CardNameTest()
        {
        }

		[Test]
		public void CardName_ShouldCreateCardName()
		{
			ICardName sut = new CardName(ECardName.Ace);
			Assert.AreEqual(sut.GetValue(), "Ace");
		}

		[Test]
		public void GetValue_ShouldBeSeven()
		{
			ICardName sut = new CardName(ECardName.Seven);
			Assert.AreEqual(sut.GetValue(), "Seven");
		}

		[Test]
		public void GetCardNames_ShouldBeThirteen()
		{
            List<String> sut = CardName.GetCardNames();
            Assert.AreEqual(sut.Count, 13);
            Assert.Contains("Ace", new Collection<String>(sut));
            Assert.Contains("Two", new Collection<String>(sut));
            Assert.Contains("Three", new Collection<String>(sut));
            Assert.Contains("Four", new Collection<String>(sut));
            Assert.Contains("Five", new Collection<String>(sut));
            Assert.Contains("Six", new Collection<String>(sut));
            Assert.Contains("Seven", new Collection<String>(sut));
            Assert.Contains("Eight", new Collection<String>(sut));
            Assert.Contains("Nine", new Collection<String>(sut));
            Assert.Contains("Ten", new Collection<String>(sut));
            Assert.Contains("Jack", new Collection<String>(sut));
            Assert.Contains("Queen", new Collection<String>(sut));
            Assert.Contains("King", new Collection<String>(sut));
		}
    }
}
