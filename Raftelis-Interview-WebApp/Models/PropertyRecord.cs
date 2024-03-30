using System.Text.RegularExpressions;

namespace Raftelis_Interview_WebApp.Models
{
    // Represents a single property record with related data and functionalities.
    public class PropertyRecord
    {
        public string? Pin { get; set; }
        public string? Address { get; set; }
        public string? Owner { get; set; }
        public decimal? MarketValue { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal? SalePrice { get; set; }
        public string? Link { get; set; }

        // Additional properties for enhanced sorting and display functionalities.
        public string StreetName { get; private set; } = string.Empty;
        public int? StreetNumber { get; private set; }
        // Generates a Google Maps link for the property based on its address.
        public string GoogleMapsLink => $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString($"{Address ?? string.Empty}, Mazama WA")}";


        public string? SortableName { get; set; } // Used for sorting records by name.

        // Parses the address to extract and set the StreetName and StreetNumber.
        public void ParseAddress()
        {
            // Skip processing if Address is not provided.
            if (Address is null) return;

            // Regex to separate street number from the rest of the address.
            var match = Regex.Match(Address, @"^(\d+)[-\s]*\s*(.*)$");
            if (match.Success)
            {
                StreetName = match.Groups[2].Value;
                StreetNumber = int.TryParse(match.Groups[1].Value, out var number) ? number : null;
            }
            else
            {
                // Use full address as StreetName if specific parsing fails.
                StreetName = Address;
            }
        }

        // Generates the first name for sorting.
        public void GenerateSortableName()
        {
            // Edge case where Owner information is missing.
            if (string.IsNullOrWhiteSpace(Owner))
            {
                SortableName = string.Empty;
                return;
            }

            // Specific logic for properties owned by LLCs.
            if (Owner.ToUpper().Contains("LLC"))
            {
                SortableName = Owner.Split(' ')[0];
            }
            else
            {
                var names = Owner.Split(',');
                SortableName = names.Length > 1 && names[1].Split(' ').Length > 1 ? names[1].Split(' ')[1] : Owner;
            }
        }
    }
}