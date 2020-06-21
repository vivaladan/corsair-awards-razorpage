using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.VisualBasic.FileIO;

namespace CorsairAwards.Services
{
    public class CsvParser
    {
        public List<Sample> ParseCsv(Stream stream)
        {
            using var parser = new TextFieldParser(stream);
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;

            // skip the header
            parser.ReadLine();

            var samples = new List<Sample>();
            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();

                samples.Add(new Sample
                {
                    Year = fields[0],
                    Quarter = fields[1],
                    Region = fields[2],
                    Country = fields[3],
                    Requestor = fields[4],
                    Site = fields[5],
                    SiteRanking = fields[6],
                    OrderType = fields[7],
                    Month = fields[8],
                    Category = fields[9],
                    PartDescription = fields[10],
                    PartNumber = fields[11],
                    SONumber = fields[12],
                    OrderDate = ParseDate(fields[13]),
                    Specialty = fields[14],
                    Purpose = fields[15],
                    Action = fields[16],
                    SampleCost = fields[17],
                    ShipmentDate = ParseDate(fields[18]),
                    ShipmentStatus = fields[19],
                    ShipTurnaround = fields[20],
                    Aging = fields[21],
                    LaunchDate = ParseDate(fields[22]),
                    CorrectedShipLaunchDate = ParseDate(fields[23]),
                    PublishedMonth = fields[24],
                    ShippedDate = fields[25],
                    PublishDate = ParseDate(fields[26]),
                    Followup = fields[27],
                    PublishTurnaround = fields[28],
                    ReviewUrl = fields[29],
                    AwardUrl = fields[30],
                    AwardRank = fields[31],
                    WinsAnAward = fields[32],
                    AwardDescription = fields[33],
                    Licence = fields[34],
                    Quote = fields[35],
                    Rating = fields[36],
                    EstimatedViews = fields[37],
                    YouTubeViews = fields[38],
                    Likes = fields[39],
                    Followers = fields[40],
                    VideoContent = fields[41],
                });
            }

            return samples;
        }
        
        private static DateTime? ParseDate(string value) =>
            DateTime.TryParse(value, new CultureInfo("en-US"), DateTimeStyles.None, out var parsed)
                ? parsed : (DateTime?) null;
    }

    public class Sample
    {
        public string Year { get; set; }
        public string Quarter { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Requestor { get; set; }
        public string Site { get; set; }
        public string SiteRanking { get; set; }
        public string OrderType { get; set; }
        public string Month { get; set; }
        public string Category { get; set; }
        public string PartDescription { get; set; }
        public string PartNumber { get; set; }
        public string SONumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string Specialty { get; set; }
        public string Purpose { get; set; }
        public string Action { get; set; }
        public string SampleCost { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string ShipmentStatus { get; set; }
        public string ShipTurnaround { get; set; }
        public string Aging { get; set; }
        public DateTime? LaunchDate { get; set; }
        public DateTime? CorrectedShipLaunchDate { get; set; }
        public string PublishedMonth { get; set; }
        public string ShippedDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Followup { get; set; }
        public string PublishTurnaround { get; set; }
        public string ReviewUrl { get; set; }
        public string AwardUrl { get; set; }
        public string AwardRank { get; set; }
        public string WinsAnAward { get; set; }
        public string AwardDescription { get; set; }
        public string Licence { get; set; }
        public string Quote { get; set; }
        public string Rating { get; set; }
        public string EstimatedViews { get; set; }
        public string YouTubeViews { get; set; }
        public string Likes { get; set; }
        public string Followers { get; set; }
        public string VideoContent { get; set; }
    }
}