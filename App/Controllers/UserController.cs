using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UKHSA.Models;
using UKHSA.Shared;
using System.Threading.Tasks;

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

    public IActionResult Requests(int page = 1, int perPage = 5)
    {

        var UserRequests = (from r in _context.Requests
                            join d in _context.Datasets on r.DatasetId equals d.Id
                            //join a in _context.Approvals on r.Id equals a.RequestId
                            where r.UserId == _userManager.GetUserId(User)
                            orderby r.Timestamp descending
                            select new RequestsDto
                            {
                                Title = d.Title,
                                Approved = a != null ? a.Approved : null,
                                Reason = a != null ? (a.Approved ? "" : a.RejectedReason) : "Pending",
                                ReqTime = r.Timestamp,
                                AppTime = a != null ? a.Timestamp : null,
                                AppExp = (a != null && a.Approved == true)? a.Expires : null,
                                ViewDataset = (r.Approval.Approved != null && r.Approval.Approved != false) ? String.Empty : "disabled"
                            }).ToList();

        int totalItems = UserRequests.Count();
        Console.WriteLine(totalItems);

        var model = new Paginated<RequestsDto>
        {
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
    public async Task<IActionResult> RequestDocument(int DatasetId, int AccessLevel, string Purpose)
    {
        var userId = _userManager.GetUserId(User);
        var dataset = await _context.Datasets.FindAsync(DatasetId);

        var request = new Request
        {
            DatasetId = DatasetId,
            UserId = userId,
            Timestamp = DateTime.UtcNow
        };
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        

        if (AccessLevel == 0)
        {
            var approval = new Approval
            {
                Request = request,
                Approved = true,
                RejectedReason = "",
                Timestamp = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMonths(6)
            };
            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Requests");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
