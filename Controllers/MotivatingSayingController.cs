using Microsoft.AspNetCore.Mvc;
using MotivatingSaying.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MotivatingSaying.Controllers
{
    public class MotivatingSayingController : Controller
    {
        public IActionResult MotivationQuote()
        {
            HttpClient client = new HttpClient();
            var responseTask = client.GetAsync("https://zenquotes.io/api/random");
            responseTask.Wait();
            if(responseTask.IsCompleted)
            {
                var result = responseTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    var MessageTask = result.Content.ReadAsStringAsync();

                    List<MyArray> quote = JsonConvert.DeserializeObject<List<MyArray>>(MessageTask.Result);

                    ViewBag.Quote = quote[0].q;
                    ViewBag.Author = quote[0].a;

                }

            }
            return View();
        }

        public IActionResult Joke()
        {
            HttpClient client = new HttpClient();
            var responseTask = client.GetAsync("https://v2.jokeapi.dev/joke/Programming");
            responseTask.Wait();
            if (responseTask.IsCompleted)
            {
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var MessageTask = result.Content.ReadAsStringAsync();

                    var joke = JsonConvert.DeserializeObject<RootJoke>(MessageTask.Result);

                    ViewBag.joke = joke.setup;

                }

            }
            return View("MotivationQuote");
        }
    }
}
