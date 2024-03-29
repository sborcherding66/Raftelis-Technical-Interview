using Raftelis_Interview_WebApp.Models;
using System.Globalization;
using System.Linq;

namespace Raftelis_Interview_WebApp.Services
{
    public class PropertyDataService
    {
        public static List<PropertyRecord> LoadPropertyData()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Parcels.txt");

            var records = new List<PropertyRecord>();
            var lines = File.ReadAllLines(filePath);
            int lineNumber = 0;

            foreach (var line in lines.Skip(1)) // Skip the header line
            {
                lineNumber++;
                var columns = line.Split('|');
                DateTime? saleDate = null;

                if (!string.IsNullOrEmpty(columns[4])) // Check if the date string empty
                {
                    try
                    {
                        saleDate = DateTime.ParseExact(columns[4], "M/d/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing date on line {lineNumber}: {ex.Message}");
                        continue; // Skip to the next iteration on error
                    }
                }

                var record = new PropertyRecord
                {
                    Pin = columns[0],
                    Address = columns[1],
                    Owner = columns[2],
                    MarketValue = decimal.Parse(columns[3], CultureInfo.InvariantCulture),
                    SaleDate = saleDate,
                    SalePrice = decimal.Parse(columns[5], CultureInfo.InvariantCulture),
                    Link = columns[6]
                };

                record.ParseAddress(); // Parse and set StreetName and StreetNumber
                records.Add(record);
            }

            // Initial unique sort
            return RemoveDuplicatesAndSortByAddress(records);
        }


        public static List<PropertyRecord> SortByName(List<PropertyRecord> records)
        {
            foreach (var record in records)
            {
                record.GenerateSortableName();
            }

            return records.OrderBy(r => r.SortableName).ToList();
        }
        public static List<PropertyRecord> SortByNameDesc(List<PropertyRecord> records)
        {
            foreach (var record in records)
            {
                record.GenerateSortableName();
            }
        
            return records.OrderBy(r => r.SortableName).Reverse().ToList();
        }


        public static List<PropertyRecord> RemoveDuplicatesAndSortByAddress(List<PropertyRecord> records)
        {
            // Remove duplicates based on PIN and sort by address
            return records.GroupBy(record => record.Pin)
                          .Select(group => group.First())
                          .OrderBy(record => record.StreetName)
                          .ThenBy(record => record.StreetNumber)
                          .ToList();
        }
        public static List<PropertyRecord> RemoveDuplicatesAndSortByAddressDesc(List<PropertyRecord> records)
        {
            // Remove duplicates based on PIN and sort by address then reverse
            return records.GroupBy(record => record.Pin)
                          .Select(group => group.First())
                          .OrderBy(record => record.StreetName)
                          .ThenBy(record => record.StreetNumber)
                          .Reverse()
                          .ToList();
        }


        public static List<PropertyRecord> SortByMarketValueAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.MarketValue).ToList();
        }
        public static List<PropertyRecord> SortByMarketValueDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.MarketValue).ToList();
        }


        public static List<PropertyRecord> SortBySaleDateAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.SaleDate).ToList();
        }
        public static List<PropertyRecord> SortBySaleDateDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.SaleDate).ToList();
        }


        public static List<PropertyRecord> SortBySalePriceAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.SalePrice).ToList();
        }
        public static List<PropertyRecord> SortBySalePriceDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.SalePrice).ToList();
        }


        public static List<PropertyRecord> SortByPINAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.Pin).ToList();
        }
        public static List<PropertyRecord> SortByPINDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.Pin).ToList();
        }
    }
}
