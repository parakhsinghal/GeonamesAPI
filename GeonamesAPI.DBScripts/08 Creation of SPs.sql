Use Geonames
Go

-- Exec dbo.TimeZone_GetDistinctTimeZones

Create Procedure dbo.TimeZone_GetDistinctTimeZones 
	
 @PageNumber	int		= NULL
,@PageSize		int		= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select Distinct dbo.TimeZone.TimeZoneId, dbo.TimeZone.RowId From dbo.TimeZone
		Where dbo.TimeZone.IsActive = 1
		Order By dbo.TimeZone.TimeZoneId
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select Distinct dbo.TimeZone.TimeZoneId, dbo.TimeZone.RowId From dbo.TimeZone
		Where dbo.TimeZone.IsActive = 1
		Order By dbo.TimeZone.TimeZoneId;
	End
End
Go

-- Exec dbo.TimeZone_GetTimeZoneDetails

Create Procedure dbo.TimeZone_GetTimeZoneDetails 

 @TimeZoneId		nvarchar(128)	= NULL
,@ISOCountryCode	char(10)		= NULL
,@ISO3Code			char(3)			= NULL
,@ISONumeric		int				= NULL
,@CountryName		nvarchar(200)	= NULL
,@Latitude			float			= NULL
,@Longitude			float			= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

As

