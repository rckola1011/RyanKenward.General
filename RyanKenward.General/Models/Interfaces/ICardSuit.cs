using System;
using RyanKenward.General.Models.Enums;

namespace RyanKenward.General.Models.Interfaces
{
    public interface ICardSuit
    {
        String GetValue();
        String GetSymbol();
    }
}
