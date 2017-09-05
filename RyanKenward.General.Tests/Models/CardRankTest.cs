using System;
using NUnit.Framework;
using RyanKenward.General.Models;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Tests.Models
{
    [TestFixture]
    public class CardRankTest
    {
        public CardRankTest()
        {
        }

		[Test]
		public void CardRank_ShouldCreateCardRank()
		{
			ICardName cardName = new CardName(ECardName.Queen);
			ICardRank sut = new CardRank(cardName);
			Assert.IsInstanceOf<ICardRank>(sut);
			Assert.AreEqual(sut.GetValue(), 12);
		}

		[Test]
		public void GetValue_ShouldBeNine()
		{
            ICardName cardName = new CardName(ECardName.Nine);
			ICardRank sut = new CardRank(cardName);
			Assert.AreEqual(sut.GetValue(), 9);
		}
    }
}
