namespace Mvc8.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRegistraionService registraionService;

    public HomeController(ILogger<HomeController> logger, IRegistraionService registraionService)
    {
        _logger = logger;
        this.registraionService = registraionService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Privacy()
    {
        ViewBag.Message = registraionService.RegisterUser("John Doe");

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
