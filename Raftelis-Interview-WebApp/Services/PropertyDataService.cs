using Raftelis_Interview_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
// 6 lines were removed! FIGURE OUT WHY!!!!!!!!
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

            // Remove duplicates based on PIN and sort
            var uniqueRecords = records
                .GroupBy(record => record.Pin)
                .Select(group => group.First())
                .OrderBy(record => record.StreetName)
                .ThenBy(record => record.StreetNumber)
                .ToList();

            return uniqueRecords;
        }
    }
}
