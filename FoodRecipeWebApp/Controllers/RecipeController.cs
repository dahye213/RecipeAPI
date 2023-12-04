using FoodRecipeWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FoodRecipeWebApp.Controllers
{
    public class RecipeController : Controller
    {
        Uri baseAddress = new Uri("http://35.183.180.44/api/Recipe"); // For recipes
        private readonly HttpClient _client;

        public RecipeController() 
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RecipeViewModel> recipeList = new List<RecipeViewModel>();
            // ################################################################################################### FIX PATH
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;

            if(response.IsSuccessStatusCode) {
                string data = response.Content.ReadAsStringAsync().Result;
                recipeList = JsonConvert.DeserializeObject<List<RecipeViewModel>>(data);
            }
            return View(recipeList);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RecipeViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/CreateRecipe", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Recipe Created!";
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
        public ActionResult Edit(int id) 
        {
            try
            {
                RecipeViewModel recipe = new RecipeViewModel();
                // ################################################################################################ FIX PATH
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/GetRecipeById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    recipe = JsonConvert.DeserializeObject<RecipeViewModel>(data);
                }
                return View(recipe);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View(); 
            }
        }

        [HttpPost]
        public IActionResult Edit(RecipeViewModel model) 
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                // ################################################################################################ FIX PATH
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/UpdateRecipe", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Recipe Details Updated!";
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
        public IActionResult Delete(int id)
        {
            try
            {
                RecipeViewModel recipe = new RecipeViewModel();
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/GetRecipeById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    recipe = JsonConvert.DeserializeObject<RecipeViewModel>(data);
                }

                return View(recipe);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                // ############################################################################################### FIX PATH
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Recipe Deleted!";
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
