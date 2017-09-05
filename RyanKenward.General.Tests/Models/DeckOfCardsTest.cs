using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using RyanKenward.General.Models;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Tests.Models
{
    [TestFixture]
    public class DeckOfCardsTest
    {
        public DeckOfCardsTest()
        {
        }

        [Test]
        public void DeckOfCards_ShouldCreateDeck()
        {
            IDeckOfCards sut = new DeckOfCards();
            Assert.IsInstanceOf<IList<Card>>(sut.GetCards());
            Assert.AreEqual(sut.GetCards().Count, 52);
        }

        [Test]
        public void DeckOfCards_ShouldBeAceOfSpades()
        {
            List<Card> customDeck = new List<Card>();
            CardName cardName = new CardName(ECardName.Ace);
            CardSuit cardSuit = new CardSuit(ECardSuit.Spades);
            Card aceOfSpades = new Card(cardName, cardSuit);
            customDeck.Add(aceOfSpades);

            IDeckOfCards sut = new DeckOfCards(customDeck);
            Assert.IsInstanceOf<IList<Card>>(sut.GetCards());
            Assert.AreEqual(sut.GetCards().Count, 1);
            Assert.AreEqual(sut.GetCards()[0], aceOfSpades);
        }

		[Test]
		public void GetCards_ShouldBeListOfCards()
		{
			IDeckOfCards sut = new DeckOfCards();
			Assert.IsInstanceOf<IList<Card>>(sut.GetCards());
		}

		[Test]
		public void GetRandomCards_ShouldBeTenDifferentCards()
		{
			IDeckOfCards sut = new DeckOfCards();
			List<Card> randomCards1 = sut.GetRandomCards(10);
			List<Card> randomCards2 = sut.GetRandomCards(10);
			Assert.AreEqual(randomCards1.Count, 10);
			Assert.AreEqual(randomCards2.Count, 10);
			IDeckOfCards deck1 = new DeckOfCards(randomCards1);
			IDeckOfCards deck2 = new DeckOfCards(randomCards2);
			Assert.AreNotEqual(deck1.ToString(), deck2.ToString());
		}

		[Test]
		public void GetRandomCards_ShouldBeEntireDeck()
		{
			List<Card> customDeck = new List<Card>();
			CardName cardName = new CardName(ECardName.Ace);
			CardSuit cardSuit = new CardSuit(ECardSuit.Spades);
			Card card = new Card(cardName, cardSuit);
			customDeck.Add(card);

			IDeckOfCards sut = new DeckOfCards(customDeck);
			List<Card> randomCards = sut.GetRandomCards(2);
            Assert.AreEqual(randomCards.Count, 1);
            Assert.AreEqual(randomCards[0].GetName().GetValue(), "Ace");
            Assert.AreEqual(randomCards[0].GetSuit().GetValue(), "Spades");
		}

		[Test]
		public void GetRandomCard_ShouldBeCard()
		{
			IDeckOfCards deck = new DeckOfCards();
			ICard sut = deck.GetRandomCard();
			Assert.NotNull(sut);
			Assert.IsInstanceOf<ICard>(sut);
		}

		[Test]
        public void RemoveRandomCardsAsync_ShouldBeCardsRemovedFromDeck()
		{
			IDeckOfCards sut = new DeckOfCards();
            ValueTask<List<Card>> randomCardsTask = sut.RemoveRandomCardsAsync(5);
            List<Card> randomCards = randomCardsTask.Result;
            Assert.IsFalse(sut.GetCards().Contains(randomCards[0]));
            Assert.IsFalse(sut.GetCards().Contains(randomCards[1]));
            Assert.IsFalse(sut.GetCards().Contains(randomCards[2]));
            Assert.IsFalse(sut.GetCards().Contains(randomCards[3]));
            Assert.IsFalse(sut.GetCards().Contains(randomCards[4]));
		}

		[Test]
		public void SortAscending_ShouldBeListAscending()
		{
			IDeckOfCards sut = new DeckOfCards();
			sut.SortAscending();
			Assert.AreEqual(sut.GetCards()[0].GetRank().GetValue(), 1);
			Assert.AreEqual(sut.GetCards()[sut.GetCards().Count - 1].GetRank().GetValue(), 13);
		}

		[Test]
		public void SortDescending_ShouldBeListDescending()
		{
			IDeckOfCards sut = new DeckOfCards();
			sut.SortDescending();
			Assert.AreEqual(sut.GetCards()[0].GetRank().GetValue(), 13);
			Assert.AreEqual(sut.GetCards()[sut.GetCards().Count - 1].GetRank().GetValue(), 1);
		}

		[Test]
		public void Shuffle_ShouldBeDeckOf52()
		{
			IDeckOfCards sut = new DeckOfCards();
			sut.Shuffle();
			Assert.NotNull(sut.GetCards());
			Assert.IsInstanceOf<List<Card>>(sut.GetCards());
			Assert.AreEqual(sut.GetCards().Count, 52);
		}

		[Test]
		public void GetAllCardsByCardName_ShouldBeFourTwos()
		{
			IDeckOfCards deck = new DeckOfCards();
            List<Card> sut = deck.GetAllCardsByCardName(ECardName.Two.ToString());
			Assert.AreEqual(sut.Count, 4);
			Assert.AreEqual(sut[0].GetName().GetValue(), "Two");
		}

		[Test]
		public void GetAllCardsByCardSuit_ShouldBeThirteenClubs()
		{
			IDeckOfCards deck = new DeckOfCards();
            List<Card> sut = deck.GetAllCardsByCardSuit(ECardSuit.Clubs.ToString());
			Assert.AreEqual(sut.Count, 13);
            Assert.AreEqual(sut[0].GetSuit().GetValue(), "Clubs");
		}

		[Test]
		public void ToString_ShouldBeTwoCardsPrinted()
		{
            List<Card> customDeck = new List<Card>();
			CardName cardName = new CardName(ECardName.Ace);
			CardSuit cardSuit = new CardSuit(ECardSuit.Spades);
			Card card = new Card(cardName, cardSuit);
            customDeck.Add(card);
            cardName = new CardName(ECardName.King);
            cardSuit = new CardSuit(ECardSuit.Hearts);
			card = new Card(cardName, cardSuit);
			customDeck.Add(card);
			
            IDeckOfCards sut = new DeckOfCards(customDeck);
            Assert.AreEqual(sut.ToString(), "Ace♠ King♡");
		}

		[Test]
		public void AddCardsToDeck_ShouldBe104Cards()
		{
			IDeckOfCards sut = new DeckOfCards();
			IDeckOfCards deck2 = new DeckOfCards();
			sut.AddCardsToDeck(deck2.GetCards());
			Assert.AreEqual(sut.GetCards().Count, 104);
		}

		[Test]
		public void AddCardsToDeckWithoutDuplicates_ShouldBe52Cards()
		{
			IDeckOfCards sut = new DeckOfCards();
			IDeckOfCards deck2 = new DeckOfCards();
            sut.AddCardsToDeckWithoutDuplicates(deck2.GetCards());
			Assert.AreEqual(sut.GetCards().Count, 52);
		}

		[Test]
		public void AddCardToDeck_ShouldBe53Cards()
		{
			CardName cardName = new CardName(ECardName.Jack);
			CardSuit cardSuit = new CardSuit(ECardSuit.Diamonds);
			Card card = new Card(cardName, cardSuit);

			IDeckOfCards sut = new DeckOfCards();
			sut.AddCardToDeck(card);
			Assert.AreEqual(sut.GetCards().Count, 53);
		}

		[Test]
		public void AceUpTheSleeve_ShouldBeFiveAces()
		{
			IDeckOfCards sut = new DeckOfCards();
			sut.AceUpTheSleeve();
			Assert.AreEqual(sut.GetCards().Count, 53);
			Assert.AreEqual(sut.GetAllCardsByCardName("Ace").Count, 5);
		}

		[Test]
		public void GetLowestRankCard_ShouldBeAce()
		{
			IDeckOfCards sut = new DeckOfCards();
			ICard card = sut.GetLowestRankCard();
			Assert.AreEqual(card.GetName().GetValue(), "Ace");
		}

		[Test]
		public void GetHighestRankCard_ShouldBeKing()
		{
			IDeckOfCards sut = new DeckOfCards();
			ICard card = sut.GetHighestRankCard();
			Assert.AreEqual(card.GetName().GetValue(), "King");
		}
    }
}
