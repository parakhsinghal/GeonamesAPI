-- Add RowVersion for optimistic concurrency

Use Geonames;
Go

Alter Table dbo.Admin1Code
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.Admin2Code
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.AlternateName
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.Continent
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.Country
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.FeatureCategory
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.FeatureCode
Add	IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.Hierarchy
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.LanguageCode
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.RawData
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.RawPostal
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go

Alter Table dbo.TimeZone
Add IsActive bit Not Null Default 1,
	RowId RowVersion Not Null;
Go