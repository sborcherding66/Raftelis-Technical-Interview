using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Raftelis_Interview_WebApp.Pages
{
    public class ThankyouModel : PageModel
    {
        private readonly ILogger<ThankyouModel> _logger;

        public ThankyouModel(ILogger<ThankyouModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
