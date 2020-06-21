using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CorsairAwards.Services
{
    public class AzureSqlRepository : IRepository
    {
        private readonly string connectionString;

        public AzureSqlRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Sample>> GetSamples()
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<Sample>("SELECT * FROM samples");
        }

        public async Task InsertOrUpdateSamples(IEnumerable<Sample> samples)
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            
            await SqlBulkCopy(connection, samples);
            

            await connection.ExecuteAsync("MERGE Samples t USING #Samples s ON (s.FileName = t.FileName AND s.FileRow = t.FileRow) WHEN MATCHED THEN UPDATE SET t.Year = s.Year, t.Quarter = s.Quarter, t.Region = s.Region, t.Country = s.Country, t.Requestor = s.Requestor, t.Site = s.Site, t.SiteRanking = s.SiteRanking, t.OrderType = s.OrderType, t.Month = s.Month, t.Category = s.Category, t.PartDescription = s.PartDescription, t.PartNumber = s.PartNumber, t.SONumber = s.SONumber, t.OrderDate = s.OrderDate, t.Specialty = s.Specialty, t.Purpose = s.Purpose, t.Action = s.Action, t.SampleCost = s.SampleCost, t.ShipmentDate = s.ShipmentDate, t.ShipmentStatus = s.ShipmentStatus, t.ShipTurnaround = s.ShipTurnaround, t.Aging = s.Aging, t.LaunchDate = s.LaunchDate, t.CorrectedShipLaunchDate = s.CorrectedShipLaunchDate, t.PublishedMonth = s.PublishedMonth, t.ShippedDate = s.ShippedDate, t.PublishDate = s.PublishDate, t.Followup = s.Followup, t.PublishTurnaround = s.PublishTurnaround, t.ReviewUrl = s.ReviewUrl, t.AwardUrl = s.AwardUrl, t.AwardRank = s.AwardRank, t.WinsAnAward = s.WinsAnAward, t.AwardDescription = s.AwardDescription, t.Licence = s.Licence, t.Quote = s.Quote, t.Rating = s.Rating, t.EstimatedViews = s.EstimatedViews, t.YouTubeViews = s.YouTubeViews, t.Likes = s.Likes, t.Followers = s.Followers, t.VideoContent = s.VideoContent WHEN NOT MATCHED THEN INSERT VALUES (s.FileName, s.FileRow, s.Year, s.Quarter, s.Region, s.Country, s.Requestor, s.Site, s.SiteRanking, s.OrderType, s.Month, s.Category, s.PartDescription, s.PartNumber, s.SONumber, s.OrderDate, s.Specialty, s.Purpose, s.Action, s.SampleCost, s.ShipmentDate, s.ShipmentStatus, s.ShipTurnaround, s.Aging, s.LaunchDate, s.CorrectedShipLaunchDate, s.PublishedMonth, s.ShippedDate, s.PublishDate, s.Followup, s.PublishTurnaround, s.ReviewUrl, s.AwardUrl, s.AwardRank, s.WinsAnAward, s.AwardDescription, s.Licence, s.Quote, s.Rating, s.EstimatedViews, s.YouTubeViews, s.Likes, s.Followers, s.VideoContent);");
        }

        private static async Task SqlBulkCopy(SqlConnection connection, IEnumerable<Sample> samples)
        {
            await connection.ExecuteAsync("CREATE TABLE #Samples ( Id int IDENTITY PRIMARY KEY, FileName VARCHAR(50) NOT NULL, FileRow INT NOT NULL, Year VARCHAR(50), Quarter VARCHAR(50), Region VARCHAR(50), Country VARCHAR(50), Requestor VARCHAR(50), Site VARCHAR(50), SiteRanking VARCHAR(50), OrderType VARCHAR(50), Month VARCHAR(50), Category VARCHAR(50), PartDescription VARCHAR(100), PartNumber VARCHAR(50), SONumber VARCHAR(50), OrderDate DATETIME2, Specialty VARCHAR(100), Purpose VARCHAR(50), Action VARCHAR(50), SampleCost VARCHAR(50), ShipmentDate DATETIME2, ShipmentStatus VARCHAR(50), ShipTurnaround VARCHAR(50), Aging VARCHAR(50), LaunchDate DATETIME2, CorrectedShipLaunchDate DATETIME2, PublishedMonth VARCHAR(50), ShippedDate DATETIME2, PublishDate DATETIME2, Followup VARCHAR(50), PublishTurnaround VARCHAR(50), ReviewUrl VARCHAR(500), AwardUrl VARCHAR(500), AwardRank VARCHAR(50), WinsAnAward VARCHAR(50), AwardDescription VARCHAR(250), Licence VARCHAR(50), Quote VARCHAR(500), Rating VARCHAR(50), EstimatedViews VARCHAR(50), YouTubeViews VARCHAR(50), Likes VARCHAR(50), Followers VARCHAR(50), VideoContent VARCHAR(50) );");

            using var sqlBulkCopy = new SqlBulkCopy(connection)
            {
                DestinationTableName = "#samples"
            };

            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("FileName", "FileName"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("FileRow", "FileRow"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Year", "Year"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Quarter", "Quarter"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Region", "Region"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Country", "Country"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Requestor", "Requestor"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Site", "Site"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SiteRanking", "SiteRanking"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("OrderType", "OrderType"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Month", "Month"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Category", "Category"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("PartDescription", "PartDescription"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("PartNumber", "PartNumber"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SONumber", "SONumber"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("OrderDate", "OrderDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Specialty", "Specialty"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Purpose", "Purpose"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Action", "Action"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("SampleCost", "SampleCost"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ShipmentDate", "ShipmentDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ShipmentStatus", "ShipmentStatus"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ShipTurnaround", "ShipTurnaround"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Aging", "Aging"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("LaunchDate", "LaunchDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("CorrectedShipLaunchDate", "CorrectedShipLaunchDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("PublishedMonth", "PublishedMonth"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ShippedDate", "ShippedDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("PublishDate", "PublishDate"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Followup", "Followup"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("PublishTurnaround", "PublishTurnaround"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("ReviewUrl", "ReviewUrl"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("AwardUrl", "AwardUrl"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("AwardRank", "AwardRank"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("WinsAnAward", "WinsAnAward"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("AwardDescription", "AwardDescription"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Licence", "Licence"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Quote", "Quote"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Rating", "Rating"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("EstimatedViews", "EstimatedViews"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("YouTubeViews", "YouTubeViews"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Likes", "Likes"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("Followers", "Followers"));
            sqlBulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping("VideoContent", "VideoContent"));
            
            await sqlBulkCopy.WriteToServerAsync(ToDataTable(samples));
        }

        private static DataTable ToDataTable(IEnumerable<Sample> samples)
        {
            var dt = new DataTable();
            dt.Clear();
            
            dt.Columns.Add("FileName");
            dt.Columns.Add("FileRow");
            dt.Columns.Add("Year");
            dt.Columns.Add("Quarter");
            dt.Columns.Add("Region");
            dt.Columns.Add("Country");
            dt.Columns.Add("Requestor");
            dt.Columns.Add("Site");
            dt.Columns.Add("SiteRanking");
            dt.Columns.Add("OrderType");
            dt.Columns.Add("Month");
            dt.Columns.Add("Category");
            dt.Columns.Add("PartDescription");
            dt.Columns.Add("PartNumber");
            dt.Columns.Add("SONumber");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("Specialty");
            dt.Columns.Add("Purpose");
            dt.Columns.Add("Action");
            dt.Columns.Add("SampleCost");
            dt.Columns.Add("ShipmentDate");
            dt.Columns.Add("ShipmentStatus");
            dt.Columns.Add("ShipTurnaround");
            dt.Columns.Add("Aging");
            dt.Columns.Add("LaunchDate");
            dt.Columns.Add("CorrectedShipLaunchDate");
            dt.Columns.Add("PublishedMonth");
            dt.Columns.Add("ShippedDate");
            dt.Columns.Add("PublishDate");
            dt.Columns.Add("Followup");
            dt.Columns.Add("PublishTurnaround");
            dt.Columns.Add("ReviewUrl");
            dt.Columns.Add("AwardUrl");
            dt.Columns.Add("AwardRank");
            dt.Columns.Add("WinsAnAward");
            dt.Columns.Add("AwardDescription");
            dt.Columns.Add("Licence");
            dt.Columns.Add("Quote");
            dt.Columns.Add("Rating");
            dt.Columns.Add("EstimatedViews");
            dt.Columns.Add("YouTubeViews");
            dt.Columns.Add("Likes");
            dt.Columns.Add("Followers");
            dt.Columns.Add("VideoContent");
            
            foreach (var sample in samples)
            {
                var dr = dt.NewRow();
                dr["FileName"] = sample.FileName;
                dr["FileRow"] = sample.FileRow;
                dr["Year"] = sample.Year;
                dr["Quarter"] = sample.Quarter;
                dr["Region"] = sample.Region;
                dr["Country"] = sample.Country;
                dr["Requestor"] = sample.Requestor;
                dr["Site"] = sample.Site;
                dr["SiteRanking"] = sample.SiteRanking;
                dr["OrderType"] = sample.OrderType;
                dr["Month"] = sample.Month;
                dr["Category"] = sample.Category;
                dr["PartDescription"] = sample.PartDescription;
                dr["PartNumber"] = sample.PartNumber;
                dr["SONumber"] = sample.SONumber;
                dr["OrderDate"] = sample.OrderDate;
                dr["Specialty"] = sample.Specialty;
                dr["Purpose"] = sample.Purpose;
                dr["Action"] = sample.Action;
                dr["SampleCost"] = sample.SampleCost;
                dr["ShipmentDate"] = sample.ShipmentDate;
                dr["ShipmentStatus"] = sample.ShipmentStatus;
                dr["ShipTurnaround"] = sample.ShipTurnaround;
                dr["Aging"] = sample.Aging;
                dr["LaunchDate"] = sample.LaunchDate;
                dr["CorrectedShipLaunchDate"] = sample.CorrectedShipLaunchDate;
                dr["PublishedMonth"] = sample.PublishedMonth;
                dr["ShippedDate"] = sample.ShippedDate;
                dr["PublishDate"] = sample.PublishDate;
                dr["Followup"] = sample.Followup;
                dr["PublishTurnaround"] = sample.PublishTurnaround;
                dr["ReviewUrl"] = sample.ReviewUrl;
                dr["AwardUrl"] = sample.AwardUrl;
                dr["AwardRank"] = sample.AwardRank;
                dr["WinsAnAward"] = sample.WinsAnAward;
                dr["AwardDescription"] = sample.AwardDescription;
                dr["Licence"] = sample.Licence;
                dr["Quote"] = sample.Quote;
                dr["Rating"] = sample.Rating;
                dr["EstimatedViews"] = sample.EstimatedViews;
                dr["YouTubeViews"] = sample.YouTubeViews;
                dr["Likes"] = sample.Likes;
                dr["Followers"] = sample.Followers;
                dr["VideoContent"] = sample.VideoContent;
                dt.Rows.Add(dr);
            }

            return dt;
        }
    }
}