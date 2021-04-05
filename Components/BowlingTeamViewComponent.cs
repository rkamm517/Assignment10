using Assignment10.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Components
{
    //View Component Class
    public class BowlingTeamViewComponent : ViewComponent
    {
        private BowlingLeagueContext _context;
        public BowlingTeamViewComponent(BowlingLeagueContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
