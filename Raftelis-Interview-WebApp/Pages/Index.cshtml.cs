using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Raftelis_Interview_WebApp.Models;
using Raftelis_Interview_WebApp.Services;

// Defines the model for the main index page, handling data loading and sorting.
public class IndexModel : PageModel
{
    // Holds the list of property records to display.
    public List<PropertyRecord>? PropertyRecords { get; set; }

    // Properties to keep track of the current sort field and its direction
    public string CurrentSortField { get; set; } = "address";
    public string CurrentSortOrder { get; set; } = "asc";
    public Dictionary<string, HtmlString> SortIndicators { get; set; } = new Dictionary<string, HtmlString>();

    public void OnGet(string sortField = "", string sortOrder = "asc")
    {
        // Load the initial set of data
        PropertyRecords = PropertyDataService.LoadPropertyData();

        // Update the current sort state based on query parameters
        if (!string.IsNullOrEmpty(sortField))
        {
            CurrentSortField = sortField;
            CurrentSortOrder = sortOrder;
        }

        // Apply sorting based on the current sort field and order
        switch (CurrentSortField)
        {
            case "owner":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.SortByName(PropertyRecords) :
                    PropertyDataService.SortByNameDesc(PropertyRecords);
                break;
            case "address":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.RemoveDuplicatesAndSortByAddress(PropertyRecords) :
                    PropertyDataService.RemoveDuplicatesAndSortByAddressDesc(PropertyRecords);
                break;
            case "marketValue":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.SortByMarketValueAsc(PropertyRecords) :
                    PropertyDataService.SortByMarketValueDesc(PropertyRecords);
                break;
            case "saleDate":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.SortBySaleDateAsc(PropertyRecords) :
                    PropertyDataService.SortBySaleDateDesc(PropertyRecords);
                break;
            case "salePrice":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.SortBySalePriceAsc(PropertyRecords) :
                    PropertyDataService.SortBySalePriceDesc(PropertyRecords);
                break;
            case "pin":
                PropertyRecords = sortOrder == "asc" ?
                    PropertyDataService.SortByPINAsc(PropertyRecords) :
                    PropertyDataService.SortByPINDesc(PropertyRecords);
                break;
            default:
               
                break;
        }

        // Prepare sort indicators for all sortable fields
        PrepareSortIndicators();
    }

    // Prepares the HTML indicators showing the current sort state for each field.
    private void PrepareSortIndicators()
    {
        var sortableFields = new List<string> { "pin", "address", "owner", "marketValue", "saleDate", "salePrice" };
        foreach (var field in sortableFields)
        {
            SortIndicators[field] = SortIndicator(field, CurrentSortField, CurrentSortOrder);
        }
    }

    // Generates a sort indicator HTML string for the current sort field and order.
    public HtmlString SortIndicator(string field, string currentSortField, string currentSortOrder)
    {
        if (field != currentSortField)
        {
            return new HtmlString(""); // No indicator if not the current sort field
        }

        string indicatorHtml;
        switch (field)
        {
            case "pin":
            case "marketValue":
            case "salePrice":
                indicatorHtml = currentSortOrder == "asc" ? "<span class='sort-indicator'>(Low to High)</span>" : "<span class='sort-indicator'>(High to Low)</span>";
                break;
            case "saleDate":
                indicatorHtml = currentSortOrder == "asc" ? "<span class='sort-indicator'>(Oldest to Newest)</span>" : "<span class='sort-indicator'>(Newest to Oldest)</span>";
                break;
            case "address":
            case "owner":
                indicatorHtml = currentSortOrder == "asc" ? "<span class='sort-indicator'>(A to Z)</span>" : "<span class='sort-indicator'>(Z to A)</span>";
                break;
            default:
                indicatorHtml = ""; // Fallback case
                break;
        }

        return new HtmlString(indicatorHtml);
    }

}

