using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Shared;
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
                            join a in _context.Approvals on r.Id equals a.RequestId into Approvals
                            from a in Approvals.DefaultIfEmpty()
                            where r.UserId == _userManager.GetUserId(User)
                            orderby r.Timestamp descending
                            select new RequestsDto
                            {
                                Id = r.Id,
                                DatasetId = d.Id,
                                AccessLevel = d.AccessLevel,
                                
                                Title = d.Title,
                                Approved = a != null ? a.Approved : null,
                                Reason = a != null ? (a.Approved ? "" : a.RejectedReason) : "Pending",
                                ReqTime = r.Timestamp,
                                AppTime = a != null ? a.Timestamp : null,
                                AppExp = (a != null && a.Approved == true)? a.Expires : null,
                                ViewDataset = (a != null && a.Approved == true) ? String.Empty : "disabled"
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
    public async Task<IActionResult> RequestDocument(int DatasetId, string Purpose)
    {
        var userId = _userManager.GetUserId(User);
        var dataset = await _context.Datasets.FindAsync(DatasetId);

        var request = new Request
        {
            DatasetId = DatasetId,
            UserId = userId,
            Reason = Purpose,
            Timestamp = DateTime.UtcNow
        };
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
        

        if (dataset.AccessLevel == 0)
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

    [HttpPost]
    public async Task<IActionResult> Extension(int RequestId, int DatasetId, int AccessLevel)
    {
        var userId = _userManager.GetUserId(User);
        var dataset = await _context.Datasets.FindAsync(DatasetId);

        if (AccessLevel == 0)
        {
            var approval = _context.Approvals.Where(a=> a.RequestId == RequestId)
            .First();

            approval.Expires = approval.Expires?.AddMonths(6);

            await _context.SaveChangesAsync();
        }
        else {
        var request = new Request
        {
            DatasetId = DatasetId,
            UserId = userId,
            Reason = "Extension Request",
            Timestamp = DateTime.UtcNow
        };

        _context.Requests.Add(request);
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
