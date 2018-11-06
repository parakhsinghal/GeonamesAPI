-- Drop the old database if exists and create a new one

USE master;
Go

IF DB_ID('Geonames') IS NOT NULL
DROP DATABASE Geonames;
Go

CREATE DATABASE Geonames;
Go

USE Geonames;
Go

Alter Database Geonames
Set Recovery Bulk_Logged;

-- Create Tables

CREATE TABLE dbo.Country
(	
    ISOCountryCode		char(2)			NOT NULL,
    ISO3Code			char(3)			NULL,
    ISONumeric			int				NULL,
    FIPSCode			char(2)			NULL,
    CountryName			nvarchar(200)	NULL,
    Capital				nvarchar(200)	NULL,
    SqKmArea			float			NULL,
    TotalPopulation		bigint			NULL,
    ContinentCodeId		char(2)			NULL,
    TopLevelDomain		nvarchar(10)	NULL,
    CurrencyCode		char(4)			NULL,
    CurrencyName		nvarchar(128)	NULL,
    Phone				nvarchar(32)	NULL,
    PostalFormat		nvarchar(64)	NULL,
    PostalRegex			nvarchar(256)	NULL,
    Languages			nvarchar(256)	NULL,
    GeonameId			int				NULL,
    Neighbors			nvarchar(256)	NULL,
    EquivalentFipsCode	nvarchar(64)	NULL
);
Go

CREATE TABLE dbo.Admin1Code
(
    Admin1CodeId	nvarchar(32)	NOT NULL,
    Admin1CodeName	nvarchar(128)	NULL,
    ASCIIName		nvarchar(128)	NULL,
    GeonameId		int				NULL
);
Go

CREATE TABLE dbo.Admin2Code
(
    Admin2CodeId	nvarchar(32)	NOT NULL,
    Admin2CodeName	nvarchar(128)	NULL,
    ASCIIName		nvarchar(128)	NULL,
    GeonameId		int				NULL
);
Go

CREATE TABLE dbo.AlternateName
(
    AlternateNameId			int				NOT NULL	IDENTITY(1,1),
    GeonameId				int				NULL,
    ISO6393LanguageCode		nvarchar(24)	NULL,
    AlternateName			nvarchar(512)	NULL,
    IsPreferredName			bit				NULL,
    IsShortName				bit				NULL,
    IsColloquial			bit				NULL,
    IsHistoric				bit				NULL
);
Go

CREATE TABLE dbo.Continent
(
    ContinentCodeId		char(2)			NOT NULL,
    GeonameId			int				NULL,
    Continent			nvarchar(32)	NULL
);
Go

CREATE TABLE dbo.FeatureCategory
(
    FeatureCategoryId		char(1)			NOT NULL,
    FeatureCategoryName		nvarchar(128)	NULL
);
Go

CREATE TABLE dbo.FeatureCode
(
    FeatureCodeId		nvarchar(16)	NOT NULL,
    FeatureCodeName		nvarchar(128)	NULL,
    Description			nvarchar(512)	NULL
);
Go

CREATE TABLE dbo.LanguageCode
(
    ISO6393		nvarchar(24)	NOT NULL,
    ISO6392		nvarchar(24)	NULL,
    ISO6391		nvarchar(24)	NULL,
    Language	nvarchar(128)	NULL
);
Go

CREATE TABLE dbo.RawData
(
    GeonameId			int				NOT NULL IDENTITY(1,1),
    Name				nvarchar(200)	NULL,
    ASCIIName			nvarchar(200)	NULL,
    AlternateNames		text			NULL,
    Latitude			float			NULL,
    Longitude			float			NULL,
    FeatureCategoryId	char(1)			NULL,
    FeatureCodeId		nvarchar(16)	NULL,
    ISOCountryCode		char(2)			NULL,
    CC2					nvarchar(200)	NULL,
    Admin1Code			nvarchar(20)	NULL,
    Admin2Code			nvarchar(80)	NULL,
    Admin3Code			nvarchar(20)	NULL,
    Admin4Code			nvarchar(20)	NULL,
    Population			bigint			NULL,
    Elevation			int				NULL,
    DEM					int				NULL,
    TimeZoneId			nvarchar(128)	NULL,
    ModificationDate	datetime		NULL
);
Go

CREATE TABLE dbo.RawPostal
(
    ISOCountryCode	char(2)			NOT NULL,
    PostalCode		nvarchar(20)	NOT NULL,
    PlaceName		nvarchar(180)	NOT NULL,
    Admin1Name		nvarchar(100)	NULL,
    Admin1Code		nvarchar(20)	NULL,
    Admin2Name		nvarchar(100)	NULL,
    Admin2Code		nvarchar(20)	NULL,
    Admin3Name		nvarchar(100)	NULL,
    Admin3Code		nvarchar(20)	NULL,
    Latitude		float			NULL,
    Longitude		float			NULL,
    Accuracy		int				NULL
);
Go

CREATE TABLE dbo.TimeZone
(
    ISOCountryCode	char(2)			NULL,
    TimeZoneId		nvarchar(128)	NOT NULL,
    GMT				decimal(18, 3)	NULL,
    DST				decimal(18, 3)	NULL,
    RawOffset		decimal(18, 3)	NULL
);
Go

CREATE TABLE dbo.Hierarchy
(
	ParentId	int			NOT NULL,
	ChildId		int			NOT NULL,
	Type		nvarchar(50)	NULL
)
Go