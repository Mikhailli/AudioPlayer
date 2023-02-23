using AudioPlayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AudioPlayer.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationContext _db = new ();
    
    public IActionResult Index()
    {
        var context = new ApplicationContext();
        var t = context.Database;
        return View();
    }
}