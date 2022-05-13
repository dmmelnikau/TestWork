using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Data;
using TestWork.Models;

namespace TestWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> EffERR()
        {
            var advertisementC = from s in _context.Advertisements
                                select s;

            
            return View( await advertisementC.ToListAsync());
        }
        public async Task<IActionResult> Index(string searchString, string sortOrder, int? likefilter, int? dislikefilter)
        {
        
            ViewData["LikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "like_desc" : "";
            ViewData["DislikeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "dislike_desc" : "dislike_order";
            ViewData["AdvFilter"] = searchString;
            ViewData["LikeFilter"] = likefilter;
            ViewData["DislikeFilter"] = dislikefilter;
            var advertisementContext = from s in _context.Advertisements
                                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisementContext = advertisementContext.Where(s => s.Company.Contains(searchString)
                                   //    || s.Title.Contains(searchString)
                                       );
            }
            if (likefilter != null && likefilter != 0)
            {
                advertisementContext = advertisementContext.Where(p => p.Likes > likefilter);
            }
            if (dislikefilter != null && dislikefilter != 0)
            {
                advertisementContext = advertisementContext.Where(p => p.Dislikes < dislikefilter);
            }
            switch (sortOrder)
            {
                case "like_desc":
                    advertisementContext = advertisementContext.OrderByDescending(s => s.Likes);
                    break;
                case "dislike_desc":
                    advertisementContext = advertisementContext.OrderByDescending(s => s.Dislikes);
                    break;
                case "dislike_order":
                    advertisementContext = advertisementContext.OrderBy(s => s.Dislikes);
                    break;
                default:
                    advertisementContext = advertisementContext.OrderBy(s => s.Likes);
                    break;
            }
          
            return View(await advertisementContext.AsNoTracking().ToListAsync());
        }

        
        public async Task<List<Advertisement>> Edit(int id, bool like, bool dislike)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (like)
            {
                advertisement.Likes++;
            }
           else if (!like)
            {
                advertisement.Likes--;
            }
            else if(dislike)
            {
                advertisement.Dislikes++;
            }
            else
            {
                advertisement.Dislikes--;
            }
            _context.Update(advertisement);
            await _context.SaveChangesAsync();
            return  _context.Advertisements.ToList();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
