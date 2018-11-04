using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using Microsoft.Extensions.Configuration;

namespace GeonamesAPI.SQLRepository
{
    public class CountrySQLRepository : ICountryRepository
    {
        private readonly sqlRepositoryHelper sqlRepositoryHelper;

        public CountrySQLRepository(IConfiguration configuration)
        {
            DBDataHelper.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            sqlRepositoryHelper = new sqlRepositoryHelper(configuration);
        }


        public IEnumerable<Country> GetAllCountries(int? pageSize = null, int? pageNumber = null)
        {
            string sql = sqlRepositoryHelper.GetCountryInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<Country> result = new List<Country>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new Country()
                            {
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr["ISO3Code"].ToString(),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? 0 : int.Parse(dr["ISONumeric"].ToString()),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr["FIPSCode"].ToString(),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr["CountryName"].ToString(),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr["Capital"].ToString(),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? 0f : double.Parse(dr["SqKmArea"].ToString()),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? 0 : long.Parse(dr["TotalPopulation"].ToString()),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr["ContinentCodeId"].ToString(),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr["TopLevelDomain"].ToString(),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr["CurrencyCode"].ToString(),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr["CurrencyName"].ToString(),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr["Phone"].ToString(),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr["PostalFormat"].ToString(),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr["PostalRegex"].ToString(),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr["Languages"].ToString(),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr["Neighbors"].ToString(),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr["EquivalentFipsCode"].ToString(),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }

        public Country GetCountryInfo(string isoCountryCode = null, string countryName = null)
        {
            string sql = sqlRepositoryHelper.GetCountryInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("CountryName", countryName));

            Country result = new Country();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = new Country()
                            {
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr["ISO3Code"].ToString(),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? 0 : int.Parse(dr["ISONumeric"].ToString()),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr["FIPSCode"].ToString(),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr["CountryName"].ToString(),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr["Capital"].ToString(),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? 0f : double.Parse(dr["SqKmArea"].ToString()),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? 0 : long.Parse(dr["TotalPopulation"].ToString()),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr["ContinentCodeId"].ToString(),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr["TopLevelDomain"].ToString(),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr["CurrencyCode"].ToString(),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr["CurrencyName"].ToString(),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr["Phone"].ToString(),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr["PostalFormat"].ToString(),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr["PostalRegex"].ToString(),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr["Languages"].ToString(),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr["Neighbors"].ToString(),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr["EquivalentFipsCode"].ToString(),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            };
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetCountryFeatureCategoryFeatureCode(string featureCategoryId, string isoCountryCode = null, string countryName = null, string featureCodeId = null, int? pageSize = null, int? pageNumber = null)
        {
            string sql = sqlRepositoryHelper.GetCountryFeatureCategoryFeatureCode;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("FeatureCategoryId", featureCategoryId));
            parameterCollection.Add(new SqlParameter("FeatureCodeId", featureCodeId));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));

