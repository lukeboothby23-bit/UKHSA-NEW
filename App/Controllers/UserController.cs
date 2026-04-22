using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Shared;
using App.Controllers;

namespace App.Controllers;

//[Authorize(Roles = "User")]
public class UserController : Controller
{
    protected readonly UKHSA_DbContext _context;
    private readonly UserManager<User> _userManager;

    public UserController(UKHSA_DbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Home()
    {
        return View("Home");
    }

    public IActionResult Requests(int page = 1, int perPage = 20)
    {

        var UserRequests = (from r in _context.Requests
                            join d in _context.Datasets on r.DatasetId equals d.Id
                            join a in _context.Approvals on r.Id equals a.RequestId into Approvals
                            from a in Approvals.DefaultIfEmpty()
                            where r.UserId == _userManager.GetUserId(User)
                            orderby r.Timestamp
                            select new RequestsDto
                            {
                                Title = d.Title,
                                Approved = a!= null ? a.Approved : false,
                                Reason = a != null ? a.RejectedReason : "Pending",
                                ReqTime = r.Timestamp,
                                AppTime = a.Timestamp != null ? a.Timestamp.ToString("dd/MM/yyyy HH:mm:ss") : "Pending Approval",
                                AppExp = a.Expires != null ? a.Expires.ToString("dd/MM/yyyy HH:mm:ss") : String.Empty,
                                ViewDataset = r.Approval != null ? String.Empty : "disabled" 
                            }).ToList();
        
        int totalItems = UserRequests.Count();
        Console.WriteLine(totalItems);

        var model = new Paginated<RequestsDto> {
            CurrentPage = page,
            PerPage = perPage,
            TotalItems = totalItems,
            Items = UserRequests,
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult RequestDocument()
    {
        List<Dataset> datasets = _context.Datasets.ToList();
        return View(datasets);
    }

    [HttpPost]
        public IActionResult RequestDocument(int DatasetId, int AccessLevel, string Purpose)
    {
        var request = new Request
        {
            DatasetId = DatasetId,
            UserId = _userManager.GetUserId(User),
            Timestamp = DateTime.UtcNow
        };

        _context.Requests.Add(request);
        _context.SaveChanges();

        return RedirectToAction("Requests");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
