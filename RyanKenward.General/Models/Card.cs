using System;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Models
{
    [Serializable]
	public class Card : ICard
	{
		public ICardName Name { get; private set; }
		public ICardRank Rank { get; private set; }
		public ICardSuit Suit { get; private set; }

		public Card(ICardName name, ICardSuit suit)
		{
			if (name == null)
				throw new ArgumentException("Card name cannot be null");

			if (suit == null)
				throw new ArgumentException("Card suit cannot be null");
            
			this.Name = name;
			this.Rank = new CardRank(this.Name);
			this.Suit = suit;
		}

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <returns>The name.</returns>
        public ICardName GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// Gets the rank.
        /// </summary>
        /// <returns>The rank.</returns>
        public ICardRank GetRank()
        {
            return this.Rank;
        }

        /// <summary>
        /// Gets the suit.
        /// </summary>
        /// <returns>The suit.</returns>
        public ICardSuit GetSuit()
        {
            return this.Suit;
        }

        /// <summary>
        /// Print this card.
        /// </summary>
		public new String ToString()
		{
			return String.Format("{0}{1}", this.Name.GetValue(), this.Suit.GetSymbol());
		}
	}
}