Begin

	Set NoCount On;	

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select
		dbo.TimeZone.ISOCountryCode		
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		From dbo.TimeZone
		inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
		inner join dbo.RawData on dbo.RawData.GeonameId = dbo.Country.GeonameId
		Where
		dbo.TimeZone.TimeZoneId = Coalesce(LTrim(RTrim(@TimeZoneId)),dbo.TimeZone.TimeZoneId)
		And dbo.TimeZone.ISOCountryCode = Coalesce(LTrim(RTrim(@ISOCountryCode)), TimeZone.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(LTrim(RTrim(@CountryName)),dbo.Country.CountryName)
		And dbo.Country.ISO3Code = Coalesce(LTrim(RTrim(@ISO3Code)),dbo.Country.ISO3Code)
		And dbo.Country.ISONumeric = Coalesce(LTrim(RTrim(@ISONumeric)),dbo.Country.ISONumeric)
		And dbo.RawData.Latitude = Coalesce(@Latitude,dbo.Rawdata.Latitude)
		And dbo.RawData.Longitude = Coalesce(@Longitude,dbo.Rawdata.Longitude)
		And dbo.TimeZone.IsActive = 1
		And dbo.Country.IsActive = 1
		And dbo.RawData.IsActive = 1
		Group By
		dbo.TimeZone.ISOCountryCode
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		Order By dbo.TimeZone.ISOCountryCode
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select
		dbo.TimeZone.ISOCountryCode		
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		From dbo.TimeZone
		inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
		inner join dbo.RawData on dbo.RawData.GeonameId = dbo.Country.GeonameId
		Where
		dbo.TimeZone.TimeZoneId = Coalesce(LTrim(RTrim(@TimeZoneId)),dbo.TimeZone.TimeZoneId)
		And dbo.TimeZone.ISOCountryCode = Coalesce(LTrim(RTrim(@ISOCountryCode)), TimeZone.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(LTrim(RTrim(@CountryName)),dbo.Country.CountryName)
		And dbo.Country.ISO3Code = Coalesce(LTrim(RTrim(@ISO3Code)),dbo.Country.ISO3Code)
		And dbo.Country.ISONumeric = Coalesce(LTrim(RTrim(@ISONumeric)),dbo.Country.ISONumeric)
		And dbo.RawData.Latitude = Coalesce(@Latitude,dbo.Rawdata.Latitude)
		And dbo.RawData.Longitude = Coalesce(@Longitude,dbo.Rawdata.Longitude)
		And dbo.TimeZone.IsActive = 1
		And dbo.Country.IsActive = 1
		And dbo.RawData.IsActive = 1
		Group By
		dbo.TimeZone.ISOCountryCode
		,dbo.TimeZone.TimeZoneId
		,dbo.TimeZone.DST
		,dbo.TimeZone.GMT
		,dbo.TimeZone.RawOffset
		,dbo.TimeZone.RowId
		Order By dbo.TimeZone.ISOCountryCode
	End

End
Go

-- Exec dbo.TimeZone_GetTimeZoneDetailsByPlaceName @PlaceName = 'Ajmer'

Create Procedure  dbo.TimeZone_GetTimeZoneDetailsByPlaceName

@PlaceName nvarchar(200) = NULL

As
Begin

	Set NoCount On;	

	Select
	dbo.TimeZone.ISOCountryCode
	,dbo.TimeZone.TimeZoneId
	,dbo.TimeZone.DST
	,dbo.TimeZone.GMT
	,dbo.TimeZone.RawOffset
	,dbo.TimeZone.RowId
	From RawData
	inner join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawData.ISOCountryCode
	inner join dbo.TimeZone on dbo.Country.ISOCountryCode = dbo.TimeZone.ISOCountryCode
	Where dbo.RawData.ASCIIName = LTrim(RTrim(@PlaceName))
	And dbo.TimeZone.IsActive = 1
	And dbo.Country.IsActive = 1
	And dbo.RawData.IsActive = 1
	Group By
	dbo.TimeZone.ISOCountryCode
	,dbo.TimeZone.TimeZoneId
	,dbo.TimeZone.DST
	,dbo.TimeZone.GMT
	,dbo.TimeZone.RawOffset
	,dbo.TimeZone.RowId
	Order By dbo.TimeZone.ISOCountryCode

End
Go

-- Exec dbo.Country_GetCountryInfo @ISOCountryCode = NULL, @CountryName  = NULL, @PageNumber = NULL, @PageSize = NULL

Create Procedure dbo.Country_GetCountryInfo  

 @ISOCountryCode	char(2)			= NULL
,@CountryName		nvarchar(200)	= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From dbo.Country
		Where
		dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		And dbo.Country.IsActive = 1
		Order by dbo.Country.CountryName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From dbo.Country
		Where
		dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		And dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		And dbo.Country.IsActive = 1
		Order by dbo.Country.CountryName;
	End
End
Go

-- Exec dbo.RawData_GetStateInfo  @CountryName = NULL,@ISOCountryCode = 'IN',@StateGeonameId = NULL,@StateName = NULL, @FeatureCategoryId = 'A' ,@FeatureCode = 'ADM1',@PageNumber = 2,@PageSize = 20

Create Procedure dbo.RawData_GetStateInfo  

 @CountryName		nvarchar(200)	= NULL
,@ISOCountryCode	char(2)			= NULL
,@StateGeonameId	bigint			= NULL
,@StateName			nvarchar(200)	= NULL
,@FeatureCategoryId char(1)			= 'A'
,@FeatureCodeId		nvarchar(10)	= 'ADM1'
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin		
		SELECT
		   dbo.RawData.GeonameId
		  ,dbo.RawData.Name
		  ,dbo.RawData.ASCIIName
		  ,dbo.RawData.AlternateNames
		  ,dbo.RawData.Latitude
		  ,dbo.RawData.Longitude
		  ,dbo.RawData.FeatureCategoryId
		  ,dbo.RawData.FeatureCodeId
		  ,dbo.RawData.ISOCountryCode
		  ,dbo.RawData.CC2
		  ,dbo.RawData.Admin1Code
		  ,dbo.RawData.Admin2Code
		  ,dbo.RawData.Admin3Code
		  ,dbo.RawData.Admin4Code
		  ,dbo.RawData.Population
		  ,dbo.RawData.Elevation
		  ,dbo.RawData.DEM
		  ,dbo.RawData.TimeZoneId
		  ,dbo.RawData.ModificationDate
		  ,dbo.RawData.RowId
		From dbo.RawData
		inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
		inner join dbo.Country on dbo.Country.GeonameId = dbo.Hierarchy.ParentId
		where 
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		and dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and dbo.RawData.GeonameId = Coalesce(@StateGeonameId,dbo.RawData.GeonameId)
		and dbo.RawData.ASCIIName = Coalesce(@StateName,dbo.RawData.ASCIIName)
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId
		and dbo.RawData.FeatureCodeId = @FeatureCodeId
		and dbo.RawData.IsActive = 1
		and dbo.Hierarchy.IsActive = 1
		and dbo.Country.IsActive = 1
		Order by dbo.RawData.ASCIIName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		   dbo.RawData.GeonameId
		  ,dbo.RawData.Name
		  ,dbo.RawData.ASCIIName
		  ,dbo.RawData.AlternateNames
		  ,dbo.RawData.Latitude
		  ,dbo.RawData.Longitude
		  ,dbo.RawData.FeatureCategoryId
		  ,dbo.RawData.FeatureCodeId
		  ,dbo.RawData.ISOCountryCode
		  ,dbo.RawData.CC2
		  ,dbo.RawData.Admin1Code
		  ,dbo.RawData.Admin2Code
		  ,dbo.RawData.Admin3Code
		  ,dbo.RawData.Admin4Code
		  ,dbo.RawData.Population
		  ,dbo.RawData.Elevation
		  ,dbo.RawData.DEM
		  ,dbo.RawData.TimeZoneId
		  ,dbo.RawData.ModificationDate
		  ,dbo.RawData.RowId
		From dbo.RawData
		inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
		inner join dbo.Country on dbo.Country.GeonameId = dbo.Hierarchy.ParentId
		where 
		dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		and dbo.Country.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.Country.ISOCountryCode)
		and dbo.RawData.GeonameId = Coalesce(@StateGeonameId,dbo.RawData.GeonameId)
		and dbo.RawData.ASCIIName = Coalesce(@StateName,dbo.RawData.ASCIIName)
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId
		and dbo.RawData.FeatureCodeId = @FeatureCodeId
		and dbo.RawData.IsActive = 1
		and dbo.Hierarchy.IsActive = 1
		and dbo.Country.IsActive = 1
		Order by dbo.RawData.ASCIIName;
	End
End
Go


Create Procedure dbo.RawData_GetCitiesInAState	

	@CountryName		nvarchar(100)	= NULL -- 'United States'
	,@ISOCountryCode	char(2)			= NULL
	,@StateName			nvarchar(100)	= NULL
	,@StateGeonameId	bigint			= NULL
	,@FeatureCategoryId char(1)			= 'A' -- Country, State, Region, etc.
	,@CityGeonameId		bigint			= NULL 
	,@CityName			nvarchar(100)	= NULL
	,@PageNumber		int				= NULL
	,@PageSize			int				= NULL
AS
BEGIN

	If(@ISOCountryCode is NULL)
    Begin
        Select @ISOCountryCode = dbo.Country.ISOCountryCode from dbo.Country Where dbo.Country.CountryName = @CountryName;
    End

	If(@StateGeonameId is NULL)
	Begin
		Select @StateGeonameId = dbo.RawData.GeonameId from dbo.RawData
		Where dbo.RawData.ASCIIName = @StateName
		and dbo.RawData.ISOCountryCode = @ISOCountryCode
		and dbo.RawData.FeatureCategoryId = @FeatureCategoryId;
	End

	If(@PageSize is not NULL and @PageNumber is not NULL)
    Begin
        SELECT
        dbo.RawData.GeonameId
        ,dbo.RawData.Name
        ,dbo.RawData.ASCIIName
        ,dbo.RawData.AlternateNames
        ,dbo.RawData.Latitude
        ,dbo.RawData.Longitude
        ,dbo.RawData.FeatureCategoryId
        ,dbo.RawData.FeatureCodeId
        ,dbo.RawData.ISOCountryCode
        ,dbo.RawData.CC2
        ,dbo.RawData.Admin1Code
        ,dbo.RawData.Admin2Code
        ,dbo.RawData.Admin3Code
        ,dbo.RawData.Admin4Code
        ,dbo.RawData.Population
        ,dbo.RawData.Elevation
        ,dbo.RawData.DEM
        ,dbo.RawData.TimeZoneId
        ,dbo.RawData.ModificationDate
        ,dbo.RawData.RowId
        FROM dbo.RawData 
        inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
        where
        dbo.Hierarchy.ParentId = @StateGeonameId
        and dbo.RawData.GeonameId = Coalesce(@CityGeonameId, dbo.RawData.GeonameId)
        and dbo.RawData.ASCIIName = Coalesce(@CityName, dbo.RawData.ASCIIName)
		and dbo.RawData.IsActive = 1
		and dbo.Hierarchy.IsActive = 1
        Order by dbo.RawData.ASCIIName Asc
        Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
    End

    Else
    Begin
        SELECT
        dbo.RawData.GeonameId
        ,dbo.RawData.Name
        ,dbo.RawData.ASCIIName
        ,dbo.RawData.AlternateNames
        ,dbo.RawData.Latitude
        ,dbo.RawData.Longitude
        ,dbo.RawData.FeatureCategoryId
        ,dbo.RawData.FeatureCodeId
        ,dbo.RawData.ISOCountryCode
        ,dbo.RawData.CC2
        ,dbo.RawData.Admin1Code
        ,dbo.RawData.Admin2Code
        ,dbo.RawData.Admin3Code
        ,dbo.RawData.Admin4Code
        ,dbo.RawData.Population
        ,dbo.RawData.Elevation
        ,dbo.RawData.DEM
        ,dbo.RawData.TimeZoneId
        ,dbo.RawData.ModificationDate
        ,dbo.RawData.RowId
        FROM dbo.RawData 
        inner join dbo.Hierarchy on dbo.Hierarchy.ChildId = dbo.RawData.GeonameId
        where
        dbo.Hierarchy.ParentId = @StateGeonameId
        --and dbo.RawData.FeatureCategoryId = Coalesce(dbo.Rawdata.FeatureCategoryId ,@FeatureCategoryId)
        --and dbo.RawData.FeatureCode = Coalesce(dbo.RawData.FeatureCode,@FeatureCode)
        and dbo.RawData.GeonameId = Coalesce(@CityGeonameId, dbo.RawData.GeonameId)
        and dbo.RawData.ASCIIName = Coalesce(@CityName, dbo.RawData.ASCIIName)
		and dbo.RawData.IsActive = 1
		and dbo.Hierarchy.IsActive = 1
        Order by dbo.RawData.ASCIIName Asc;
    End
END
Go

-- Exec dbo.Continent_GetContinentInfo @ContinentCodeId=NULL,@GeonameId=NULL,@Continent=NULL

Create Procedure dbo.Continent_GetContinentInfo  

 @ContinentCodeId	char(2)			= NULL
,@GeonameId			bigint			= NULL
,@Continent			nvarchar(32)	= NULL

As
Begin

	Set NoCount On;

	Select 
	   dbo.Continent.ContinentCodeId      
      ,dbo.Continent.Continent
	  ,dbo.Continent.GeonameId
	  ,dbo.RawData.ASCIIName
	  ,dbo.RawData.AlternateNames
	  ,dbo.RawData.Latitude
	  ,dbo.RawData.Longitude
	  ,dbo.RawData.FeatureCategoryId
	  ,dbo.RawData.FeatureCodeId
	  ,dbo.RawData.TimeZoneId
	  ,dbo.RawData.RowId
	From dbo.Continent
	Inner Join dbo.RawData
	on dbo.RawData.GeonameId = dbo.Continent.GeonameId
	Where
	dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
	and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
	and dbo.Continent.Continent = Coalesce(@Continent,dbo.Continent.Continent)
	and dbo.Continent.IsActive = 1
	and dbo.RawData.IsActive = 1
End
Go

-- Exec dbo.LanguageCode_GetLanguageInfo @ISO6393Code=NULL,@Language=NULL,@PageNumber=2,@PageSize=20

Create Procedure dbo.LanguageCode_GetLanguageInfo  

 @ISO6393Code	nvarchar(24)	= NULL
,@Language		nvarchar(128)	= NULL
,@PageNumber	int				= NULL
,@PageSize		int				= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT 
		dbo.LanguageCode.ISO6393
		,dbo.LanguageCode.ISO6392
		,dbo.LanguageCode.ISO6391
		,dbo.LanguageCode.Language
		,dbo.LanguageCode.RowId
		FROM dbo.LanguageCode
		Where 
		dbo.LanguageCode.ISO6393 = Coalesce(@ISO6393Code,dbo.LanguageCode.ISO6393)
		and dbo.LanguageCode.Language = Coalesce(@Language,dbo.LanguageCode.Language)
		and dbo.LanguageCode.IsActive = 1
		Order by dbo.LanguageCode.Language
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End
	
	Else
	Begin
		SELECT 
		dbo.LanguageCode.ISO6393
		,dbo.LanguageCode.ISO6392
		,dbo.LanguageCode.ISO6391
		,dbo.LanguageCode.Language
		,dbo.LanguageCode.RowId
		FROM dbo.LanguageCode
		Where 
		dbo.LanguageCode.ISO6393 = Coalesce(@ISO6393Code,dbo.LanguageCode.ISO6393)
		and dbo.LanguageCode.Language = Coalesce(@Language,dbo.LanguageCode.Language)
		and dbo.LanguageCode.IsActive = 1
		Order by dbo.LanguageCode.Language
	End
End
Go


Create Procedure dbo.FeatureCategory_GetFeatureCategoryInfo  

@FeatureCategoryId char(1) = NULL

As
Begin

	Set NoCount On;

	Select 
	dbo.FeatureCategory.FeatureCategoryId
    ,dbo.FeatureCategory.FeatureCategoryName
	,dbo.FeatureCategory.RowId
	From dbo.FeatureCategory
	Where 
	dbo.FeatureCategory.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.FeatureCategory.FeatureCategoryId)
	and dbo.FeatureCategory.IsActive = 1;

