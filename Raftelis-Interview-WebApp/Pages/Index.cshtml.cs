using Microsoft.AspNetCore.Mvc.RazorPages;
using Raftelis_Interview_WebApp.Models;
using Raftelis_Interview_WebApp.Services;

public class IndexModel : PageModel
{
    public List<PropertyRecord>? PropertyRecords { get; set; }
    // This property will keep track of the current sorting state for the market value
    public string CurrentSort { get; set; } = "";

    public void OnGet(string sort = "", string currentSort = "")
    {
        PropertyRecords = PropertyDataService.LoadPropertyData();
        CurrentSort = currentSort; // Keep the current sort state to toggle between sorts

        switch (sort)
        {
            case "owner":
                PropertyRecords = PropertyDataService.SortByName(PropertyRecords);
                break;
            case "address":
                PropertyRecords = PropertyDataService.RemoveDuplicatesAndSortByAddress(PropertyRecords);
                break;
            case "marketValue":
                if (currentSort == "asc" || string.IsNullOrEmpty(currentSort))
                {
                    PropertyRecords = PropertyDataService.SortByMarketValueAsc(PropertyRecords);
                    CurrentSort = "desc"; // Prepare to toggle to descending on the next click
                }
                else if (currentSort == "desc")
                {
                    PropertyRecords = PropertyDataService.SortByMarketValueDesc(PropertyRecords);
                    CurrentSort = "asc"; // Reset to ascending for the next interaction
                }
                break;
            case "saleDate":
                if (currentSort == "asc" || string.IsNullOrEmpty(currentSort))
                {
                    PropertyRecords = PropertyDataService.SortBySaleDateAsc(PropertyRecords);
                    CurrentSort = "desc";
                }
                else if (currentSort == "desc")
                {
                    PropertyRecords = PropertyDataService.SortBySaleDateDesc(PropertyRecords);
                    CurrentSort = "asc";
                }
                break;
            case "salePrice":
                if (currentSort == "asc" || string.IsNullOrEmpty(currentSort))
                {
                    PropertyRecords = PropertyDataService.SortBySalePriceAsc(PropertyRecords);
                    CurrentSort = "desc";
                }
                else if (currentSort == "desc")
                {
                    PropertyRecords = PropertyDataService.SortBySalePriceDesc(PropertyRecords);
                    CurrentSort = "asc";
                }
                break;
            case "pin":
                if (currentSort == "asc" || string.IsNullOrEmpty(currentSort))
                {
                    PropertyRecords = PropertyDataService.SortByPINAsc(PropertyRecords);
                    CurrentSort = "desc";
                }
                else if (currentSort == "desc")
                {
                    PropertyRecords = PropertyDataService.SortByPINDesc(PropertyRecords);
                    CurrentSort = "asc";
                }
                break;
            // Add cases for other sorting criteria as needed
            default:
                // No additional action taken for the default case
                break;
        }
    }
}

