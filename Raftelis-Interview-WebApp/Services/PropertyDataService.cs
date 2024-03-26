// Services/PropertyDataService.cs
using Raftelis_Interview_WebApp.Models;
using System.Globalization;
using System.IO; // Make sure to include this for File operations

namespace Raftelis_Interview_WebApp.Services
{
    public class PropertyDataService
    {
        public static List<PropertyRecord> LoadPropertyData()
        {
            // Construct the path to the data file.
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Parcels.txt");

            var records = new List<PropertyRecord>();
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1)) // first line is headers
            {
                var columns = line.Split('|');
                var record = new PropertyRecord
                {
                    Pin = columns[0],
                    Address = columns[1],
                    Owner = columns[2],
                    MarketValue = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                    SaleDate = DateTime.ParseExact(columns[4], "M/d/yyyy", CultureInfo.InvariantCulture),
                    SalePrice = decimal.Parse(columns[5], CultureInfo.InvariantCulture),
                    Link = columns[6]
                };
                records.Add(record);
            }

            return records;
        }
    }
}
