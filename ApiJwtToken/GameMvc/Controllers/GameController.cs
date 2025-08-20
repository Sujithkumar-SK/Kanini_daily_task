using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
namespace GameMvc.Models;
[Route("[controller]/[action]")]
public class GameController : Controller
{
  private readonly HttpClient _httpclient;
  private readonly IConfiguration _config;
  public GameController(HttpClient httpClient, IConfiguration config)
  {
    _httpclient = httpClient;
    _config = config;
  }
  public async Task<IActionResult> Index()
  {
    string apiurl = _config["ApiBaseUrl"] + "/Game";
    var request = new HttpRequestMessage(HttpMethod.Get, apiurl);
    var token = HttpContext.Session.GetString("JWToken");
    if (string.IsNullOrEmpty(token))
    {
        return RedirectToAction("Login", "Auth");
    }
    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

    var response = await _httpclient.SendAsync(request);
    if (!response.IsSuccessStatusCode)
    {
        return Unauthorized("You need to login first");
    }

    var result = await response.Content.ReadAsStringAsync();
    var game = JsonSerializer.Deserialize<List<Game>>(result,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    return View(game);
  }
}