using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RyanKenward.General.Models.Enums;

namespace RyanKenward.General.Models.Interfaces
{
	public interface IDeckOfCards
	{
        List<Card> GetCards();
        //void CreateStandardDeck();
        void AddCardsToDeck(List<Card> cards);
        void AddCardsToDeckWithoutDuplicates(List<Card> cards);
        void AddCardToDeck(Card card);
        //void AceUpTheSleeve();
        List<Card> GetRandomCards(int numberOfCards);
        Card GetRandomCard();
        ValueTask<List<Card>> RemoveRandomCardsAsync(int numberOfCardsToRemove);
        void SortAscending();
        void SortDescending();
        void Shuffle();
        List<Card> GetAllCardsByCardName(String cardName);
        List<Card> GetAllCardsByCardSuit(String cardSuit);
        Card GetLowestRankCard();
        Card GetHighestRankCard();
        String ToString();
	}
}
