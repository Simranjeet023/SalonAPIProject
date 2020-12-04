using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalonAPIClient.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalonAPIClient.Helper;

namespace SalonAPIClient.Controllers
{
    public class ReviewsController : Controller
    {

        ServerApi _api = new ServerApi();
        public async Task<ActionResult> Index()
        {
            //QueryResult
            Review reviews = new Review();
            List<Review> reviewsList = new List<Review>();
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("reviews");

                if (res.IsSuccessStatusCode)
                {
                    var reviewResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    var pair = JsonConvert.DeserializeObject<Pair[]>(reviewResponse);

                    for(int i = 0; i < pair.Length; i++)
                    {
                        reviews = new Review { Id=pair[i].Id, Rating=pair[i].Rating, Description=pair[i].Description, SalonId=pair[i].Salon.Id };
                        reviewsList.Add(reviews);
                    }
                }
                return View(reviewsList);
            }
        }

       // GET: ReviewController/Details/5
       public async Task<ActionResult> Details(int id)
        {
            ReviewDTO reviews = new ReviewDTO();
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("reviews/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var reviewResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    var pair = JsonConvert.DeserializeObject<ReviewDetials>(reviewResponse);

                    reviews = new ReviewDTO { Id = pair.Id, Rating = pair.Rating, Description = pair.Description };
                }
                return View(reviews);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Review review)
        {
            try
            {
                using (HttpClient client = _api.Initial())
                {
                    //client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                    //Sending Request tot get all the salons
                    HttpResponseMessage res = await client.PostAsJsonAsync("reviews", review);

                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        //GET
        public async Task<ActionResult> Edit(int id)
        {
            using (HttpClient client = _api.Initial())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                Review review = new Review();
                //Sending Request tot get all the salons
                 HttpResponseMessage res = await client.GetAsync("reviews/" + id);
                //Review review = res.Content.ReadAsAsync<Review>().Result;

                if (res.IsSuccessStatusCode)
                {
                    var reviewResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    var pair = JsonConvert.DeserializeObject<ReviewDetials>(reviewResponse);

                    review = new Review { Id = pair.Id, Rating = pair.Rating, Description = pair.Description, SalonId=pair.Salon.Id };
                    return View(review);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Review review)
        {
            try
            {
                using (HttpClient client = _api.Initial())
                {
                    //client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                    //Sending Request tot get all the salons
                    HttpResponseMessage res = await client.PutAsJsonAsync("reviews/" + review.Id, review);

                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ReviewController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                using (HttpClient client = _api.Initial())
                {
                   // client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                    //Sending Request tot get all the salons
                    HttpResponseMessage res = await client.DeleteAsync("reviews/" + id);             

                    if (res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch
            {
                return View("Error");
            }

        }
    }
}
