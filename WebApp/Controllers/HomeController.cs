using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About() => View();

    public IActionResult Calculator(Operator? op, double? x, double? y = null)
    {
        // var x = double.Parse(Request.Query["x"]);
        // var y = double.Parse(Request.Query["y"]);
        // var op = Request.Query["op"];

        if (op == null)
        {
            ViewBag.ErrorMessage("Niepoprawny operator");
            return View("CalculatorError");
        }

        if (x == null || (y == null && op != Operator.SIN))
        {
            ViewBag.ErrorMessage("Nieprawidłowy parametr liczby x lub y");
            return View("CalculatorError");
        }

        switch (op)
        {
            case Operator.ADD:
            {
                ViewBag.Result = x + y;
                break;
            }
            case Operator.SUB:
            {
                ViewBag.Result = x - y;
                break;
            }
            case Operator.MUL:
            {
                ViewBag.Result = x * y;
                break;
            }
            case Operator.DIV:
            {
                if (y == 0)
                {
                    ViewBag.ErrorMessage("Nie mozna dzielic przez 0");
                    return View("CalculatorError");
                }
                ViewBag.Result = x / y;
                break;
            }
            case Operator.POW:
            {
                ViewBag.Result = Math.Pow((double)x, (double)y!);
                break;
            }
            case Operator.SIN:
            {
                ViewBag.Result = Math.Sin((double)x);
                break;
            }
        }
        
        return View();
    }

    public IActionResult CalculatorError()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}