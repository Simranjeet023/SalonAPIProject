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

namespace SalonAPIClient.Controllers
{
    public class ReviewsController : Controller
    {
        public string BaseUrl = "http://tchaw-eval-prod.apigee.net/salonapiproxy/api/";
        // GET: ReviewController
        //public async Task<ActionResult> Index()
        //{
        //    QueryResult<ReviewResource> reviews = new QueryResult<ReviewResource>();
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(BaseUrl);
        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        //Sending Request tot get all the categories
        //        HttpResponseMessage res = await client.GetAsync("reviews");

        //        if (res.IsSuccessStatusCode)
        //        {
        //            var productResponse = res.Content.ReadAsStringAsync().Result;
        //            //Deserializing the response recieved from web api
        //            reviews = JsonConvert.DeserializeObject<QueryResult<ReviewResource>>(productResponse);
        //        }
        //        return View(reviews);
        //    }
        //}
        public async Task<ActionResult> Index()
        {
            //QueryResult
            ReviewDTO reviews = new ReviewDTO();
            List<ReviewDTO> reviewsList = new List<ReviewDTO>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("reviews");

                if (res.IsSuccessStatusCode)
                {
                    var reviewResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    // reviews = 
                    var pair = JsonConvert.DeserializeObject<Pair[]>(reviewResponse);

                    for(int i = 0; i < pair.Length; i++)
                    {
                        reviews = new ReviewDTO { Id=pair[i].Id, Rating=pair[i].Rating, Description=pair[i].Description };
                        reviewsList.Add(reviews);
                    }
                    //reviews = pair[0];

                }
                return View(reviewsList);
            }
        }

            // GET: ReviewController/Details/5
            public async Task<ActionResult> Details(int id)
        {
            Review product = new Review();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("reviews/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var productResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    product = JsonConvert.DeserializeObject<Review>(productResponse);

                }
                return View(product);
            }
        }

        // POST: ReviewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Review product)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending Request tot get all the salons
                    HttpResponseMessage res = await client.PostAsJsonAsync("reviews", product);

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

        public async Task<IEnumerable<Salon>> GetSalons() 
        {
            List<Salon> salons = new List<Salon>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("salons");

                if (res.IsSuccessStatusCode)
                {
                    var categoryResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    salons = JsonConvert.DeserializeObject<List<Salon>>(categoryResponse);

                }
                return salons.ToList();
            }
        }


        // POST: ReviewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Review review)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending Request tot get all the salons
                    HttpResponseMessage res = await client.DeleteAsync("reviews/" + id);
                    Salon category = res.Content.ReadAsAsync<Salon>().Result;

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
