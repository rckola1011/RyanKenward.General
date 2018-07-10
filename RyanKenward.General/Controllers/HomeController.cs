using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using RyanKenward.General.Models;
using RyanKenward.General.Models.Interfaces;

namespace RyanKenward.General.Controllers
{
    public class HomeController : Controller
    {
        private static readonly String SESSION_DECK = "deck";

        public ActionResult Index()
        {
            ViewBag.Title = "Ryan Kenward: C#";
            return View();
        }

        [HttpGet]
        public JsonResult HelloWorld()
        {
            return Json("Hello world!", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CardNames()
        {
            List<String> cardNames = CardName.GetCardNames();
            return Json(cardNames, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CardSuits()
        {
            List<String> cardSuits = CardSuit.GetCardSuits();
            return Json(cardSuits, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateDeckOfCards()
        {
            IDeckOfCards deck = new DeckOfCards();
            if (Session[SESSION_DECK] == null)
            {
            	Session.Add(SESSION_DECK, deck);
            }
            else
            {
            	Session[SESSION_DECK] = deck;
            }
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeckOfCards()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
                Session.Add(SESSION_DECK, deck);
            }
            else
            {
                deck = (DeckOfCards)Session[SESSION_DECK];
            }
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ShuffleCards()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
                deck = new DeckOfCards();
            }
            else
            {
                deck = (DeckOfCards)Session[SESSION_DECK];
            }

            deck.Shuffle();
            Session[SESSION_DECK] = deck; // Update deck in session
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RandomCard()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            ICard randomCard = deck.GetRandomCard();
            return Json(randomCard, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SortCardsAscending()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            deck.SortAscending();
            Session[SESSION_DECK] = deck; // Update deck in session
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SortCardsDescending()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            deck.SortDescending();
            Session[SESSION_DECK] = deck; // Update deck in session
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilterDeckByCardName(String cardName)
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            IList<Card> cardsByName;
            try 
            {
                cardsByName = deck.GetAllCardsByCardName(cardName);
            }
            catch (Exception e)
            {
                Console.Write(e);
                cardsByName = new List<Card>();
            }

            return Json(cardsByName, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult FilterDeckByCardSuit(String cardSuit)
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            IList<Card> cardsBySuit;
            try
            {
                cardsBySuit = deck.GetAllCardsByCardSuit(cardSuit);
            }
            catch (Exception e)
            {
                Console.Write(e);
                cardsBySuit = new List<Card>();
            }

            return Json(cardsBySuit, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddDeck()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            	IDeckOfCards deckToAdd = new DeckOfCards();
            	deck.AddCardsToDeck(deckToAdd.GetCards());
            }

            Session[SESSION_DECK] = deck; // Update deck in session
            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AceUpTheSleeve()
        {
            DeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            // Add card in parallel
            Task task = new Task(() => { 
                deck.AceUpTheSleeve();
                Session[SESSION_DECK] = deck; // Update deck in session
            });
            task.Start();

            return Json(deck, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveRandomCards(int numberOfCardsToRemove)
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            ValueTask<List<Card>> cardsRemovedTask = deck.RemoveRandomCardsAsync(numberOfCardsToRemove);

            return Json(cardsRemovedTask.Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LowestRankCard()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            ICard card = deck.GetLowestRankCard();
            return Json(card, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult HighestRankCard()
        {
            IDeckOfCards deck;
            if (Session[SESSION_DECK] == null)
            {
            	deck = new DeckOfCards();
            }
            else
            {
            	deck = (DeckOfCards)Session[SESSION_DECK];
            }

            ICard card = deck.GetHighestRankCard();
            return Json(card, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ClearSession()
        {
            Session.Clear();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
