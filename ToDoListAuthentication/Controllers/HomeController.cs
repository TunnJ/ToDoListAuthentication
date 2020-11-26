using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoListAuthentication.Data;
using ToDoListAuthentication.Models;

namespace ToDoListAuthentication.Controllers
{
    public class HomeController : Controller
    {

        //Constructor injection
        //This controller receives a DbContext object from dependcy injection
        private ApplicationDbContext context;
        public HomeController(ApplicationDbContext ctx) => context = ctx;

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();

            IQueryable<ToDo> query = context.ToDos;
            if (filters.HasName)
            {
                query = query.Where(t => t.Name == filters.Name);
            }
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            if (filters.HasSprintNumber)
            {
                query = query.Where(t => t.SprintNumber == filters.SprintNumber);
            }
            if (filters.HasPointValue)
            {
                query = query.Where(t => t.PointValue == filters.PointValue);
            }
            var tickets = query;
            return View(tickets);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Statuses = context.Statuses.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(ToDo ticket)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(ticket);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Statuses = context.Statuses.ToList();
                return View(ticket);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] string id, ToDo selected)
        {
            if (selected.StatusId == null)
            {
                context.ToDos.Remove(selected);
            }
            else
            {
                string newStatusId = selected.StatusId;
                selected = context.ToDos.Find(selected.Id);
                selected.StatusId = newStatusId;
                context.ToDos.Update(selected);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
        private readonly ILogger<HomeController> _logger;

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
