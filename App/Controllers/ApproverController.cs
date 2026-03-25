using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using App.Models;
using App.Shared;

namespace App.Controllers;

[Authorize(Roles = "Approver, Admin")]
public class ApproverController : Controller
{
     protected readonly UKHSA_DbContext _context;
    private readonly UserManager<User> _userManager;

    public ApproverController(UKHSA_DbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public IActionResult ApproveRequest(int page = 1, int perPage = 20)
    {

        //int totalItems = _context.Requests.Count();
        //var requests = _context.Requests.ToList();
        var ApproveRequest =    (from r in _context.Requests
                                join d in _context.Datasets on r.DatasetId equals d.Id
                                orderby r.Timestamp descending
                                select new ApproveRequestDto
                                {
                                    Title = d.Title,
                                    Username = r.User.Forename + " " + r.User.Surname, // need to change
                                    Timestamp = r.Timestamp
                                }).ToList();
        int totalItems = ApproveRequest.Count();

        var model = new Paginated<ApproveRequestDto> {
            CurrentPage = page,
            PerPage = perPage,
            TotalItems = totalItems,
            Items = ApproveRequest,
        };

        return View (model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var request = _context.Requests
        .Include(r => r.Approval)
        .FirstOrDefault(r => r.Id == requestId);


        if (request == null)
        {
        return RedirectToAction (nameof(ApproveRequest));
        }

        if (request.Approval == null)
        {
            request.Approval = new Approval
            {
                Request = request,
                Approved = true,
                RejectedReason = "",
                Timestamp = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMonths(6)
            };
        }
        else
        {
        request.Approval.Approved = true;
        request.Approval.RejectedReason = "";
        request.Approval.Timestamp = DateTime.UtcNow;
        }

        _context.SaveChanges();

        return RedirectToAction("ApproveRequest");
    }

    [HttpGet]
    public IActionResult DenyRequest(int requestId)
    {
        var request = _context.Requests.Find(requestId);
        return View(request);
    }

    [HttpPost]
    public IActionResult DenyRequest(int requestId, string reason)
    {
        var request = _context.Requests.Find(requestId);

        if (request.Approval == null)
        {
            request.Approval = new Approval
            {
                Request = request,
                RejectedReason = reason,
                Approved = false,
                Timestamp = DateTime.UtcNow
            };
        }
        else
        {
        request.Approval.Approved = false;
        request.Approval.RejectedReason = reason;
        request.Approval.Timestamp = DateTime.UtcNow;
        }

        return RedirectToAction("ApproveRequest");
    }

    [HttpGet]
    public IActionResult DenyRequest(int requestId)
    {
        var request = _context.Requests.Find(requestId);
        return View(request);
    }

    [HttpPost]
    public IActionResult DenyRequest(int requestId, string reason)
    {
        var request = _context.Requests.Find(requestId);

        if (request.Approval == null)
        {
            request.Approval = new Approval
            {
                Request = request,
                RejectedReason = reason,
                Approved = false,
                Timestamp = DateTime.UtcNow
            };
        }
        else
        {
        request.Approval.Approved = false;
        request.Approval.RejectedReason = reason;
        request.Approval.Timestamp = DateTime.UtcNow;
        }

        return RedirectToAction("ApproveRequest");
    }
}
