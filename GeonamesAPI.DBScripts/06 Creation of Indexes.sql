-- Create Indexes

Use Geonames;
GO

Create NonClustered Index NCI_Admin1Code_CoveringIndex on Admin1Code(Admin1CodeName, ASCIIName, GeonameId, IsActive);
Go

Create NonClustered Index NCI_Admin2Code_CoveringIndex on Admin2Code(Admin2CodeName, ASCIIName, GeonameId, IsActive);
Go

Create NonClustered Index NCI_Continent_CoveringIndex on Continent(Continent, GeonameId, IsActive);
Go

Create NonClustered Index NCI_FeatureCode_CoveringIndex on FeatureCode(FeatureCodeName, IsActive);
Go

Create NonClustered Index NCI_FeatureCategory_CoveringIndex on FeatureCategory(FeatureCategoryName, IsActive);
Go

Create NonClustered Index NCI_TimeZone_CoveringIndex on TimeZone(TimeZoneId, GMT, DST, RawOffset, IsActive);
Go

Create NonClustered Index NCI_LanguageCode_CoveringIndex on LanguageCode(ISO6392, ISO6391, Language, IsActive);
Go

Create NonClustered index NCI_RawData_ASCIIName_Name on RawData (ASCIIName, Name, IsActive)
Go 

Create NonClustered Index NCI_RawData_Latitude_Longitude on RawData (Latitude, Longitude, IsActive)
Go

Create NonClustered Index NCI_RawData_ISOCountryCode on RawData (ISOCountryCode, IsActive)
Go

Create NonClustered Index NCI_RawData_FeatureCategoryId_FeatureCode
on RawData (FeatureCategoryId, FeatureCodeId, IsActive)
Go