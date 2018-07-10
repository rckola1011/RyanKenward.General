using System;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Models
{
    [Serializable]
    public class CardRank : ICardRank
	{
        public int Value { get; private set; }

        public CardRank(ICardName name)
        {
            if (name == null)
            	throw new ArgumentException("Card name cannot be null");

            // New in C# 7: declare out variables in method call
            SetCardRank(name, out bool success);

            if (!success)
                Console.Write("Failed to find the rank for card '{0}'", name.GetValue());
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        public int GetValue()
        {
            return this.Value;
        }

        /// <summary>
        /// Sets the card rank by card name
        /// </summary>
        /// <param name="name">Name.</param>
        private void SetCardRank(ICardName name, out bool success)
        {
            switch (name.GetValue().ToLower())
            {
            	case "ace":
            		this.Value = 1;
            		break;
            	case "two":
            		this.Value = 2;
            		break;
            	case "three":
            		this.Value = 3;
            		break;
            	case "four":
            		this.Value = 4;
            		break;
            	case "five":
            		this.Value = 5;
            		break;
            	case "six":
            		this.Value = 6;
            		break;
            	case "seven":
            		this.Value = 7;
            		break;
            	case "eight":
            		this.Value = 8;
            		break;
            	case "nine":
            		this.Value = 9;
            		break;
            	case "ten":
            		this.Value = 10;
            		break;
            	case "jack":
            		this.Value = 11;
            		break;
            	case "queen":
            		this.Value = 12;
            		break;
            	case "king":
            		this.Value = 13;
            		break;
            	default:
            		this.Value = 0;
                    success = false;
            		break;
            }
            success = true;
        }
	}
}
