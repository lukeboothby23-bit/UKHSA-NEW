using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using UKHSA.Models;
using System.Security.Principal;

namespace UKHSA.Controllers;

//[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<User> _userManager;
    protected readonly UKHSA_DbContext _context;

    public AdminController(UserManager<User> userManager, UKHSA_DbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult AddDataset()
    {
        //var datasets = _context.Datasets.ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddDataset(AddDatasetViewModel Dataset)
    {

        Console.WriteLine(Dataset.AccessLevel);
        Dataset InputData = new Dataset
        {
            Title = Dataset.Title,
            Description = Dataset.Description,
            AccessLevel = Int32.Parse(Dataset.AccessLevel),
        };
        _context.Datasets.Add(InputData);
        await _context.SaveChangesAsync();
        return Redirect("/");
    }



    public IActionResult RoleManagement()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
