using SalonAPIClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SalonAPIClient.Helper;

namespace SalonAPIClient.Controllers
{
    public class SalonsController : Controller
    {

        ServerApi _api = new ServerApi();
        // GET: SalonController
        public async Task<ActionResult> Index()
        {
            List<Salon> salons = new List<Salon>();
            using (HttpClient client = _api.Initial())
            {
               // client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request to get all the salons
                HttpResponseMessage res = await client.GetAsync("salons");

                if (res.IsSuccessStatusCode)
                {
                    var salonResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    salons = JsonConvert.DeserializeObject<List<Salon>>(salonResponse);
                }
                return View(salons);
            }
        }

        public async Task<ActionResult> Details(int id)
        {
            Salon salon = new Salon();
            using (HttpClient client = _api.Initial())
            {
                //client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("salons/" + id);

                if (res.IsSuccessStatusCode)
                {
                    var salonResponse = res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api
                    salon = JsonConvert.DeserializeObject<Salon>(salonResponse);

                }
                return View(salon);
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: SalonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Salon salon)
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
                    HttpResponseMessage res = await client.PostAsJsonAsync("salons", salon);

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

        // GET: 
        public async Task<ActionResult> Edit(int id)
        {
            using (HttpClient client = _api.Initial())
            {
                //client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("apikey", "O0CYGHBtqhHRruBKAQa38ARUCQIcSsHM");

                //Sending Request tot get all the salons
                HttpResponseMessage res = await client.GetAsync("salons/" + id);
                Salon salon = res.Content.ReadAsAsync<Salon>().Result;

                if (res.IsSuccessStatusCode)
                {
                    return View(salon);
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }

            }

        }

        // POST: SalonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Salon salon)
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
                    HttpResponseMessage res = await client.PutAsJsonAsync("salons/" + salon.Id, salon);

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

        public async Task<ActionResult> Delete(int id)
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
                    HttpResponseMessage res = await client.DeleteAsync("salons/" + id);
                    Salon salon = res.Content.ReadAsAsync<Salon>().Result;

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
