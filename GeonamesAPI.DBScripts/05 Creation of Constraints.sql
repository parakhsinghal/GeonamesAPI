-- Create constraints

Use Geonames;
GO

Alter Table dbo.Rawdata
Add Constraint PK_Rawdata_GeonameId Primary Key (GeonameId);
Go

Alter Table dbo.TimeZone
Add Constraint PK_TimeZone_TimeZoneId Primary Key (TimeZoneId);
Go

Alter Table dbo.LanguageCode
Add Constraint PK_LanguageCode_ISO6393 Primary Key (ISO6393);
Go

Alter Table dbo.Country
Add Constraint PK_Country_ISOCountryCode Primary Key (ISOCountryCode);
Go

Alter Table dbo.FeatureCategory
Add Constraint PK_FeatureCategory_FeatureCategoryId Primary Key (FeatureCateGoryId);
Go

Alter Table dbo.Admin2Code
Add Constraint PK_Admin2Code_Admin2CodeId Primary Key (Admin2CodeId);
Go

Alter Table dbo.FeatureCode
Add Constraint PK_FeatureCode_FeatureCodeId Primary Key (FeatureCodeId);
Go

Alter Table dbo.Continent
Add Constraint PK_Continent_ContinentCodeId Primary Key (ContinentCodeId);
Go

Alter Table dbo.Admin1Code
Add Constraint PK_Admin1Code_Admin1CodeId Primary Key (Admin1CodeId);
Go

Alter Table dbo.AlternateName
Add Constraint PK_AlternateName_AlternateNameId Primary Key (AlternateNameId);
Go

Alter Table dbo.Hierarchy
Add Constraint PK_Hierarchy_ParentId_ChildId Primary Key(ParentId, ChildId)
Go

Alter Table dbo.Rawdata
Add Constraint FK_Rawdata_TimeZoneId Foreign Key (TimeZoneId) References dbo.TimeZone(TimeZoneId)
Go

Alter Table dbo.Rawdata
Add Constraint FK_Rawdata_FeatureCategory Foreign Key (FeatureCategoryId) 
References dbo.FeatureCategory(FeatureCategoryId);
Go

Alter Table dbo.Rawdata
Add Constraint FK_Rawdata_Country Foreign Key (ISOCountryCode)
References dbo.Country(ISOCountryCode);
Go

Alter Table dbo.Continent
Add Constraint FK_Continent_Rawdata Foreign Key (GeonameId) References dbo.Rawdata(GeonameId);
Go

Alter Table dbo.Hierarchy
Add Constraint FK_Hierarchy_RawData_ParentId Foreign Key (ParentId) References dbo.RawData (GeonameId);
Go

Alter Table dbo.Hierarchy
Add Constraint FK_Hierarchy_RawData_ChildId Foreign Key (ChildId) References dbo.RawData (GeonameId);
Go

Alter Table dbo.Admin1Code
Add Constraint FK_Admin1Code_Rawdata Foreign Key (GeonameId) References dbo.Rawdata(GeonameId);
Go

Alter Table dbo.Admin2Code
Add Constraint FK_Admin2Code_Rawdata Foreign Key (GeonameId) References dbo.Rawdata(GeonameId);
Go

Alter Table dbo.AlternateName
Add Constraint FK_AlternateName_Rawdata Foreign Key (GeonameId) References dbo.Rawdata(GeonameId);
Go

Alter Table dbo.RawPostal
Add Constraint FK_RawPostal_Country Foreign Key (ISOCountryCode) References dbo.Country(ISOCountryCode);
Go

Alter Table dbo.TimeZone
Add Constraint FK_TimeZone_Country Foreign Key (ISOCountryCode) References dbo.Country(ISOCountryCode);
Go