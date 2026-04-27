using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using App.Models;
using System.Security.Principal;

namespace App.Controllers;

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


    [HttpGet]
    public async Task<IActionResult> RoleManagement()
    {
        var users = _userManager.Users.ToList();
        var model = new List<RoleManagementViewModel>();

        foreach (var user in users)
        {
            model.Add(new RoleManagementViewModel
            {
                Id = user.Id,
                Name = user.Forename + " " + user.Surname,
                Email = user.Email ?? "",
                IsUser = await _userManager.IsInRoleAsync(user, "User"),
                IsApprover = await _userManager.IsInRoleAsync(user, "Approver"),
                IsAdmin = await _userManager.IsInRoleAsync(user, "Admin")
            });
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> RoleManagement(string id, string role)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
            return RedirectToAction("RoleManagement");

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        await _userManager.AddToRoleAsync(user, role);

        return RedirectToAction("RoleManagement");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}