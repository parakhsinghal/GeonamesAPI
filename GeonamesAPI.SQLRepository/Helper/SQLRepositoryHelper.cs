using Microsoft.Extensions.Configuration;

namespace GeonamesAPI.SQLRepository.Helper
{
    internal class sqlRepositoryHelper
    {
        private readonly IConfiguration continentSection, countrySection, languageSection, featureCategorySection, featureCodeSection, timezoneSection, stateSection, rawPostalCodeSection;

        public sqlRepositoryHelper(IConfiguration config)
        {
            continentSection = config.GetSection("SQLRepository").GetSection("ContinentSQLRepository");
            countrySection = config.GetSection("SQLRepository").GetSection("CountrySQLRepository");
            languageSection = config.GetSection("SQLRepository").GetSection("LanguageSQLRepository");
            featureCategorySection = config.GetSection("SQLRepository").GetSection("FeatureCategorySQLRepository");
            featureCodeSection = config.GetSection("SQLRepository").GetSection("FeatureCodeSQLRepository");
            timezoneSection = config.GetSection("SQLRepository").GetSection("TimeZoneSQLRepository"); ;
            stateSection = config.GetSection("SQLRepository").GetSection("StateSQLRepository");
            rawPostalCodeSection = config.GetSection("SQLRepository").GetSection("RawPostalCode");
        }
        #region Continent

        public string GetContinentInfo { get { return continentSection["GetContinentInfo"]; } }
        public string GetCountriesInAContinent { get { return continentSection["GetCountriesInAContinent"]; } }
        public string UpdateContinents { get { return continentSection["UpdateContinents"]; } }
        public string InsertContinents { get { return continentSection["InsertContinents"]; } }
        public string DeleteContinent { get { return continentSection["DeleteContinent"]; } }

        #endregion

        #region Country

        public string GetCountryInfo { get { return countrySection["GetCountryInfo"]; } }
        public string GetCountryFeatureCategoryFeatureCode { get { return countrySection["GetCountryFeatureCategoryFeatureCode"]; } }
        public string UpdateCountries { get { return countrySection["UpdateCountries"]; } }
        public string InsertCountries { get { return countrySection["InsertCountries"]; } }
        public string DeleteCountry { get { return countrySection["DeleteCountry"]; } }

        #endregion

        #region State

        public string GetStateInfo { get { return stateSection["GetStateInfo"]; } }
        public string GetCitiesInAState { get { return stateSection["GetCitiesInAState"]; } }

        #endregion

        #region TimeZone

        public string GetTimeZoneDetails { get { return timezoneSection["GetTimeZoneDetails"]; } }
        public string GetTimeZoneDetailsByPlaceName { get { return timezoneSection["GetTimeZoneDetailsByPlaceName"]; } }
        public string GetDistinctTimeZones { get { return timezoneSection["GetDistinctTimeZones"]; } }
        public string UpdateTimeZones { get { return timezoneSection["UpdateTimeZones"]; } }
        public string InsertTimeZones { get { return timezoneSection["InsertTimeZones"]; } }
        public string DeleteTimeZone { get { return timezoneSection["DeleteTimeZone"]; } }

        #endregion

        #region FeatureCategory

        public string GetFeatureCategoryInfo { get { return featureCategorySection["GetFeatureCategoryInfo"]; } }
        public string UpdateFeatureCategories { get { return featureCategorySection["UpdateFeatureCategories"]; } }
        public string InsertFeatureCategories { get { return featureCategorySection["InsertFeatureCategories"]; } }
        public string DeleteFeatureCategory { get { return featureCategorySection["DeleteFeatureCategory"]; } }

        #endregion

        #region FeatureCode

        public string GetFeatureCodeInfo { get { return featureCodeSection["GetFeatureCodeInfo"]; } }
        public string UpdateFeatureCodes { get { return featureCodeSection["UpdateFeatureCodes"]; } }
        public string InsertFeatureCodes { get { return featureCodeSection["InsertFeatureCodes"]; } }
        public string DeleteFeatureCode { get { return featureCodeSection["DeleteFeatureCode"]; } }

        #endregion

        #region LanguageSQLRepository

        public string GetLanguageInfo { get { return languageSection["GetLanguageInfo"]; } }
        public string UpdateLanguages { get { return languageSection["UpdateLanguages"]; } }
        public string InsertLanguages { get { return languageSection["InsertLanguages"]; } }
        public string DeleteLanuage { get { return languageSection["DeleteLanguage"]; } }

        #endregion

        #region RawPostalRepository

        public string GetPostalCodeInfo { get { return rawPostalCodeSection["GetPostalCodeInfo"]; } }
        public string UpdatePostalInfo { get { return rawPostalCodeSection["UpdatePostalInfo"]; } }
        public string InsertPstalInfo { get { return rawPostalCodeSection["InsertPostalInfo"]; } }
        public string DeletePostalInfo { get { return rawPostalCodeSection["DeletePostalInfo"]; } }

        #endregion
    }
}