using FoodRecipeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FoodRecipeWebApp.Controllers
{
    public class RateController : Controller
    {
        Uri baseAddress = new Uri("http://35.183.180.44/api/Rate"); // For Rate
        private readonly HttpClient _client;

        public RateController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RateViewModel> rateList = new List<RateViewModel>();
            // ################################################################################################### FIX PATH
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/rate/GetAllRate").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                rateList = JsonConvert.DeserializeObject<List<RateViewModel>>(data);
            }
            return View(rateList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RateViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/rate/CreateRate", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rating Created!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                RateViewModel rate = new RateViewModel();
                // ################################################################################################ FIX PATH
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/rate/GetRateById/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    rate = JsonConvert.DeserializeObject<RateViewModel>(data);
                }
                return View(rate);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(RateViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // ################################################################################################ FIX PATH
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/rate/UpdateRate", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rating Details Updated!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                RateViewModel rate = new RateViewModel();
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/rate/GetRateById/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    rate = JsonConvert.DeserializeObject<RateViewModel>(data);
                }

                return View(rate);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            try
            {
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/rate/DeleteRate/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Rating Deleted!";
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }
    }
}
