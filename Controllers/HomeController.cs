using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext context)
        {
            _logger = logger;
            _context = context;
        }

        //Index view controller method
        public IActionResult Index(int? teamId, string teamname, int pageNum = 0)
        {
            //Set number of items per page
            int pageSize = 5;
            
            //Create a new View Model instance with the data for the index view
            return View(new IndexViewModel

            {
                Teams = (_context.Teams
                .Where(x => x.TeamId == teamId || teamId == null)
                .OrderBy(x => x.TeamName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                //Set information for Page Numbering
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    TotalNumItems = (teamId == null ? _context.Teams.Count() :
                        _context.Teams.Where(x => x.TeamId == teamId).Count())
                },

                TeamName = teamname
            }) ;
                
               
        }

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
