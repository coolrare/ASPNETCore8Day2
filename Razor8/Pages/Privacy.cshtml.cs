using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor8.Pages
{
    public class PrivacyModel : PageModel
    {
        public string Now { get; set; }

        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;

            this.Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void OnGet()
        {
            this.Now = DateTime.Now.ToString("yyyy-MM-dd");
        }

        public void OnPost()
        {
            this.Now = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
    }

}