End
Go

-- Exec dbo.FeatureCode_GetFeatureCodeInfo  @FeatureCodeId = NULL,@PageNumber = 2,@PageSize = 20

Create Procedure dbo.FeatureCode_GetFeatureCodeInfo  

 @FeatureCodeId		nvarchar(16)	= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		Select 
		 dbo.FeatureCode.FeatureCodeId
		,dbo.FeatureCode.FeatureCodeName
		,dbo.FeatureCode.Description
		,dbo.FeatureCode.RowId
		From dbo.FeatureCode
		Where dbo.FeatureCode.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.FeatureCode.FeatureCodeId)
		and dbo.FeatureCode.IsActive = 1
		Order By dbo.FeatureCode.FeatureCodeId
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		Select 
		dbo.FeatureCode.FeatureCodeId
		,dbo.FeatureCode.FeatureCodeName
		,dbo.FeatureCode.Description
		,dbo.FeatureCode.RowId
		From dbo.FeatureCode
		Where dbo.FeatureCode.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.FeatureCode.FeatureCodeId)
		and dbo.FeatureCode.IsActive = 1
		Order By dbo.FeatureCode.FeatureCodeId
	End
End
Go

Create Procedure dbo.Country_GetCountriesInAContinent

 @ContinentName		nvarchar(100)	= NULL
,@ContinentCodeId	nvarchar(100)	= NULL
,@GeonameId			bigint			= NULL
,@PageNumber		int				= NULL
,@PageSize			int				= NULL

As
Begin

	Set NoCount On;

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From dbo.Country
		Inner Join dbo.Continent on dbo.Continent.ContinentCodeId = dbo.Country.ContinentCodeId
		Where
		dbo.Continent.Continent = Coalesce(@ContinentName,dbo.Continent.Continent)
		and dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
		and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
		and dbo.Continent.IsActive = 1
		and dbo.Continent.IsActive = 1
		Order by dbo.Country.CountryName
		Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;
	End

	Else
	Begin
		SELECT
		dbo.Country.ISOCountryCode
		,dbo.Country.ISO3Code
		,dbo.Country.ISONumeric
		,dbo.Country.FIPSCode
		,dbo.Country.CountryName
		,dbo.Country.Capital
		,dbo.Country.SqKmArea
		,dbo.Country.TotalPopulation
		,dbo.Country.ContinentCodeId
		,dbo.Country.TopLevelDomain
		,dbo.Country.CurrencyCode
		,dbo.Country.CurrencyName
		,dbo.Country.Phone
		,dbo.Country.PostalFormat
		,dbo.Country.PostalRegex
		,dbo.Country.Languages
		,dbo.Country.GeonameId
		,dbo.Country.Neighbors
		,dbo.Country.EquivalentFipsCode
		,dbo.Country.RowId
		From dbo.Country
		Inner Join dbo.Continent on dbo.Continent.ContinentCodeId = dbo.Country.ContinentCodeId
		Where
		dbo.Continent.Continent = Coalesce(@ContinentName,dbo.Continent.Continent)
		and dbo.Continent.ContinentCodeId = Coalesce(@ContinentCodeId,dbo.Continent.ContinentCodeId)
		and dbo.Continent.GeonameId = Coalesce(@GeonameId,dbo.Continent.GeonameId)
		and dbo.Continent.IsActive = 1
		and dbo.Continent.IsActive = 1
		Order by dbo.Country.CountryName
	End
