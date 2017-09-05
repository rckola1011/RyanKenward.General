using System;
using System.Collections.Generic;
using RyanKenward.General.Models.Enums;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Models
{
    [Serializable]
    public class CardName : ICardName
    {
        public String Value { get; private set; }

        public CardName(ECardName cardName)
        {
            this.Value = cardName.ToString();
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
        /// Gets a list of all possible card names.
        /// </summary>
        /// <returns>The card names.</returns>
        public static List<String> GetCardNames()
        {
            List<String> cardNames = new List<string>(Enum.GetNames(typeof(ECardName)));
            return cardNames;
        }
    }
}
