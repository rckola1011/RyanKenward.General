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
    public class CardSuitTest
    {
        public CardSuitTest()
        {
        }

		[Test]
		public void CardSuit_ShouldCreateCardSuit()
		{
			ICardSuit sut = new CardSuit(ECardSuit.Hearts);
			Assert.IsInstanceOf<ICardSuit>(sut);
			Assert.AreEqual(sut.GetValue(), "Hearts");
			Assert.AreEqual(sut.GetSymbol(), "♡");
		}

		[Test]
		public void GetValue_ShouldBeSpades()
		{
			ICardSuit sut = new CardSuit(ECardSuit.Spades);
			Assert.AreEqual(sut.GetValue(), "Spades");
		}

		[Test]
		public void GetSymbol_ShouldBeSpades()
		{
			ICardSuit sut = new CardSuit(ECardSuit.Spades);
			Assert.AreEqual(sut.GetSymbol(), "♠");
		}

		[Test]
		public void GetCardSuits_ShouldBeFour()
		{
            List<String> sut = CardSuit.GetCardSuits();
			Assert.AreEqual(sut.Count, 4);
            Assert.Contains("Clubs", new Collection<String>(sut));
            Assert.Contains("Diamonds", new Collection<String>(sut));
            Assert.Contains("Hearts", new Collection<String>(sut));
            Assert.Contains("Spades", new Collection<String>(sut));
		}
    }
}
