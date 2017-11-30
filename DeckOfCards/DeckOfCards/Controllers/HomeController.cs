using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DeckOfCards.Controllers
{
    public class HomeController : Controller
    {
        public string CardData { get; private set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Cards()
        {
            HttpWebRequest WR = WebRequest.CreateHttp("https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");
            WR.UserAgent = ".NET Framework Test Client";
            HttpWebResponse Response = (HttpWebResponse)WR.GetResponse();
           
           

           
           // JObject JsonData = JObject.Parse(CardData);

           // string Draw = JsonData["deck_id"].ToString();

           /* https://deckofcardsapi.com/api/deck/fa3qhvtaxf8p/draw/?count=5  */      



        HttpStatusCode status = Response.StatusCode;
            if (status == HttpStatusCode.OK)
            {

                StreamReader reader = new StreamReader(Response.GetResponseStream());

        string Deck = reader.ReadToEnd();

        JObject JsonDeck = JObject.Parse(Deck);

        string deckId = JsonDeck["deck_id"].ToString();

        WR = WebRequest.CreateHttp($"https://deckofcardsapi.com/api/deck/{deckId}/draw/?count=5");

                Response = (HttpWebResponse) WR.GetResponse();

        StreamReader reader2 = new StreamReader(Response.GetResponseStream());

        string DeckList = reader2.ReadToEnd();

        JsonDeck = JObject.Parse(DeckList);
              /*  ViewBag.Remaining = JsonDeck["remaining"];
                ViewBag.Shuffled = JsonDeck["shuffled"];
                ViewBag.Success = JsonDeck["success"];
                ViewBag.Deck_ID = JsonDeck["deck_id"];*/

                ViewBag.Cards = JsonDeck["cards"];

                return View();
            }
            else
            {
                ViewBag.Error = status;
                return View();
            }
        }


    }
}