End
Go

Create Procedure dbo.RawData_GetCountryFeatureCategoryFeatureCode
@ISOCountryCode			char(2)			= NULL
,@CountryName			nvarchar(200)	= NULL
,@FeatureCategoryId		char(1)			= NULL
,@FeatureCodeId			nvarchar(10)	= NULL
,@PageNumber			int				= NULL
,@PageSize				int				= NULL
AS
BEGIN
	If @ISOCountryCode is NULL and @CountryName is not NULL
		Begin

			Select @ISOCountryCode = dbo.Country.ISOCountryCode from dbo.Country
			Where dbo.Country.CountryName = @CountryName;

		End

	IF @PageNumber IS NOT NULL AND @PageSize IS NOT NULL
		Begin
			Select 
			dbo.RawData.GeonameId
			,dbo.RawData.Name
			,dbo.RawData.ASCIIName
			,dbo.RawData.AlternateNames
			,dbo.RawData.Latitude
			,dbo.RawData.Longitude
			,dbo.RawData.FeatureCategoryId
			,dbo.RawData.FeatureCodeId
			,dbo.RawData.ISOCountryCode
			,dbo.RawData.CC2
			,dbo.RawData.Admin1Code
			,dbo.RawData.Admin2Code
			,dbo.RawData.Admin3Code
			,dbo.RawData.Admin4Code
			,dbo.RawData.Population
			,dbo.RawData.Elevation
			,dbo.RawData.DEM
			,dbo.RawData.TimeZoneId
			,dbo.RawData.ModificationDate
			,dbo.RawData.RowId
			from dbo.RawData
			inner join dbo.Country on dbo.RawData.ISOCountryCode = dbo.Country.ISOCountryCode
			Where
			dbo.RawData.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawData.ISOCountryCode)
			and 
			dbo.RawData.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.RawData.FeatureCategoryId)
			and 
			dbo.RawData.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.RawData.FeatureCodeId)
			and dbo.RawData.IsActive = 1
			and dbo.Country.IsActive = 1
			Order by dbo.RawData.ASCIIName
			Offset (@PageNumber-1)*@PageSize Rows Fetch Next @PageSize Rows Only;

		End
	Else
		Begin
			Select 
			dbo.RawData.GeonameId
			,dbo.RawData.Name
			,dbo.RawData.ASCIIName
			,dbo.RawData.AlternateNames
			,dbo.RawData.Latitude
			,dbo.RawData.Longitude
			,dbo.RawData.FeatureCategoryId
			,dbo.RawData.FeatureCodeId
			,dbo.RawData.ISOCountryCode
			,dbo.RawData.CC2
			,dbo.RawData.Admin1Code
			,dbo.RawData.Admin2Code
			,dbo.RawData.Admin3Code
			,dbo.RawData.Admin4Code
			,dbo.RawData.Population
			,dbo.RawData.Elevation
			,dbo.RawData.DEM
			,dbo.RawData.TimeZoneId
			,dbo.RawData.ModificationDate
			,dbo.RawData.RowId
			from dbo.RawData
			inner join dbo.Country on dbo.RawData.ISOCountryCode = dbo.Country.ISOCountryCode
			Where
			dbo.RawData.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawData.ISOCountryCode)
			and 
			dbo.RawData.FeatureCategoryId = Coalesce(@FeatureCategoryId,dbo.RawData.FeatureCategoryId)
			and 
			dbo.RawData.FeatureCodeId = Coalesce(@FeatureCodeId,dbo.RawData.FeatureCodeId)
			and dbo.RawData.IsActive = 1
			and dbo.Country.IsActive = 1
			Order by dbo.RawData.ASCIIName;
		End
END
Go

Create Procedure dbo.RawPostal_GetPostalCodeInfo

	@ISOCountryCode		char(2)			= NULL
	,@CountryName		nvarchar(100)	= NULL
	,@PostalCode		nvarchar(20)	= NULL
	,@PageSize			int				= NULL
	,@PageNumber		int				= NULL
AS
BEGIN

	SET NOCOUNT ON;

    IF @PageSize is not NULL and @PageNumber is not NULL
		Begin
		  SELECT dbo.RawPostal.ISOCountryCode
		  ,dbo.RawPostal.PostalCode
		  ,dbo.RawPostal.PlaceName
		  ,dbo.RawPostal.Admin1Name
		  ,dbo.RawPostal.Admin1Code
		  ,dbo.RawPostal.Admin2Name
		  ,dbo.RawPostal.Admin2Code
		  ,dbo.RawPostal.Admin3Name
		  ,dbo.RawPostal.Admin3Code
		  ,dbo.RawPostal.Latitude
		  ,dbo.RawPostal.Longitude
		  ,dbo.RawPostal.Accuracy
		  ,dbo.RawPostal.RowId
		  FROM dbo.RawPostal
		  Inner Join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawPostal.ISOCountryCode
		  Where dbo.RawPostal.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawPostal.ISOCountryCode)
		  and dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
		  and dbo.RawPostal.PostalCode = Coalesce(@PostalCode,dbo.RawPostal.PostalCode)		  
		  and dbo.RawPostal.IsActive = 1
		  and dbo.Country.IsActive = 1
		  Order by dbo.RawPostal.PostalCode
		  Offset @PageSize*(@PageNumber-1) Rows Fetch Next @PageSize Rows Only;
		End
		Else
		Begin
			SELECT dbo.RawPostal.ISOCountryCode
			  ,dbo.RawPostal.PostalCode
			  ,dbo.RawPostal.PlaceName
			  ,dbo.RawPostal.Admin1Name
			  ,dbo.RawPostal.Admin1Code
			  ,dbo.RawPostal.Admin2Name
			  ,dbo.RawPostal.Admin2Code
			  ,dbo.RawPostal.Admin3Name
			  ,dbo.RawPostal.Admin3Code
			  ,dbo.RawPostal.Latitude
			  ,dbo.RawPostal.Longitude
			  ,dbo.RawPostal.Accuracy
			  ,dbo.RawPostal.RowId
			  FROM dbo.RawPostal
			  Inner Join dbo.Country on dbo.Country.ISOCountryCode = dbo.RawPostal.ISOCountryCode
			  Where dbo.RawPostal.ISOCountryCode = Coalesce(@ISOCountryCode,dbo.RawPostal.ISOCountryCode)
			  and dbo.Country.CountryName = Coalesce(@CountryName,dbo.Country.CountryName)
			  and dbo.RawPostal.PostalCode = Coalesce(@PostalCode,dbo.RawPostal.PostalCode)
			  and dbo.RawPostal.IsActive = 1
			  and dbo.Country.IsActive = 1
			  Order by dbo.RawPostal.PostalCode;
		End