            List<RawData> result = new List<RawData>();
            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new RawData()
                            {
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                                ASCIIName = dr["ASCIIName"] == DBNull.Value ? string.Empty : dr["ASCIIName"].ToString(),
                                AlternateNames = dr["AlternateNames"] == DBNull.Value ? string.Empty : dr["AlternateNames"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                                FeatureCodeId = dr["FeatureCodeId"] == DBNull.Value ? string.Empty : dr["FeatureCodeId"].ToString(),
                                CC2 = dr["CC2"] != DBNull.Value ? string.Empty : dr["CC2"].ToString(),
                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                                Admin4Code = dr["Admin4Code"] == DBNull.Value ? string.Empty : dr["Admin4Code"].ToString(),
                                Population = dr["Population"] == DBNull.Value ? 0 : long.Parse(dr["Population"].ToString()),
                                Elevation = dr["Elevation"] == DBNull.Value ? 0 : int.Parse(dr["Elevation"].ToString()),
                                DEM = dr["DEM"] == DBNull.Value ? 0 : int.Parse(dr["DEM"].ToString()),
                                ModificationDate = dr["ModificationDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["ModificationDate"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetStates(string countryName = null, string isoCountryCode = null, int? pageNumber = null, int? pageSize = null)
        {
            string sql = sqlRepositoryHelper.GetStateInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<RawData> result = new List<RawData>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new RawData()
                            {
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                                ASCIIName = dr["ASCIIName"] == DBNull.Value ? string.Empty : dr["ASCIIName"].ToString(),
                                AlternateNames = dr["AlternateNames"] == DBNull.Value ? string.Empty : dr["AlternateNames"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                                FeatureCodeId = dr["FeatureCodeId"] == DBNull.Value ? string.Empty : dr["FeatureCodeId"].ToString(),
                                CC2 = dr["CC2"] != DBNull.Value ? string.Empty : dr["CC2"].ToString(),
                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                                Admin4Code = dr["Admin4Code"] == DBNull.Value ? string.Empty : dr["Admin4Code"].ToString(),
                                Population = dr["Population"] == DBNull.Value ? 0 : long.Parse(dr["Population"].ToString()),
                                Elevation = dr["Elevation"] == DBNull.Value ? 0 : int.Parse(dr["Elevation"].ToString()),
                                DEM = dr["DEM"] == DBNull.Value ? 0 : int.Parse(dr["DEM"].ToString()),
                                ModificationDate = dr["ModificationDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["ModificationDate"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }

        public RawData GetStateInfo(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null)
        {
            string sql = sqlRepositoryHelper.GetStateInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("StateName", stateName));
            parameterCollection.Add(new SqlParameter("StateGeonameId", stateGeonameId));

            RawData result = new RawData();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result = new RawData()
                            {
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                                ASCIIName = dr["ASCIIName"] == DBNull.Value ? string.Empty : dr["ASCIIName"].ToString(),
                                AlternateNames = dr["AlternateNames"] == DBNull.Value ? string.Empty : dr["AlternateNames"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                                FeatureCodeId = dr["FeatureCodeId"] == DBNull.Value ? string.Empty : dr["FeatureCodeId"].ToString(),
                                CC2 = dr["CC2"] != DBNull.Value ? string.Empty : dr["CC2"].ToString(),
                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                                Admin4Code = dr["Admin4Code"] == DBNull.Value ? string.Empty : dr["Admin4Code"].ToString(),
                                Population = dr["Population"] == DBNull.Value ? 0 : long.Parse(dr["Population"].ToString()),
                                Elevation = dr["Elevation"] == DBNull.Value ? 0 : int.Parse(dr["Elevation"].ToString()),
                                DEM = dr["DEM"] == DBNull.Value ? 0 : int.Parse(dr["DEM"].ToString()),
                                ModificationDate = dr["ModificationDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["ModificationDate"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            };
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetCitiesInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null, int? pageSize = null, int? pageNumber = null)
        {
            string sql = sqlRepositoryHelper.GetCitiesInAState;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("StateGeonameId", stateGeonameId));
            parameterCollection.Add(new SqlParameter("StateName", stateName));
            parameterCollection.Add(new SqlParameter("CityName", cityName));
            parameterCollection.Add(new SqlParameter("CityGeonameId", cityGeonameId));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));

            List<RawData> result = new List<RawData>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new RawData()
                            {
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                                ASCIIName = dr["ASCIIName"] == DBNull.Value ? string.Empty : dr["ASCIIName"].ToString(),
                                AlternateNames = dr["AlternateNames"] == DBNull.Value ? string.Empty : dr["AlternateNames"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                                FeatureCodeId = dr["FeatureCodeId"] == DBNull.Value ? string.Empty : dr["FeatureCodeId"].ToString(),
                                CC2 = dr["CC2"] != DBNull.Value ? string.Empty : dr["CC2"].ToString(),
                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                                Admin4Code = dr["Admin4Code"] == DBNull.Value ? string.Empty : dr["Admin4Code"].ToString(),
                                Population = dr["Population"] == DBNull.Value ? 0 : long.Parse(dr["Population"].ToString()),
                                Elevation = dr["Elevation"] == DBNull.Value ? 0 : int.Parse(dr["Elevation"].ToString()),
                                DEM = dr["DEM"] == DBNull.Value ? 0 : int.Parse(dr["DEM"].ToString()),
                                ModificationDate = dr["ModificationDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["ModificationDate"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }

        public RawData GetCityInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null)
        {
            string sql = sqlRepositoryHelper.GetCitiesInAState;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("StateGeonameId", stateGeonameId));
            parameterCollection.Add(new SqlParameter("StateName", stateName));
            parameterCollection.Add(new SqlParameter("CityName", cityName));
            parameterCollection.Add(new SqlParameter("CityGeonameId", cityGeonameId));

            RawData result = new RawData();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        result = new RawData()
                        {
                            GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                            Name = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString(),
                            ASCIIName = dr["ASCIIName"] == DBNull.Value ? string.Empty : dr["ASCIIName"].ToString(),
                            AlternateNames = dr["AlternateNames"] == DBNull.Value ? string.Empty : dr["AlternateNames"].ToString(),
                            Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                            Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                            FeatureCodeId = dr["FeatureCodeId"] == DBNull.Value ? string.Empty : dr["FeatureCodeId"].ToString(),
                            CC2 = dr["CC2"] != DBNull.Value ? string.Empty : dr["CC2"].ToString(),
                            Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                            Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                            Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                            Admin4Code = dr["Admin4Code"] == DBNull.Value ? string.Empty : dr["Admin4Code"].ToString(),
                            Population = dr["Population"] == DBNull.Value ? 0 : long.Parse(dr["Population"].ToString()),
                            Elevation = dr["Elevation"] == DBNull.Value ? 0 : int.Parse(dr["Elevation"].ToString()),
                            DEM = dr["DEM"] == DBNull.Value ? 0 : int.Parse(dr["DEM"].ToString()),
                            ModificationDate = dr["ModificationDate"] == DBNull.Value ? DateTime.MinValue : DateTime.Parse(dr["ModificationDate"].ToString()),
                            RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<Country> UpdateCountries(IEnumerable<Upd_VM.Country> countries)
        {
            string sql = sqlRepositoryHelper.UpdateCountries;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable countriesInputTable = new DataTable("Country_TVP");
            countriesInputTable.Columns.Add("ISOCountryCode");
            countriesInputTable.Columns.Add("ISO3Code");
            countriesInputTable.Columns.Add("ISONumeric");
            countriesInputTable.Columns.Add("FIPSCode");
            countriesInputTable.Columns.Add("CountryName");
            countriesInputTable.Columns.Add("Capital");
            countriesInputTable.Columns.Add("SqKmArea");
            countriesInputTable.Columns.Add("TotalPopulation");
            countriesInputTable.Columns.Add("ContinentCodeId");
            countriesInputTable.Columns.Add("TopLevelDomain");
            countriesInputTable.Columns.Add("CurrencyCode");
            countriesInputTable.Columns.Add("CurrencyName");
            countriesInputTable.Columns.Add("Phone");
            countriesInputTable.Columns.Add("PostalFormat");
            countriesInputTable.Columns.Add("PostalRegex");
            countriesInputTable.Columns.Add("Languages");
            countriesInputTable.Columns.Add("GeonameId");
            countriesInputTable.Columns.Add("Neighbors");
            countriesInputTable.Columns.Add("EquivalentFipsCode");
            countriesInputTable.Columns.Add("RowId", typeof(byte[]));

            foreach (Upd_VM.Country country in countries)
            {
                countriesInputTable.Rows.Add(new object[]
                                {
                                    country.ISOCountryCode,
                                    country.ISONumeric,
                                    country.ISO3Code,
                                    country.FIPSCode,
                                    country.CountryName,
                                    country.Capital,
                                    country.SqKmArea,
                                    country.TotalPopulation,
                                    country.ContinentCodeId,
                                    country.TopLevelDomain,
                                    country.CurrencyCode,
                                    country.CurrencyName,
                                    country.Phone,
                                    country.PostalFormat,
                                    country.PostalRegex,
                                    country.Languages,
                                    country.GeonameId,
                                    country.Neighbors,
                                    country.EquivalentFipsCode,
                                    country.RowId
                                });
            }

            SqlParameter inputData = new SqlParameter("Country_TVP", countriesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<Country> result = new List<Country>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable countriesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (countriesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in countriesOutputTable.Rows)
                        {
                            result.Add(new Country()
                            {
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr["ISO3Code"].ToString(),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? 0 : int.Parse(dr["ISONumeric"].ToString()),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr["FIPSCode"].ToString(),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr["CountryName"].ToString(),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr["Capital"].ToString(),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? 0f : double.Parse(dr["SqKmArea"].ToString()),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? 0 : long.Parse(dr["TotalPopulation"].ToString()),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr["ContinentCodeId"].ToString(),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr["TopLevelDomain"].ToString(),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr["CurrencyCode"].ToString(),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr["CurrencyName"].ToString(),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr["Phone"].ToString(),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr["PostalFormat"].ToString(),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr["PostalRegex"].ToString(),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr["Languages"].ToString(),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr["Neighbors"].ToString(),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr["EquivalentFipsCode"].ToString(),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public IEnumerable<Country> InsertCountries(IEnumerable<Ins_VM.Country> countries)
        {
            string sql = sqlRepositoryHelper.InsertCountries;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable countriesInputTable = new DataTable("Country_TVP");
            countriesInputTable.Columns.Add("ISOCountryCode");
            countriesInputTable.Columns.Add("ISO3Code");
            countriesInputTable.Columns.Add("ISONumeric");
            countriesInputTable.Columns.Add("FIPSCode");
            countriesInputTable.Columns.Add("CountryName");
            countriesInputTable.Columns.Add("Capital");
            countriesInputTable.Columns.Add("SqKmArea");
            countriesInputTable.Columns.Add("TotalPopulation");
            countriesInputTable.Columns.Add("ContinentCodeId");
            countriesInputTable.Columns.Add("TopLevelDomain");
            countriesInputTable.Columns.Add("CurrencyCode");
            countriesInputTable.Columns.Add("CurrencyName");
            countriesInputTable.Columns.Add("Phone");
            countriesInputTable.Columns.Add("PostalFormat");
            countriesInputTable.Columns.Add("PostalRegex");
            countriesInputTable.Columns.Add("Languages");
            countriesInputTable.Columns.Add("GeonameId");
            countriesInputTable.Columns.Add("Neighbors");
            countriesInputTable.Columns.Add("EquivalentFipsCode");

            foreach (Ins_VM.Country country in countries)
            {
                countriesInputTable.Rows.Add(new object[]
                                {
                                    country.ISOCountryCode,
                                    country.ISONumeric,
                                    country.ISO3Code,
                                    country.FIPSCode,
                                    country.CountryName,
                                    country.Capital,
                                    country.SqKmArea,
                                    country.TotalPopulation,
                                    country.ContinentCodeId,
                                    country.TopLevelDomain,
                                    country.CurrencyCode,
                                    country.CurrencyName,
                                    country.Phone,
                                    country.PostalFormat,
                                    country.PostalRegex,
                                    country.Languages,
                                    country.GeonameId,
                                    country.Neighbors,
                                    country.EquivalentFipsCode
                                });
            }

            SqlParameter inputData = new SqlParameter("Country_TVP", countriesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<Country> result = new List<Country>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable countriesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (countriesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in countriesOutputTable.Rows)
                        {
                            result.Add(new Country()
                            {
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr["ISO3Code"].ToString(),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? 0 : int.Parse(dr["ISONumeric"].ToString()),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr["FIPSCode"].ToString(),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr["CountryName"].ToString(),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr["Capital"].ToString(),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? 0f : double.Parse(dr["SqKmArea"].ToString()),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? 0 : long.Parse(dr["TotalPopulation"].ToString()),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr["ContinentCodeId"].ToString(),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr["TopLevelDomain"].ToString(),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr["CurrencyCode"].ToString(),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr["CurrencyName"].ToString(),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr["Phone"].ToString(),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr["PostalFormat"].ToString(),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr["PostalRegex"].ToString(),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr["Languages"].ToString(),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr["Neighbors"].ToString(),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr["EquivalentFipsCode"].ToString(),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public int DeleteCountry(string isoCountryCode)
        {
            throw new NotImplementedException();
        }
    }
}