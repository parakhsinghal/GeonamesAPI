-- Changing the recovery mode of database to simple for minimal transactional logging
ALTER DATABASE Geonames
SET RECOVERY SIMPLE
Go

-- Remove duplicate entries from the Hierarchy table
With DuplicateData_Hierarchy_CTE As
(
	Select ParentId, ChildId, Type
	,ROW_NUMBER() Over (Partition by ParentId, ChildId Order By ParentId) as RowNumber
	from Hierarchy
)

Delete from DuplicateData_Hierarchy_CTE where RowNumber > 1;
Go

Delete from Hierarchy
Where ParentId not in (Select GeonameId from RawData)
Go

Delete from Hierarchy
Where ChildId not in (Select GeonameId from RawData)
Go

-- Remove data from AlternateName that is not available in Rawdata table
Delete from dbo.AlternateName where dbo.AlternateName.GeonameId not in 
(Select GeonameId from dbo.RawData);
Go

-- Create data in the dbo.Country table from dbo.Rawdata
-- that does not exist in the dbo.Country table
Insert Into dbo.Country (ISOCountryCode, CountryName, GeonameId)
Select ISOCountryCode, ASCIIName, GeonameId from dbo.RawData
where dbo.RawData.ISOCountryCode not in (Select ISOCountryCode from Country)

-- Remove data from AlternateName that is not available in LanguageCode table

-- Creating RowVersion Columns in all the tables and 
-- populating it with data
-- Note that creation of column is a fully logged operations