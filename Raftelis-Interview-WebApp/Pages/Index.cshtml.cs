using Microsoft.AspNetCore.Mvc.RazorPages;
using Raftelis_Interview_WebApp.Models;
using Raftelis_Interview_WebApp.Services;

public class IndexModel : PageModel
{
    public List<PropertyRecord>? PropertyRecords { get; set; }

    public void OnGet()
    {
        PropertyRecords = PropertyDataService.LoadPropertyData();
    }
}
