using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{

    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {
            1,
            new ContactModel()
            {
                Id = 1,
                FirstName = "Adamo",
                LastName = "Kus",
                Email = "adamkus@wsei.edu.pl",
                PhoneNumber = "333 134 003",
                BirthDate = new DateOnly(2003, 10, 10)
            }
        },
        {
            2,
            new ContactModel()
            {
                Id = 2,
                FirstName = "Michal",
                LastName = "Glus",
                Email = "michalglus@wsei.edu.pl",
                PhoneNumber = "885 267 388",
                BirthDate = new DateOnly(2005, 3, 22)
            }
        },
        {
            3,
            new ContactModel()
            {
                Id = 3,
                FirstName = "Kamil",
                LastName = "Zdun",
                Email = "kamilzdun@wsei.edu.pl",
                PhoneNumber = "578 399 100",
                BirthDate = new DateOnly(2000, 1, 5)
            }
        }
    };

    private static int currentId = 3;
    
    
    public IActionResult Index()
    {
        return View(_contacts);
    }
    
    public IActionResult Add()
    {
        
        return View();
    }
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Id = ++currentId;
        _contacts.Add(model.Id, model);
        return View("Index", _contacts);
    }
}