using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestWork.Data;
using TestWork.Models;
using System.IO;
using TestWork.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using PusherServer;

namespace TestWork.Controllers
{
    [Authorize]
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<User> _userManager;

        public AdvertisementController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment,UserManager<User> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
          
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        // GET: News
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 3;   // количество элементов на странице

            IQueryable<Advertisement> source = _context.Advertisements;
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            AdvertisementViewModel viewModel = new AdvertisementViewModel
            {
                PageViewModel = pageViewModel,
                Advertisements = items
            };
            return View(viewModel);
        }
        [AllowAnonymous]
        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }
            var visitors = 0;

            if (System.IO.File.Exists("visitors.txt"))
            {
                string noOfVisitors = System.IO.File.ReadAllText("visitors.txt");
                visitors = Int32.Parse(noOfVisitors);
            }

            ++visitors;
            var visit_text = (visitors == 1) ? " просмотр" : " просмотров";

            System.IO.File.WriteAllText("visitors.txt", visitors.ToString());

            var options = new PusherOptions();
            options.Cluster = "PUSHER_APP_CLUSTER";

            var pusher = new Pusher(
            "PUSHER_APP_ID",
            "PUSHER_APP_KEY",
            "PUSHER_APP_SECRET", options);

            pusher.TriggerAsync(
            "general",
            "newVisit",
            new { visits = visitors.ToString(), message = visit_text });
            advertisement.Visits = visitors;
            ViewData["visitors"] = advertisement.Visits;
            ViewData["visitors_txt"] = visit_text;
            advertisement.ERR = (double)((advertisement.Dislikes + advertisement.Likes) * 100 / advertisement.Visits);
            _context.Update(advertisement);
            await _context.SaveChangesAsync();
            return View(advertisement);
        }
       

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,ImageFile,Company,Text,UserId")] Advertisement advertisement)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            advertisement.UserId = user.Id;
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(advertisement.ImageFile.FileName);
                string extension = Path.GetExtension(advertisement.ImageFile.FileName);
                advertisement.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await advertisement.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements.FindAsync(id);
            if (advertisements == null)
            {
                return NotFound();
            }
            return View(advertisements);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImageFile,Company,Text")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(advertisement.ImageFile.FileName);
                    string extension = Path.GetExtension(advertisement.ImageFile.FileName);
                    advertisement.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await advertisement.ImageFile.CopyToAsync(fileStream);
                    }
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvsExists(advertisement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisements == null)
            {
                return NotFound();
            }

            return View(advertisements);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisements.FindAsync(id);
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", advertisement.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvsExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
