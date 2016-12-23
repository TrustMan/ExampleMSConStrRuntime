using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly MusicStoreContext dbContext; //add line

        //public HomeController(IOptions<AppSettings> options) DELETE LINE
        public HomeController(MusicStoreContext context, IOptions<AppSettings> options) //add line
        {
            _appSettings = options.Value;
            //add 3 line
            if (context.ProviderSpecified)
            {
                dbContext = context;
            }
        }
        //
        // GET: /Home/
        //public async Task<IActionResult> Index([FromServices] MusicStoreContext dbContext,[FromServices] IMemoryCache cache) DELETE LINE
        public async Task<IActionResult> Index([FromServices] IMemoryCache cache) //add line
        {
            if (dbContext != null)          //add line
            {                               //add line
                // Get most popular albums
                var cacheKey = "topselling";
                List<Album> albums;
                if (!cache.TryGetValue(cacheKey, out albums))
                {
                    albums = await GetTopSellingAlbumsAsync(dbContext, 6);

                    if (albums != null
                        && albums.Count > 0)
                    {
                        if (_appSettings.CacheDbResults)
                        {
                            // Refresh it every 10 minutes.
                            // Let this be the last item to be removed by cache if cache GC kicks in.
                            cache.Set(
                                cacheKey,
                                albums,
                                new MemoryCacheEntryOptions()
                                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                                    .SetPriority(CacheItemPriority.High));
                        }
                    }
                }

                return View(albums);
            }                           //add next 5 lines
            else
            {
                return RedirectToAction("Index", "Setup");
            }
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        private Task<List<Album>> GetTopSellingAlbumsAsync(MusicStoreContext dbContext, int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return dbContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count)
                .Take(count)
                .ToListAsync();
        }
    }
}