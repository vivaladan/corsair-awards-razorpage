DROP TABLE Samples;

CREATE TABLE Samples (
    Id int IDENTITY PRIMARY KEY,
    FileName VARCHAR(50) NOT NULL,
    FileRow INT NOT NULL,
    Year VARCHAR(50),
    Quarter VARCHAR(50),
    Region VARCHAR(50),
    Country VARCHAR(50),
    Requestor VARCHAR(50),
    Site VARCHAR(50),
    SiteRanking VARCHAR(50),
    OrderType VARCHAR(50),
    Month VARCHAR(50),
    Category VARCHAR(50),
    PartDescription VARCHAR(100),
    PartNumber VARCHAR(50),
    SONumber VARCHAR(50),
    OrderDate DATETIME2,
    Specialty VARCHAR(100),
    Purpose VARCHAR(50),
    Action VARCHAR(50),
    SampleCost VARCHAR(50),
    ShipmentDate DATETIME2,
    ShipmentStatus VARCHAR(50),
    ShipTurnaround VARCHAR(50),
    Aging VARCHAR(50),
    LaunchDate DATETIME2,
    CorrectedShipLaunchDate DATETIME2,
    PublishedMonth VARCHAR(50),
    ShippedDate DATETIME2,
    PublishDate DATETIME2,
    Followup VARCHAR(50),
    PublishTurnaround VARCHAR(50),
    ReviewUrl VARCHAR(500),
    AwardUrl VARCHAR(500),
    AwardRank VARCHAR(50),
    WinsAnAward VARCHAR(50),
    AwardDescription VARCHAR(250),
    Licence VARCHAR(50),
    Quote VARCHAR(500),
    Rating VARCHAR(50),
    EstimatedViews VARCHAR(50),
    YouTubeViews VARCHAR(50),
    Likes VARCHAR(50),
    Followers VARCHAR(50),
    VideoContent VARCHAR(50)
);

ALTER TABLE Samples ADD CONSTRAINT IX_filename_filerow UNIQUE (FileName, FileRow);

DROP TABLE Categories;

CREATE TABLE Categories (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(50),
)

SELECT * FROM Categories

DROP TABLE PartDescriptions;

CREATE TABLE PartDescriptions (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(100),
)

SELECT * FROM PartDescriptions

DROP TABLE PartNumbers;

CREATE TABLE PartNumbers (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(50),
)

SELECT * FROM PartNumbers

DROP TABLE Regions;

CREATE TABLE Regions (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(50),
)

SELECT * FROM Regions

DROP TABLE Countries;

CREATE TABLE Countries (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(50),
)

SELECT * FROM Countries

DROP TABLE Years;

CREATE TABLE Years (
    Id int IDENTITY PRIMARY KEY,
    Name VARCHAR(50),
)

SELECT * FROM Years