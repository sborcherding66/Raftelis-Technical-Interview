using Raftelis_Interview_WebApp.Models;
using System.Globalization;

namespace Raftelis_Interview_WebApp.Services
{
    // Handles loading and processing property data from a text file.
    public class PropertyDataService
    {
        // Loads property data, and preprocesses it.
        public static List<PropertyRecord> LoadPropertyData()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Parcels.txt");
            var records = new List<PropertyRecord>();
            var lines = File.ReadAllLines(filePath);
            int lineNumber = 0;

            // Loop through the records skipping the header
            foreach (var line in lines.Skip(1))
            {
                lineNumber++;
                var columns = line.Split('|');

                // Skip the record if essential data (PIN, Address, or Link) is missing.
                if (string.IsNullOrWhiteSpace(columns[0]) ||
                    string.IsNullOrWhiteSpace(columns[1]) ||
                    string.IsNullOrWhiteSpace(columns[6]))
                {
                    Console.WriteLine($"Essential data missing on line {lineNumber}. Skipping record.");
                    continue;
                }

                // Attempt to parse the sale date, skipping the record on failure.
                DateTime? saleDate = null;
                if (!string.IsNullOrEmpty(columns[4]))
                {
                    try
                    {
                        saleDate = DateTime.ParseExact(columns[4], "M/d/yyyy", CultureInfo.InvariantCulture);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Error parsing date on line {lineNumber}: {ex.Message}");
                        continue;
                    }
                }

                // Parse market value, defaulting to null if parsing fails.
                decimal? marketValue = null;
                if (decimal.TryParse(columns[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValueMV))
                {
                    marketValue = parsedValueMV;
                }

                // Parse sale price, defaulting to null if parsing fails.
                decimal? salePrice = null;
                if (decimal.TryParse(columns[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValueSP))
                {
                    salePrice = parsedValueSP;
                }

                // Create and add the property record.
                var record = new PropertyRecord
                {
                    Pin = columns[0],
                    Address = columns[1],
                    Owner = string.IsNullOrWhiteSpace(columns[2]) ? null : columns[2].Trim(),
                    MarketValue = marketValue,
                    SaleDate = saleDate,
                    SalePrice = salePrice,
                    Link = columns[6]
                };

                record.ParseAddress(); // Parse and set StreetName and StreetNumber
                records.Add(record);
            }

            // Remove duplicate records and sort by address before returning.
            return RemoveDuplicatesAndSortByAddress(records);
        }


        // Sort records alphabetically by first name
        public static List<PropertyRecord> SortByName(List<PropertyRecord> records)
        {
            foreach (var record in records)
            {
                record.GenerateSortableName();
            }

            return records.OrderBy(r => r.SortableName).ToList();
        }

        // Sort records by first name reverse alphabetically.
        public static List<PropertyRecord> SortByNameDesc(List<PropertyRecord> records)
        {
            foreach (var record in records)
            {
                record.GenerateSortableName();
            }
        
            return records.OrderBy(r => r.SortableName).Reverse().ToList();
        }


        // Eliminates duplicate records by PIN and sorts by address alphabetically then numerically.
        public static List<PropertyRecord> RemoveDuplicatesAndSortByAddress(List<PropertyRecord> records)
        {
            return records.GroupBy(record => record.Pin)
                          .Select(group => group.First())
                          .OrderBy(record => record.StreetName)
                          .ThenBy(record => record.StreetNumber)
                          .ToList();
        }

        // Same method as above but reversed
        public static List<PropertyRecord> RemoveDuplicatesAndSortByAddressDesc(List<PropertyRecord> records)
        {
            return records.GroupBy(record => record.Pin)
                          .Select(group => group.First())
                          .OrderBy(record => record.StreetName)
                          .ThenBy(record => record.StreetNumber)
                          .Reverse()
                          .ToList();
        }


        // Sorts records by market value in ascending order.
        public static List<PropertyRecord> SortByMarketValueAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.MarketValue).ToList();
        }
        // Sorts records by market value in descending order.
        public static List<PropertyRecord> SortByMarketValueDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.MarketValue).ToList();
        }


        // Sorts records by sale date in ascending order.
        public static List<PropertyRecord> SortBySaleDateAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.SaleDate).ToList();
        }
        // Sorts records by sale date in descending order.
        public static List<PropertyRecord> SortBySaleDateDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.SaleDate).ToList();
        }


        // Sorts records by sale price in ascending order.
        public static List<PropertyRecord> SortBySalePriceAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.SalePrice).ToList();
        }
        // Sorts records by sale price in descending order.
        public static List<PropertyRecord> SortBySalePriceDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.SalePrice).ToList();
        }


        // Sorts records by PIN in ascending order.
        public static List<PropertyRecord> SortByPINAsc(List<PropertyRecord> records)
        {
            return records.OrderBy(r => r.Pin).ToList();
        }
        // Sorts records by PIN in descending order.
        public static List<PropertyRecord> SortByPINDesc(List<PropertyRecord> records)
        {
            return records.OrderByDescending(r => r.Pin).ToList();
        }
    }
}
