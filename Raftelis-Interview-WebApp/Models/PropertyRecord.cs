using System;
using System.Text.RegularExpressions; // Add this for the regex parsing

namespace Raftelis_Interview_WebApp.Models
{
    public class PropertyRecord
    {
        public string? Pin { get; set; }
        public string? Address { get; set; }
        public string? Owner { get; set; }
        public decimal MarketValue { get; set; }
        public DateTime? SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public string? Link { get; set; }

        // Properties for sorting
        public string StreetName { get; private set; } = string.Empty;
        public int? StreetNumber { get; private set; }

        // Property for Google Maps link
        public string GoogleMapsLink => $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(Address ?? string.Empty)}";

        public void ParseAddress()
        {
            if (Address is null) return; // Early return if Address is null

            var match = Regex.Match(Address, @"^(\d+)[-\s]*\s*(.*)$");
            if (match.Success)
            {
                StreetName = match.Groups[2].Value;
                if (int.TryParse(match.Groups[1].Value, out var number))
                {
                    StreetNumber = number;
                }
            }
            else
            {
                StreetName = Address; // Use the full address if parsing fails
            }
        }
    }
}