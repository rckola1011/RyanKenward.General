using System;
using System.Collections.Generic;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Models
{
    [Serializable]
    public class CardSuit : ICardSuit
    {
        public String Value { get; private set; }
        public String Symbol { get; private set; }

        public CardSuit(ECardSuit cardSuit)
        {
            this.Value = cardSuit.ToString();

			// New in C# 7: declare out variables in method call
            SetSymbol(cardSuit, out bool success);

			if (!success)
                Console.Write("Failed to find the symbol for suit '{0}'", cardSuit.ToString());
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        public String GetValue()
        {
            return this.Value;
        }

        /// <summary>
        /// Gets the symbol.
        /// </summary>
        /// <returns>The symbol.</returns>
        public String GetSymbol()
        {
            return this.Symbol;
        }

        /// <summary>
        /// Gets a list of all possible card suits.
        /// </summary>
        /// <returns>The card suits.</returns>
        public static List<String> GetCardSuits()
        {
            List<String> cardSuits = new List<String>(Enum.GetNames(typeof(ECardSuit)));
			return cardSuits;
        }

        /// <summary>
        /// Sets the symbol.
        /// </summary>
        /// <param name="cardSuit">Card suit.</param>
        private void SetSymbol(ECardSuit cardSuit, out bool success)
        {
            switch (cardSuit.ToString().ToLower())
            {
				case "clubs":
					this.Symbol = "♣";
					break;
				case "diamonds":
					this.Symbol = "♢";
					break;
				case "hearts":
					this.Symbol = "♡";
					break;
				case "spades":
					this.Symbol = "♠";
					break;
                default:
                    this.Symbol = "";
                    success = false;
                    break;
            }
            success = true;
        }
    }
}
