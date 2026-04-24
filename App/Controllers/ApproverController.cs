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

    [HttpPost]
    public IActionResult ApproveRequest(int requestId)
    {
        var request = _context.Requests.Find(requestId);
        var approvalList = _context.Approvals.Where(a => a.RequestId == requestId);

        if (approvalList.Any())
        {
            var approval = approvalList.First();

            approval.Approved = true;
            approval.RejectedReason = "";
            approval.Timestamp = DateTime.UtcNow;
            approval.Expires = DateTime.UtcNow.AddMonths(6);

        }
        else
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
        _context.SaveChanges();

        return RedirectToAction("ApproveRequest");
    }

    [HttpGet]
    public IActionResult ApproveRequest(int page = 1, int perPage = 5)
    {
        var ApproveRequest = (from r in _context.Requests
                              join d in _context.Datasets on r.DatasetId equals d.Id
                              where !_context.Approvals.Any(a => a.RequestId == r.Id)
                              orderby r.Timestamp descending
                              select new ApproveRequestDto
                              {
                                  Id = r.Id,
                                  Title = d.Title,
                                  Username = r.User.Forename + " " + r.User.Surname, // need to change
                                  Timestamp = r.Timestamp
                              }).ToList();
        int totalItems = ApproveRequest.Count();

        var model = new Paginated<ApproveRequestDto>
        {
            CurrentPage = page,
            PerPage = perPage,
            TotalItems = totalItems,
            Items = ApproveRequest,
        };

        return View (model);
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
        var approvalList = _context.Approvals.Where(a => a.RequestId == requestId);

        if (approvalList.Any())
        {
            var approval = approvalList.First();

            approval.Approved = false;
            approval.RejectedReason = reason;
            approval.Timestamp = DateTime.UtcNow;
            approval.Expires = DateTime.UtcNow.AddMonths(6);
        }
        else
        {
            request.Approval = new Approval
            {
                Request = request,
                Approved = false,
                RejectedReason = reason,
                Timestamp = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMonths(6)
            };
        }
        _context.SaveChanges();

        _context.SaveChanges();

        return RedirectToAction("ApproveRequest");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}