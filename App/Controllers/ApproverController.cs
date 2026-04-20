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

    [HttpGet]
    public IActionResult ApproveRequest(int page = 1, int perPage = 20)
    {

        //int totalItems = _context.Requests.Count();
        //var requests = _context.Requests.ToList();
        var ApproveRequest =    (from r in _context.Requests
                                join d in _context.Datasets on r.DatasetId equals d.Id
                                join u in _context.Users on r.UserId equals u.Id
                                orderby r.Timestamp descending
                                select new ApproveRequestDto
                                {
                                    Id = r.Id,
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

        return RedirectToAction("ApproveRequest");
    }

    [HttpPost]
    public IActionResult ApproveRequest(int requestId)
    {
        var request = _context.Requests.Find(requestId);

        if (request == null)
        {
        return View (model);
        }

        if (request.Approval == null)
        {
            request.Approval = new Approval
            {
                Request = request,
                Approved = true,
                RejectedReason = null,
                Timestamp = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddYears(1)
            };
        }
        else
        {
        request.Approval.Approved = true;
        request.Approval.RejectedReason = null;
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

        _context.SaveChanges();

        return RedirectToAction("ApproveRequest");
    }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}