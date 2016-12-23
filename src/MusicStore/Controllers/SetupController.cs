using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using MusicStore.Models;
using MusicStore.ViewModels;


namespace MusicStore.Controllers
{
    public class SetupController : Controller
    {
        private readonly IConfiguration _configuration;

        public SetupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            SetupViewModel model = new SetupViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(SetupViewModel model)
        {
            _configuration["ConnectionStrings:MySQLConnection"] = string.Format("Server={0},{1};Database={2};Uid={3};Pwd={4};Charset=utf8;", model.Server, model.Port, model.Dbname, model.Username, model.Password);
            var context = HttpContext.RequestServices;
            await SampleData.InitializeMusicStoreDatabaseAsync(context, true);
            return RedirectToAction("Index", "Home");
        }
    }
}
