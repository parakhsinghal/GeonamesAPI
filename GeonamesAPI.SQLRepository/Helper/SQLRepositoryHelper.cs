using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace GeonamesAPI.SQLRepository.Helper
{
    public static class SQLRepositoryHelper
    {
        #region Continent

        public static readonly string GetContinentInfo = ConfigurationManager.AppSettings["GetContinentInfo"];
        public static readonly string GetCountriesInAContinent = ConfigurationManager.AppSettings["GetCountriesInAContinent"];
        public static readonly string UpdateContinents = ConfigurationManager.AppSettings["UpdateContinents"];
        public static readonly string InsertContinents = ConfigurationManager.AppSettings["InsertContinents"];
        public static readonly string DeleteContinent = ConfigurationManager.AppSettings["DeleteContinent"];

        #endregion

        #region Country

        public static readonly string GetCountryInfo = ConfigurationManager.AppSettings["GetCountryInfo"];
        public static readonly string GetCountryFeatureCategoryFeatureCode = ConfigurationManager.AppSettings["GetCountryFeatureCategoryFeatureCode"];
        public static readonly string UpdateCountries = ConfigurationManager.AppSettings["UpdateCountries"];
        public static readonly string InsertCountries = ConfigurationManager.AppSettings["InsertCountries"];
        public static readonly string DeleteCountry = ConfigurationManager.AppSettings["DeleteCountry"];

        #endregion

        #region State

        public static readonly string GetStateInfo = ConfigurationManager.AppSettings["GetStateInfo"];
        public static readonly string GetCitiesInAState = ConfigurationManager.AppSettings["GetCitiesInAState"];

        #endregion

        #region TimeZone

        public static readonly string GetTimeZoneDetails = ConfigurationManager.AppSettings["GetTimeZoneDetails"];
        public static readonly string GetTimeZoneDetailsByPlaceName = ConfigurationManager.AppSettings["GetTimeZoneDetailsByPlaceName"];
        public static readonly string GetDistinctTimeZones = ConfigurationManager.AppSettings["GetDistinctTimeZones"];
        public static readonly string UpdateTimeZones = ConfigurationManager.AppSettings["UpdateTimeZones"];
        public static readonly string InsertTimeZones = ConfigurationManager.AppSettings["InsertTimeZones"];
        public static readonly string DeleteTimeZone = ConfigurationManager.AppSettings["DeleteTimeZone"];

        #endregion

        #region FeatureCategory

        public static readonly string GetFeatureCategoryInfo = ConfigurationManager.AppSettings["GetFeatureCategoryInfo"];
        public static readonly string UpdateFeatureCategories = ConfigurationManager.AppSettings["UpdateFeatureCategories"];
        public static readonly string InsertFeatureCategories = ConfigurationManager.AppSettings["InsertFeatureCategories"];
        public static readonly string DeleteFeatureCategory = ConfigurationManager.AppSettings["DeleteFeatureCategory"];

        #endregion

        #region FeatureCode

        public static readonly string GetFeatureCodeInfo = ConfigurationManager.AppSettings["GetFeatureCodeInfo"];
        public static readonly string UpdateFeatureCodes = ConfigurationManager.AppSettings["UpdateFeatureCodes"];
        public static readonly string InsertFeatureCodes = ConfigurationManager.AppSettings["InsertFeatureCodes"];
        public static readonly string DeleteFeatureCode = ConfigurationManager.AppSettings["DeleteFeatureCode"];

        #endregion

        #region LanguageSQLRepository

        public static readonly string GetLanguageInfo = ConfigurationManager.AppSettings["GetLanguageInfo"];
        public static readonly string UpdateLanguages = ConfigurationManager.AppSettings["UpdateLanguages"];
        public static readonly string InsertLanguages = ConfigurationManager.AppSettings["InsertLanguages"];
        public static readonly string DeleteLanuage = ConfigurationManager.AppSettings["DeleteLanguage"];

        #endregion

        #region RawPostalRepository

        public static readonly string GetPostalCodeInfo = ConfigurationManager.AppSettings["GetPostalCodeInfo"];
        public static readonly string UpdatePostalInfo = ConfigurationManager.AppSettings["UpdatePostalInfo"];
        public static readonly string InsertPstalInfo = ConfigurationManager.AppSettings["InsertPostalInfo"];
        public static readonly string DeletePostalInfo = ConfigurationManager.AppSettings["DeletePostalInfo"];

        #endregion
    }
}