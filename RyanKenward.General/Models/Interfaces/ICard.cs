using System;

namespace RyanKenward.General.Models.Interfaces
{
	public interface ICard
	{
        ICardName GetName();
        ICardRank GetRank();
        ICardSuit GetSuit();
		String ToString();
	}
}
