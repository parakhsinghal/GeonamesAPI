-- Change the transaction logging mode to only do minimal logging.

ALTER DATABASE Geonames
SET RECOVERY BULK_LOGGED;
GO

-- Insert data into the created tables

SET IDENTITY_INSERT dbo.RawData OFF 
GO

BULK INSERT RawData FROM 'Place where the RawData file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, KeepIdentity, MaxErrors = 100);
GO

SET IDENTITY_INSERT dbo.RawData ON 
GO

BULK INSERT LanguageCode FROM 'Place where the LanguageCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT FeatureCode FROM 'Place where the FeatureCode file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

INSERT INTO FeatureCateGOry (FeatureCateGOryId, FeatureCateGOryName)
VALUES  ('A', 'Country, State, Region, etc.'),
        ('H', 'Stream, Lake, etc.'),
        ('L', 'Parks, Areas, etc.'),
        ('P', 'Cities, Villages, etc.'),
        ('R', 'Roads, Railroads, etc.'),
        ('S', 'Spots, buildings, farms, etc.'),
        ('T', 'Mountain, Hill, Rock, etc.'),
        ('U', 'Undersea'),
        ('V', 'Forest, Heath, etc.');
GO

-- Make sure that you delete the first line in the csv file, if it has coulmn names in it.

BULK INSERT Country FROM 'Place where the Country data file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Admin1Code FROM 'place where the Admin1Code is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Admin2Code FROM 'Place where the Admin2Code file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

INSERT INTO  Continent (ContinentCodeId, Continent)
VALUES  ('AF', 'Africa'),
        ('AS', 'Asia'),
        ('EU', 'Europe'),
        ('NA', 'North America'),
        ('OC', 'Oceania'),
        ('SA', 'South America'),
        ('AN', 'Antarctica');
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'Africa'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'Africa'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'Antarctica'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'Antarctica'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'Asia'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'Asia'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'Europe'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'Europe'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'North America'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'North America'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'Oceania'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'Oceania'
GO

UPDATE dbo.Continent
Set dbo.Continent.GeonameId = 
(SELECT dbo.RawData.GeonameId
FROM dbo.RawData
WHERE dbo.RawData.ASCIIName = 'South America'
AND dbo.RawData.FeatureCodeId = 'CONT')
WHERE dbo.Continent.Continent = 'South America'
GO

SET IDENTITY_INSERT dbo.AlternateName OFF
GO

BULK INSERT dbo.AlternateName FROM 'Place where the AlterNames file is' 
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

SET IDENTITY_INSERT dbo.AlternateName ON
GO

UPDATE dbo.AlternateName
SET dbo.AlternateName.ISO6393LanguageCode = dbo.LanguageCode.ISO6393
FROM dbo.AlternateName
INNER JOIN dbo.LanguageCode
ON dbo.AlternateName.ISO6393LanguageCode = dbo.LanguageCode.ISO6392
WHERE LEN(dbo.AlternateName.ISO6393LanguageCode) = 3
GO

UPDATE dbo.AlternateName
SET dbo.AlternateName.ISO6393LanguageCode = dbo.LanguageCode.ISO6393
FROM dbo.AlternateName
INNER JOIN dbo.LanguageCode
ON dbo.AlternateName.ISO6393LanguageCode = dbo.LanguageCode.ISO6391
WHERE LEN(dbo.AlternateName.ISO6393LanguageCode) = 2
GO

BULK INSERT Timezone FROM 'Palce where teh TimeZone file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT RawPostal FROM 'Place where the RawPostal file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

BULK INSERT Hierarchy FROM 'Place where the Hierarchy file is'
WITH (FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n', TabLock, MaxErrors = 100);
GO

-- Change the transaction logging mode to only do full logging.

ALTER DATABASE Geonames
SET RECOVERY FULL;