END
Go

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure inserts values in dbo.Admin1Code table
=============================================
*/
CREATE PROCEDURE dbo.Admin1Code_Insert

	@Input dbo.Admin1Code_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF EXISTS ( SELECT
						Admin1CodeId,
						Admin1CodeName,
						ASCIIName,
						GeonameId,
						RowId
						FROM @Input)
				
					BEGIN
						INSERT INTO dbo.Admin1Code 
						(
							Admin1CodeId,
							Admin1CodeName,
							ASCIIName,
							GeonameId,
							RowId
						) 
						OUTPUT INSERTED.*					
						SELECT
							Admin1CodeId,
							Admin1CodeName,
							ASCIIName,
							GeonameId,
							NULL
						FROM @Input
					END

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.Admin2Code table.
-- =============================================
*/
CREATE PROCEDURE dbo.Admin2Code_Insert
	
	@Input dbo.Admin2Code_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   Admin2CodeId
			  ,Admin2CodeName
			  ,ASCIIName
			  ,GeonameId
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.Admin2Code
			(
				 Admin2CodeId
				,Admin2CodeName
				,ASCIIName
				,GeonameId
				,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   Admin2CodeId
			  ,Admin2CodeName
			  ,ASCIIName
			  ,GeonameId
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.AlternateName table.
-- =============================================
*/
CREATE PROCEDURE dbo.AlternateName_Insert
	
	@Input dbo.AlternateName_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   AlternateNameId
			  ,GeonameId
			  ,ISO6393LanguageCode
			  ,AlternateName
			  ,IsPreferredName
			  ,IsShortName
			  ,IsColloquial
			  ,IsHistoric
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.AlternateName
			(
			   GeonameId
			  ,ISO6393LanguageCode
			  ,AlternateName
			  ,IsPreferredName
			  ,IsShortName
			  ,IsColloquial
			  ,IsHistoric
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   GeonameId
			  ,ISO6393LanguageCode
			  ,AlternateName
			  ,IsPreferredName
			  ,IsShortName
			  ,IsColloquial
			  ,IsHistoric
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.Continent table.
-- =============================================
*/
CREATE PROCEDURE dbo.Continent_Insert
	
	@Input dbo.Continent_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   ContinentCodeId
			  ,GeonameId
			  ,Continent
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.Continent
			(
			   ContinentCodeId
			  ,GeonameId
			  ,Continent
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   ContinentCodeId
			  ,GeonameId
			  ,Continent
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.Country table.
-- =============================================
*/
CREATE PROCEDURE dbo.Country_Insert
	
	@Input dbo.Country_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   ISOCountryCode
			  ,ISO3Code
			  ,ISONumeric
			  ,FIPSCode
			  ,CountryName
			  ,Capital
			  ,SqKmArea
			  ,TotalPopulation
			  ,ContinentCodeId
			  ,TopLevelDomain
			  ,CurrencyCode
			  ,CurrencyName
			  ,Phone
			  ,PostalFormat
			  ,PostalRegex
			  ,Languages
			  ,GeonameId
			  ,Neighbors
			  ,EquivalentFipsCode
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.Country
			(
			   ISOCountryCode
			  ,ISO3Code
			  ,ISONumeric
			  ,FIPSCode
			  ,CountryName
			  ,Capital
			  ,SqKmArea
			  ,TotalPopulation
			  ,ContinentCodeId
			  ,TopLevelDomain
			  ,CurrencyCode
			  ,CurrencyName
			  ,Phone
			  ,PostalFormat
			  ,PostalRegex
			  ,Languages
			  ,GeonameId
			  ,Neighbors
			  ,EquivalentFipsCode
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   ISOCountryCode
			  ,ISO3Code
			  ,ISONumeric
			  ,FIPSCode
			  ,CountryName
			  ,Capital
			  ,SqKmArea
			  ,TotalPopulation
			  ,ContinentCodeId
			  ,TopLevelDomain
			  ,CurrencyCode
			  ,CurrencyName
			  ,Phone
			  ,PostalFormat
			  ,PostalRegex
			  ,Languages
			  ,GeonameId
			  ,Neighbors
			  ,EquivalentFipsCode
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.FeatureCategory table.
-- =============================================
*/
CREATE PROCEDURE dbo.FeatureCategory_Insert
	
	@Input dbo.FeatureCategory_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   FeatureCategoryId
			  ,FeatureCategoryName
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.FeatureCategory
			(
			   FeatureCategoryId
			  ,FeatureCategoryName
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   FeatureCategoryId
			  ,FeatureCategoryName
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.FeatureCode table.
-- =============================================
*/
CREATE PROCEDURE dbo.FeatureCode_Insert
	
	@Input dbo.FeatureCode_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   FeatureCodeId
			  ,FeatureCodeName
			  ,Description
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.FeatureCode
			(
			   FeatureCodeId
			  ,FeatureCodeName
			  ,Description
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   FeatureCodeId
			  ,FeatureCodeName
			  ,Description
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.Hierarchy table.
-- =============================================
*/
CREATE PROCEDURE dbo.Hierarchy_Insert
	
	@Input dbo.Hierarchy_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   ParentId
			  ,ChildId
			  ,Type
			  ,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.Hierarchy
			(
			   ParentId
			  ,ChildId
			  ,Type
			  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
			   ParentId
			  ,ChildId
			  ,Type
			  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure can be used to insert data into dbo.LanguageCode table.
-- =============================================
*/
CREATE PROCEDURE dbo.LanguageCode_Insert
	
	@Input dbo.LanguageCode_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
			   ISO6393
			  ,ISO6392
			  ,ISO6391
			  ,Language
			FROM @Input
		)
		AND NOT EXISTS
		(
			SELECT
				ISO6393
			FROM dbo.LanguageCode
			WHERE ISO6393 IN (SELECT ISO6393 FROM @Input)
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.LanguageCode
			(
			   ISO6393
			  ,ISO6392
			  ,ISO6391
			  ,Language
			)
			OUTPUT INSERTED.*
			SELECT 
			   ISO6393
			  ,ISO6392
			  ,ISO6391
			  ,Language
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;

	END CATCH
END
GO

/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015
Description: this stored procedure can be used to insert data into the dbo.RawData table.
=============================================
*/
CREATE PROCEDURE dbo.RawData_Insert
	
	@Input dbo.RawData_TVP READONLY

AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT 
				   GeonameId
				  ,Name
				  ,ASCIIName
				  ,AlternateNames
				  ,Latitude
				  ,Longitude
				  ,FeatureCategoryId
				  ,FeatureCodeId
				  ,ISOCountryCode
				  ,CC2
				  ,Admin1Code
				  ,Admin2Code
				  ,Admin3Code
				  ,Admin4Code
				  ,Population
				  ,Elevation
				  ,DEM
				  ,TimeZoneId
				  ,ModificationDate
				  ,RowId
			FROM @Input
		)

		BEGIN TRANSACTION
			
			INSERT INTO dbo.RawData
			(
			       Name
				  ,ASCIIName
				  ,AlternateNames
				  ,Latitude
				  ,Longitude
				  ,FeatureCategoryId
				  ,FeatureCodeId
				  ,ISOCountryCode
				  ,CC2
				  ,Admin1Code
				  ,Admin2Code
				  ,Admin3Code
				  ,Admin4Code
				  ,Population
				  ,Elevation
				  ,DEM
				  ,TimeZoneId
				  ,ModificationDate
				  ,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
				   Name
				  ,ASCIIName
				  ,AlternateNames
				  ,Latitude
				  ,Longitude
				  ,FeatureCategoryId
				  ,FeatureCodeId
				  ,ISOCountryCode
				  ,CC2
				  ,Admin1Code
				  ,Admin2Code
				  ,Admin3Code
				  ,Admin4Code
				  ,Population
				  ,Elevation
				  ,DEM
				  ,TimeZoneId
				  ,ModificationDate
				  ,NULL
			FROM @Input;
		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH

END
GO


/*
=============================================
Author: Parakh Singhal
Create date: September 19, 2015         
Description: This stored procedure inserts data into the dbo.RawPostal table.
-- =============================================
*/
CREATE PROCEDURE dbo.RawPostal_Insert
	
	@Input dbo.RawPostal_TVP READONLY
AS
BEGIN
	
	SET NOCOUNT ON;

    BEGIN TRY

		IF EXISTS
		(
			SELECT
				 ISOCountryCode
				,PostalCode
				,PlaceName
				,Admin1Name
				,Admin1Code
				,Admin2Name
				,Admin2Code
				,Admin3Name
				,Admin3Code
				,Latitude
				,Longitude
				,Accuracy
				,RowId
			FROM @Input
		)
		BEGIN TRANSACTION
			
			INSERT INTO dbo.RawPostal
			(
				 ISOCountryCode
				,PostalCode
				,PlaceName
				,Admin1Name
				,Admin1Code
				,Admin2Name
				,Admin2Code
				,Admin3Name
				,Admin3Code
				,Latitude
				,Longitude
				,Accuracy
				,RowId
			)
			OUTPUT INSERTED.*
			SELECT 
				ISOCountryCode
				,PostalCode
				,PlaceName
				,Admin1Name
				,Admin1Code
				,Admin2Name
				,Admin2Code
				,Admin3Name
				,Admin3Code
				,Latitude
				,Longitude
				,Accuracy
				,NULL
			FROM @Input;

		COMMIT;

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure inserts values in dbo.Admin1Code table
=============================================
*/
CREATE PROCEDURE dbo.TimeZone_Insert

	@Input dbo.TimeZone_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY
		
		BEGIN TRANSACTION
			
			IF EXISTS ( SELECT
						   ISOCountryCode
						  ,TimeZoneId
						  ,GMT
						  ,DST
						  ,RawOffset
						  ,RowId						 
						FROM @Input)
				
					BEGIN
						INSERT INTO dbo.TimeZone 
						(
							   ISOCountryCode
							  ,TimeZoneId
							  ,GMT
							  ,DST
							  ,RawOffset
							  ,RowId
						) 
						OUTPUT INSERTED.*					
						SELECT
							   ISOCountryCode
							  ,TimeZoneId
							  ,GMT
							  ,DST
							  ,RawOffset      
							  ,NULL
						FROM @Input
					END

		COMMIT TRANSACTION;
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.Admin1Code table
-- =============================================
*/
CREATE PROCEDURE dbo.Admin1Code_Update
	
	@Input dbo.Admin1Code_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.Admin1Code.RowId 
			FROM 
			dbo.Admin1Code
			INNER JOIN @Input as Input			
			ON dbo.Admin1Code.Admin1CodeId = Input.Admin1CodeId
			WHERE 			
			Input.RowId = dbo.Admin1Code.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.Admin1Code
					SET
						Admin1CodeName = Input.Admin1CodeName,
						ASCIIName = Input.ASCIIName,
						GeonameId = Input.GeonameId
					OUTPUT INSERTED.*
					FROM dbo.Admin1Code
					INNER JOIN @Input AS Input
					ON dbo.Admin1Code.Admin1CodeId = Input.Admin1CodeId
					WHERE
					dbo.Admin1Code.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
			   Admin1CodeId
			  ,Admin1CodeName
			  ,ASCIIName
			  ,GeonameId
			  ,RowId
			 FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.Admin2Code table
-- =============================================
*/
CREATE PROCEDURE dbo.Admin2Code_Update
	
	@Input dbo.Admin2Code_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.Admin2Code.RowId 
			FROM 
			dbo.Admin2Code
			INNER JOIN @Input as Input			
			ON dbo.Admin2Code.Admin2CodeId = Input.Admin2CodeId
			WHERE 			
			Input.RowId = dbo.Admin2Code.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.Admin2Code
					SET
					   Admin2CodeName	=	Input.Admin2CodeName
					  ,ASCIIName		=	Input.ASCIIName
					  ,GeonameId		=	Input.GeonameId
					OUTPUT INSERTED.*
					FROM dbo.Admin2Code
					INNER JOIN @Input AS Input
					ON dbo.Admin2Code.Admin2CodeId = Input.Admin2CodeId
					WHERE
					dbo.Admin2Code.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
			   Admin2CodeId
			  ,Admin2CodeName
			  ,ASCIIName
			  ,GeonameId
			  ,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.AlternateName table
-- =============================================
*/
CREATE PROCEDURE dbo.AlternateName_Update
	
	@Input dbo.AlternateName_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.AlternateName.RowId 
			FROM 
			dbo.AlternateName
			INNER JOIN @Input as Input			
			ON dbo.AlternateName.AlternateNameId = Input.AlternateNameId
			WHERE 			
			Input.RowId = dbo.AlternateName.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.AlternateName
					SET
					   GeonameId			=	Input.GeonameId
					  ,ISO6393LanguageCode	=	Input.ISO6393LanguageCode
					  ,AlternateName		=	Input.AlternateName
					  ,IsPreferredName		=	Input.IsPreferredName
					  ,IsShortName			=	Input.IsShortName
					  ,IsColloquial			=	Input.IsColloquial
					  ,IsHistoric			=	Input.IsHistoric
					OUTPUT INSERTED.*
					FROM dbo.AlternateName
					INNER JOIN @Input AS Input
					ON dbo.AlternateName.AlternateNameId = Input.AlternateNameId
					WHERE
					dbo.AlternateName.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT * FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.Continent table
-- =============================================
*/
CREATE PROCEDURE dbo.Continent_Update
	
	@Input dbo.Continent_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.Continent.RowId 
			FROM 
			dbo.Continent
			INNER JOIN @Input as Input			
			ON dbo.Continent.ContinentCodeId = Input.ContinentCodeId
			WHERE 			
			Input.RowId = dbo.Continent.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.Continent
					SET
					   GeonameId	=	Input.GeonameId
					  ,Continent	=	Input.Continent					  
					OUTPUT INSERTED.*
					FROM dbo.Continent
					INNER JOIN @Input AS Input
					ON dbo.Continent.ContinentCodeId = Input.ContinentCodeId
					WHERE
					dbo.Continent.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
			   ContinentCodeId
			  ,GeonameId
			  ,Continent
			  ,RowId
			 FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.Country table
-- =============================================
*/
CREATE PROCEDURE dbo.Country_Update
	
	@Input dbo.Country_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.Country.RowId 
			FROM 
			dbo.Country
			INNER JOIN @Input as Input			
			ON dbo.Country.ISOCountryCode = Input.ISOCountryCode
			WHERE 			
			Input.RowId = dbo.Country.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.Country
					SET
					   ISO3Code				=	Input.ISO3Code
					  ,ISONumeric			=	Input.ISONumeric
					  ,FIPSCode				=	Input.FIPSCode
					  ,CountryName			=	Input.CountryName
					  ,Capital				=	Input.Capital
					  ,SqKmArea				=	Input.SqKmArea
					  ,TotalPopulation		=	Input.TotalPopulation
					  ,ContinentCodeId		=	Input.ContinentCodeId
					  ,TopLevelDomain		=	Input.TopLevelDomain
					  ,CurrencyCode			=	Input.CurrencyCode
					  ,CurrencyName			=	Input.CurrencyName
					  ,Phone				=	Input.Phone
					  ,PostalFormat			=	Input.PostalFormat
					  ,PostalRegex			=	Input.PostalRegex
					  ,Languages			=	Input.Languages
					  ,GeonameId			=	Input.GeonameId
					  ,Neighbors			=	Input.Neighbors
					  ,EquivalentFipsCode	=	Input.EquivalentFipsCode  
					OUTPUT INSERTED.*
					FROM dbo.Country
					INNER JOIN @Input AS Input
					ON dbo.Country.ISOCountryCode = Input.ISOCountryCode
					WHERE
					dbo.Country.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
			   ISOCountryCode
			  ,ISO3Code
			  ,ISONumeric
			  ,FIPSCode
			  ,CountryName
			  ,Capital
			  ,SqKmArea
			  ,TotalPopulation
			  ,ContinentCodeId
			  ,TopLevelDomain
			  ,CurrencyCode
			  ,CurrencyName
			  ,Phone
			  ,PostalFormat
			  ,PostalRegex
			  ,Languages
			  ,GeonameId
			  ,Neighbors
			  ,EquivalentFipsCode
			  ,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.FeatureCategory table
-- =============================================
*/
CREATE PROCEDURE dbo.FeatureCategory_Update
	
	@Input dbo.FeatureCategory_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.FeatureCategory.RowId 
			FROM 
			dbo.FeatureCategory
			INNER JOIN @Input as Input			
			ON dbo.FeatureCategory.FeatureCategoryId = Input.FeatureCategoryId
			WHERE 			
			Input.RowId = dbo.FeatureCategory.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.FeatureCategory
					SET
						FeatureCategoryName		=	Input.FeatureCategoryName
					OUTPUT INSERTED.*
					FROM dbo.FeatureCategory
					INNER JOIN @Input AS Input
					ON dbo.FeatureCategory.FeatureCategoryId = Input.FeatureCategoryId
					WHERE
					dbo.FeatureCategory.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
				 FeatureCategoryId
				,FeatureCategoryName
				,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.FeatureCode table
-- =============================================
*/
CREATE PROCEDURE dbo.FeatureCode_Update
	
	@Input dbo.FeatureCode_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.FeatureCode.RowId 
			FROM 
			dbo.FeatureCode
			INNER JOIN @Input as Input			
			ON dbo.FeatureCode.FeatureCodeId = Input.FeatureCodeId
			WHERE 			
			Input.RowId = dbo.FeatureCode.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.FeatureCode
					SET
						 FeatureCodeName	=	Input.FeatureCodeName
						,Description		=	Input.Description
					OUTPUT INSERTED.*
					FROM dbo.FeatureCode
					INNER JOIN @Input AS Input
					ON dbo.FeatureCode.FeatureCodeId = Input.FeatureCodeId
					WHERE
					dbo.FeatureCode.RowId = Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
				 FeatureCodeId
				,FeatureCodeName
				,Description
				,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.Hierachy table
-- =============================================
*/
CREATE PROCEDURE dbo.Hierarchy_Update
	
	@Input dbo.Hierarchy_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.Hierarchy.RowId 
			FROM 
			dbo.Hierarchy
			INNER JOIN @Input as Input			
			ON dbo.Hierarchy.ParentId = Input.ParentId
			WHERE 			
			Input.ChildId	=	dbo.Hierarchy.ChildId	AND
			Input.RowId		=	dbo.Hierarchy.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.Hierarchy
					SET
						Type	=	Input.Type
					OUTPUT INSERTED.*
					FROM dbo.Hierarchy
					INNER JOIN @Input AS Input
					ON dbo.Hierarchy.ParentId = Input.ParentId
					WHERE
					dbo.Hierarchy.ChildId	=	Input.ChildId	AND
					dbo.Hierarchy.RowId		=	Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
			    ParentId
			   ,ChildId
			   ,Type
			   ,RowId
			 FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.LanguageCode table
-- =============================================
*/
CREATE PROCEDURE dbo.LanguageCode_Update
	
	@Input dbo.LanguageCode_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.LanguageCode.RowId 
			FROM 
			dbo.LanguageCode
			INNER JOIN @Input as Input			
			ON dbo.LanguageCode.ISO6393 = Input.ISO6393
			WHERE
			Input.RowId = dbo.LanguageCode.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.LanguageCode
					SET
					   ISO6393	=	Input.ISO6393
					  ,ISO6392	=	Input.ISO6392
					  ,ISO6391	=	Input.ISO6391
					  ,Language	=	Input.Language			  
					OUTPUT INSERTED.*
					FROM dbo.LanguageCode
					INNER JOIN @Input AS Input
					ON dbo.LanguageCode.ISO6393 = Input.ISO6393
					WHERE
					dbo.LanguageCode.RowId	=	Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
				   ISO6393
				  ,ISO6392
				  ,ISO6391
				  ,Language
				  ,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/*
=============================================
Author:	Parakh Singhal
Create date: September 19, 2015
Description: This stored procedure updates data in the dbo.RawData table
-- =============================================
*/
CREATE PROCEDURE dbo.RawData_Update
	
	@Input dbo.RawData_TVP READONLY

AS
BEGIN

	SET NOCOUNT ON;

	BEGIN TRY

	DECLARE @RowCount_SentData		int		= NULL,
			@RowCount_UpdatableData int		= NULL;

		
		WITH CTE_TEMP AS
		(
			SELECT			    
				Input.RowId
			FROM 
			@Input as Input
			INTERSECT
			SELECT 
				dbo.RawData.RowId 
			FROM 
			dbo.RawData
			INNER JOIN @Input as Input			
			ON dbo.RawData.GeonameId = Input.GeonameId
			WHERE
			Input.RowId = dbo.RawData.RowId
		)

		SELECT @RowCount_UpdatableData = Count(1) FROM CTE_TEMP;
		SELECT @RowCount_SentData = Count(1) FROM @Input;

		IF @RowCount_SentData = @RowCount_UpdatableData
		BEGIN
			BEGIN TRANSACTION
			
					UPDATE dbo.RawData
					SET
					   Name					=	Input.Name
					  ,ASCIIName			=	Input.ASCIIName
					  ,AlternateNames		=	Input.AlternateNames
					  ,Latitude				=	Input.Latitude
					  ,Longitude			=	Input.Longitude
					  ,FeatureCategoryId	=	Input.FeatureCategoryId
					  ,FeatureCodeId		=	Input.FeatureCodeId
					  ,ISOCountryCode		=	Input.ISOCountryCode
					  ,CC2					=	Input.CC2
					  ,Admin1Code			=	Input.Admin1Code
					  ,Admin2Code			=	Input.Admin2Code
					  ,Admin3Code			=	Input.Admin3Code
					  ,Admin4Code			=	Input.Admin4Code
					  ,Population			=	Input.Population
					  ,Elevation			=	Input.Elevation
					  ,DEM					=	Input.DEM
					  ,TimeZoneId			=	Input.TimeZoneId
					  ,ModificationDate		=	Input.  ModificationDate
					OUTPUT INSERTED.*
					FROM dbo.RawData
					INNER JOIN @Input AS Input
					ON dbo.RawData.GeonameId = Input.GeonameId
					WHERE
					dbo.RawData.RowId	=	Input.RowId
			
			COMMIT TRANSACTION;
		END

		ELSE
		BEGIN
			SELECT 
				   GeonameId
				  ,Name
				  ,ASCIIName
				  ,AlternateNames
				  ,Latitude
				  ,Longitude
				  ,FeatureCategoryId
				  ,FeatureCodeId
				  ,ISOCountryCode
				  ,CC2
				  ,Admin1Code
				  ,Admin2Code
				  ,Admin3Code
				  ,Admin4Code
				  ,Population
				  ,Elevation
				  ,DEM
				  ,TimeZoneId
				  ,ModificationDate
				  ,RowId
			FROM @Input;
		END

	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION;
		THROW;

	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.Admin1Code table.
=============================================
*/
CREATE PROCEDURE [dbo].[Admin1Code_Delete]

	@Input nvarchar(32)

AS
BEGIN

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.Admin1Code
					WHERE Admin1CodeId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.Admin1Code
					SET IsActive = 0
					WHERE Admin1CodeId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.Admin2Code table.
=============================================
*/
CREATE PROCEDURE [dbo].[Admin2Code_Delete]

	@Input nvarchar(32)

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.Admin2Code
					WHERE Admin2CodeId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.Admin2Code
					SET IsActive = 0
					WHERE Admin2CodeId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.AlternateName table.
=============================================
*/
CREATE PROCEDURE [dbo].[AlternateName_Delete]

	@Input int

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.AlternateName
					WHERE AlternateNameId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.AlternateName
					SET IsActive = 0
					WHERE AlternateNameId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.Continent table.
=============================================
*/
CREATE PROCEDURE [dbo].[Continent_Delete]

	@Input int

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.Continent
					WHERE ContinentCodeId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.Continent
					SET IsActive = 0
					WHERE ContinentCodeId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.Country table.
=============================================
*/
CREATE PROCEDURE [dbo].[Country_Delete]

	@Input char(2)

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.Country
					WHERE ISOCountryCode = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.Country
					SET IsActive = 0
					WHERE ISOCountryCode = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.FeatureCategory table.
=============================================
*/
CREATE PROCEDURE [dbo].[FeatureCategory_Delete]

	@Input char(1)

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.FeatureCategory
					WHERE FeatureCategoryId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.FeatureCategory
					SET IsActive = 0
					WHERE FeatureCategoryId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.FeatureCode table.
=============================================
*/
CREATE PROCEDURE [dbo].[FeatureCode_Delete]

	@Input nvarchar(16)

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.FeatureCode
					WHERE FeatureCodeId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.FeatureCode
					SET IsActive = 0
					WHERE FeatureCodeId = @Input
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.Hierarchy table.
=============================================
*/
CREATE PROCEDURE [dbo].[Hierarchy_Delete]

	@Input_ParentId int,
	@Input_ChildId int

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.Hierarchy
					WHERE 
					ParentId = @Input_ParentId AND
					ChildId = @Input_ChildId)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.Hierarchy
					SET IsActive = 0
					WHERE 
					ParentId = @Input_ParentId AND
					ChildId = @Input_ChildId
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.LanguageCode table.
=============================================
*/
CREATE PROCEDURE [dbo].[LanguageCode_Delete]

	@Input nvarchar(24)

AS
BEGIN

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.LanguageCode
					WHERE 					
					ISO6393 = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.LanguageCode
					SET IsActive = 0
					WHERE 
					ISO6393 = @Input;
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.RawData table.
=============================================
*/
CREATE PROCEDURE [dbo].[RawData_Delete]

	@Input int

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.RawData
					WHERE 					
					GeonameId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.RawData
					SET IsActive = 0
					WHERE 
					GeonameId = @Input;
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO

/* 
=============================================
Author:	Parakh Singhal
Create date: September 18, 2015
Description: This stored procedure deletes a single value in dbo.TimeZone table.
=============================================
*/
CREATE PROCEDURE [dbo].[TimeZone_Delete]

	@Input nvarchar(128)

AS
BEGIN	

	BEGIN TRY
		
		IF EXISTS ( SELECT					
					RowId
					FROM dbo.TimeZone
					WHERE 					
					TimeZoneId = @Input)
			BEGIN
				BEGIN TRANSACTION	

					UPDATE dbo.TimeZone
					SET IsActive = 0
					WHERE 
					TimeZoneId = @Input;
						
				COMMIT TRANSACTION;
			END
	END TRY

	BEGIN CATCH

		ROLLBACK TRANSACTION; 
		THROW;
		 
	END CATCH
END
GO