using ClientSide.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientSide.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeString(string input)
        {
            //if (string.IsNullOrEmpty(input))
            //{
            //    ViewBag.Result = "Введіть рядок!";
            //    return View("Index");
            //}
            var http = "http://localhost:5195";
            var https = "https://localhost:7252";

            string apiUrl = $"{http}/String?value={System.Net.WebUtility.UrlEncode(input)}";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();

                ViewBag.Result = result;
            }
            catch
            {
                ViewBag.Result = "Помилка підключення до сервера.";
            }

            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
