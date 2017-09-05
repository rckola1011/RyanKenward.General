using System;
using NUnit.Framework;
using RyanKenward.General.Models;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Tests.Models
{
    [TestFixture]
    public class CardTest
    {
        public CardTest()
        {
        }

		[Test]
		public void Card_ShouldCreateCard()
		{
			CardName cardName = new CardName(ECardName.Ace);
			CardSuit cardSuit = new CardSuit(ECardSuit.Spades);
			Card sut = new Card(cardName, cardSuit);
			Assert.AreEqual(sut.GetName().GetValue(), "Ace");
            Assert.AreEqual(sut.GetRank().GetValue(), 1);
			Assert.AreEqual(sut.GetSuit().GetValue(), "Spades");
		}

		[Test]
		public void GetName_ShouldBeCardName()
		{
			CardName cardName = new CardName(ECardName.Jack);
            CardSuit cardSuit = new CardSuit(ECardSuit.Clubs);
			Card sut = new Card(cardName, cardSuit);
            Assert.IsInstanceOf<ICardName>(sut.GetName());
			Assert.AreEqual(sut.GetName(), cardName);
		}

		[Test]
		public void GetRank_ShouldBeCardRank()
		{
            CardName cardName = new CardName(ECardName.King);
            CardSuit cardSuit = new CardSuit(ECardSuit.Hearts);
			Card sut = new Card(cardName, cardSuit);
            Assert.IsInstanceOf<ICardRank>(sut.GetRank());
            Assert.AreEqual(sut.GetRank().GetValue(), 13);
		}

		[Test]
		public void GetSuit_ShouldBeCardSuit()
		{
			CardName cardName = new CardName(ECardName.Six);
			CardSuit cardSuit = new CardSuit(ECardSuit.Diamonds);
			Card sut = new Card(cardName, cardSuit);
			Assert.IsInstanceOf<ICardSuit>(sut.GetSuit());
			Assert.AreEqual(sut.GetSuit(), cardSuit);
		}

		[Test]
		public void ToString_ShouldBeCardString()
		{
			CardName cardName = new CardName(ECardName.Six);
			CardSuit cardSuit = new CardSuit(ECardSuit.Diamonds);
			Card sut = new Card(cardName, cardSuit);
            Assert.AreEqual(sut.ToString(), "Six♢");
		}
    }
}
