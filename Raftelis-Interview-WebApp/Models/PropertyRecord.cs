namespace Raftelis_Interview_WebApp.Models
{
    public class PropertyRecord
    {
        public string Pin { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public decimal MarketValue { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public string Link { get; set; }

        // Property for Google Maps link
        public string GoogleMapsLink => $"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(Address)}";
    }
}
