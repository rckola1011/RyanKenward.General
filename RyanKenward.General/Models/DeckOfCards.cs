using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Models
{
    [Serializable]
    public class DeckOfCards : IDeckOfCards
	{
        public List<Card> Cards { get; private set; }

        /// <summary>
        /// Initializes a new instance of the DeckOfCards class with a standard deck of cards.
        /// </summary>
        public DeckOfCards()
        {
            this.Cards = DeckOfCards.CreateStandardDeck();
        }

        /// <summary>
        /// Initializes a new instance of the DeckOfCards class with a list of cards.
        /// </summary>
        /// <param name="cards">Cards.</param>
        public DeckOfCards(List<Card> cards)
        {
            if (cards == null)
            	throw new ArgumentException("Card list cannot be null");

            this.Cards = cards;
        }

        /// <summary>
        /// Gets the cards list.
        /// </summary>
        /// <returns>The cards.</returns>
        public List<Card> GetCards()
        {
	        return this.Cards;
        }

        /// <summary>
        /// Creates a standard deck of cards.
        /// </summary>
        public static List<Card> CreateStandardDeck()
        {
        	List<Card> cards = new List<Card>();
        	int numOfCardNames = Enum.GetValues(typeof(ECardName)).Length;
        	int numOfCardSuits = Enum.GetValues(typeof(ECardSuit)).Length;
        	// Loop names of cards
        	for (int iCard = 0; iCard < numOfCardNames; iCard++)
        	{
                CardName name = new CardName((ECardName)Enum.GetValues(typeof(ECardName)).GetValue(iCard));
        		// Loop card suits
        		for (int iSuit = 0; iSuit < numOfCardSuits; iSuit++)
        		{
                    CardSuit suit = new CardSuit((ECardSuit)Enum.GetValues(typeof(ECardSuit)).GetValue(iSuit));
        			cards.Add(new Card(name, suit));
        		}
        	}
            return cards;
        }

        /// <summary>
        /// Adds the list of cards to the deck.
        /// </summary>
        /// <param name="cards">List of cards to add.</param>
        public void AddCardsToDeck(List<Card> cards)
        {
            if (cards == null)
            	throw new ArgumentException("Card list cannot be null");

            ((List<Card>)this.Cards).AddRange(cards);
        }

        /// <summary>
        /// Adds the cards to deck without duplicates.
        /// </summary>
        /// <param name="cards">List of cards to add.</param>
        public void AddCardsToDeckWithoutDuplicates(List<Card> cards)
        {
            if (cards == null)
            	throw new ArgumentException("Card list cannot be null");

            AddCardsToDeck(cards);
            this.Cards = this.GetCards().GroupBy(card => new { 
                    CardName = card.GetName().GetValue(), 
                    CardSuit = card.GetSuit().GetValue() })
                .Select(dCard => dCard.First())
                .ToList<Card>();
        }

        /// <summary>
        /// Adds the card to the deck.
        /// </summary>
        /// <param name="card">Card.</param>
        public void AddCardToDeck(Card card)
        {
            if (card == null)
            	throw new ArgumentException("Card cannot be null");

            this.Cards.Add(card);
        }

        /// <summary>
        /// Adds an ace to your deck (shhhhh...)
        /// </summary>
        public void AceUpTheSleeve()
        {
            ICardName cardName = new CardName(ECardName.Ace);
            ICardSuit cardSuit = new CardSuit(ECardSuit.Spades);
            AddCardToDeck(new Card(cardName, cardSuit));
        }

        /// <summary>
        /// Gets a list of random cards.
        /// </summary>
        /// <returns>The random cards.</returns>
        /// <param name="numberOfCards">Number of random cards to get.</param>
        public List<Card> GetRandomCards(int numberOfCards)
        {
            if (numberOfCards < 0)
                throw new ArgumentException("The number of random cards must be greater than zero.");

            // If the number of random cards is more or equal to the cards in the deck, return the entire deck
            if (this.Cards.Count <= numberOfCards)
                return this.Cards;

            List<Card> randomCards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
                randomCards.Add(GetRandomCard());
            return randomCards;
        }

        /// <summary>
        /// Gets a random card from the deck.
        /// </summary>
        /// <returns>The random card.</returns>
        public Card GetRandomCard()
        {
            using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
            	byte[] byteArray = new byte[8];
            	rNGCryptoServiceProvider.GetBytes(byteArray);
            	int iRandomNumber = BitConverter.ToInt32(byteArray, 0);
            	int iRandomCard = System.Math.Abs((iRandomNumber % this.Cards.Count));
                return this.Cards[iRandomCard];
            }
        }

        /// <summary>
        /// Removes the specified number of random cards in async.
        /// </summary>
        /// <returns>The random cards removed.</returns>
        /// <param name="numberOfCardsToRemove">Number of cards to remove.</param>
        public async ValueTask<List<Card>> RemoveRandomCardsAsync(int numberOfCardsToRemove)
        {
            /*
             * The following method demonstrates asyncronous logic, LINQ joins, and new features in C# 7.
             */

            // Task to perform in background.  We don't need this value yet
            Task<List<Card>> randomCardTask = new Task<List<Card>>(() => GetRandomCards(numberOfCardsToRemove));
            randomCardTask.Start();

            // Logic to perform while task is running
            Shuffle();

            // Ok now we need the value of the task for sure.  Let's wait for it if it is not yet finished
            await randomCardTask;

            // Filter out the random cards from the deck
            this.Cards = this.Cards
                               .Where(ec => !randomCardTask.Result.Any(rc => rc.GetName().GetValue() == ec.GetName().GetValue() 
                                                                      && rc.GetSuit().GetValue() == ec.GetSuit().GetValue())).ToList();

            // Return random cards that were filtered out
            return randomCardTask.Result;   // ValueTask is new in C# 7 and has extra functionality over Task<>
        }

        /// <summary>
        /// Sorts the deck of cards in ascending order.
        /// </summary>
        public void SortAscending()
        {
            // Method syntax LINQ
            List<Card> cards = this.Cards.OrderBy(card => card.GetRank().GetValue()).ToList<Card>();
            this.Cards = cards;
        }

        /// <summary>
        /// Sorts the deck of cards in descending order.
        /// </summary>
        public void SortDescending()
        {
            // Query syntax LINQ
            IEnumerable<Card> cards =
                from card in this.Cards.AsEnumerable()
                orderby card.GetRank().GetValue() descending
                select card;
            this.Cards = cards.ToList<Card>();
        }

        /// <summary>
        /// Shuffles the deck of cards randomly.
        /// </summary>
        public void Shuffle()
        {
            List<Card> shuffledCards = new List<Card>();
            while (this.Cards.Count > 0)
            {
                Card randomCard = GetRandomCard();
            	shuffledCards.Add(randomCard);
            	this.Cards.Remove(randomCard);
            }
            this.Cards = shuffledCards;
        }

        /// <summary>
        /// Gets all cards in the deck by card name.
        /// </summary>
        /// <returns>All cards matching the card name.</returns>
        /// <param name="cardName">Card name.</param>
        public List<Card> GetAllCardsByCardName(String cardName)
        {
            if (cardName == null)
                throw new ArgumentException("Card name cannot be null");

            // Query syntax LINQ
            IEnumerable<Card> cards =
            	from card in this.Cards.AsEnumerable()
                where card.GetName().GetValue() == cardName.ToString()
            	select card;
            return cards.ToList<Card>();
        }

        /// <summary>
        /// Gets all cards in the deck by card suit.
        /// </summary>
        /// <returns>All cards matching the card suit.</returns>
        /// <param name="cardSuit">Card suit.</param>
        public List<Card> GetAllCardsByCardSuit(String cardSuit)
        {
            if (cardSuit == null)
            	throw new ArgumentException("Card suit cannot be null");

            // Method syntax LINQ
            List<Card> cards = this.Cards.Where(card => card.GetSuit().GetValue() == cardSuit.ToString()).ToList<Card>();
            return cards;
        }

        /// <summary>
        /// Gets the lowest rank card in the deck.
        /// </summary>
        /// <returns>The lowest rank card.</returns>
        public Card GetLowestRankCard()
        {
            return this.Cards.First(card => card.Rank.GetValue() == this.Cards.Min(minCard => minCard.GetRank().GetValue()));
        }

        /// <summary>
        /// Gets the highest rank card in the deck.
        /// </summary>
        /// <returns>The highest rank card.</returns>
        public Card GetHighestRankCard()
        {
            return this.Cards.First(card => card.Rank.GetValue() == this.Cards.Max(maxCard => maxCard.GetRank().GetValue()));
        }

        /// <summary>
        /// Prints the deck of cards to the console.
        /// </summary>
        public new String ToString()
        {
            String cardString = "";
            foreach (Card card in this.Cards)
                cardString += card.ToString() + " ";
            return cardString.TrimEnd();
        }
	}
}
