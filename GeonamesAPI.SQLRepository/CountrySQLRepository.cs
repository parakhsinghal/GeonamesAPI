using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.SQLRepository
{
    public class CountrySQLRepository : ICountryRepository
    {
        public CountrySQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<Country> GetAllCountries(int? pageSize = null, int? pageNumber = null)
        {
            string sql = SQLRepositoryHelper.GetCountryInfo;
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
                                    ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr.Field<string>("ISO3Code"),
                                    ISONumeric = dr["ISONumeric"] == DBNull.Value ? null : dr.Field<int?>("ISONumeric"),
                                    FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr.Field<string>("FIPSCode"),
                                    CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr.Field<string>("CountryName"),
                                    Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr.Field<string>("Capital"),
                                    SqKmArea = dr["SqKmArea"] == DBNull.Value ? null : dr.Field<double?>("SqKmArea"),
                                    TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? null : dr.Field<long?>("TotalPopulation"),
                                    ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr.Field<string>("ContinentCodeId"),
                                    TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr.Field<string>("TopLevelDomain"),
                                    CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyCode"),
                                    CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyName"),
                                    Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr.Field<string>("Phone"),
                                    PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalFormat"),
                                    PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalRegex"),
                                    Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr.Field<string>("Languages"),
                                    GeonameId = dr["GeonameId"] == DBNull.Value ? null : dr.Field<int?>("GeonameId"),
                                    Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr.Field<string>("Neighbors"),
                                    EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr.Field<string>("EquivalentFipsCode"),
                                    RowId = dr.Field<byte[]>("RowId"),
                                });
                        }
                    }
                }
            }

            return result;
        }

        public Country GetCountryInfo(string isoCountryCode = null, string countryName = null)
        {
            string sql = SQLRepositoryHelper.GetCountryInfo;
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
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr.Field<string>("ISO3Code"),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? null : dr.Field<int?>("ISONumeric"),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr.Field<string>("FIPSCode"),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr.Field<string>("CountryName"),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr.Field<string>("Capital"),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? null : dr.Field<double?>("SqKmArea"),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? null : dr.Field<long?>("TotalPopulation"),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr.Field<string>("ContinentCodeId"),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr.Field<string>("TopLevelDomain"),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyCode"),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyName"),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr.Field<string>("Phone"),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalFormat"),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalRegex"),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr.Field<string>("Languages"),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? null : dr.Field<int?>("GeonameId"),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr.Field<string>("Neighbors"),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr.Field<string>("EquivalentFipsCode"),
                                RowId = dr.Field<byte[]>("RowId"),
                            };
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetCountryFeatureCategoryFeatureCode(string featureCategoryId, string isoCountryCode = null, string countryName = null, string featureCodeId = null, int? pageSize = null, int? pageNumber = null)
        {
            string sql = SQLRepositoryHelper.GetCountryFeatureCategoryFeatureCode;
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
                                GeonameId = dr["GeonameId"] != DBNull.Value ? dr.Field<int?>("GeonameId") : null,
                                Name = dr["Name"] != DBNull.Value ? dr.Field<string>("Name") : string.Empty,
                                ASCIIName = dr["ASCIIName"] != DBNull.Value ? dr.Field<string>("ASCIIName") : string.Empty,
                                AlternateNames = dr["AlternateNames"] != DBNull.Value ? dr.Field<string>("AlternateNames") : string.Empty,
                                Latitude = dr["Latitude"] != DBNull.Value ? dr.Field<double?>("Latitude") : null,
                                Longitude = dr["Longitude"] != DBNull.Value ? dr.Field<double?>("Longitude") : null,
                                FeatureCodeId = dr["FeatureCodeId"] != DBNull.Value ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                CC2 = dr["CC2"] != DBNull.Value ? dr.Field<string>("CC2") : string.Empty,
                                Admin1Code = dr["Admin1Code"] != DBNull.Value ? dr.Field<string>("Admin1Code") : string.Empty,
                                Admin2Code = dr["Admin2Code"] != DBNull.Value ? dr.Field<string>("Admin2Code") : string.Empty,
                                Admin3Code = dr["Admin3Code"] != DBNull.Value ? dr.Field<string>("Admin3Code") : string.Empty,
                                Admin4Code = dr["Admin4Code"] != DBNull.Value ? dr.Field<string>("Admin4Code") : string.Empty,
                                Population = dr["Population"] != DBNull.Value ? dr.Field<long?>("Population") : null,
                                Elevation = dr["Elevation"] != DBNull.Value ? dr.Field<int?>("Elevation") : null,
                                DEM = dr["DEM"] != DBNull.Value ? dr.Field<int?>("DEM") : null,
                                ModificationDate = dr["ModificationDate"] != DBNull.Value ? dr.Field<DateTime?>("ModificationDate") : null,
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetStates(string countryName = null, string isoCountryCode = null, int? pageNumber = null, int? pageSize = null)
        {
            string sql = SQLRepositoryHelper.GetStateInfo;
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
                                GeonameId = dr["GeonameId"] != DBNull.Value ? dr.Field<int?>("GeonameId") : null,
                                Name = dr["Name"] != DBNull.Value ? dr.Field<string>("Name") : string.Empty,
                                ASCIIName = dr["ASCIIName"] != DBNull.Value ? dr.Field<string>("ASCIIName") : string.Empty,
                                AlternateNames = dr["AlternateNames"] != DBNull.Value ? dr.Field<string>("AlternateNames") : string.Empty,
                                Latitude = dr["Latitude"] != DBNull.Value ? dr.Field<double?>("Latitude") : null,
                                Longitude = dr["Longitude"] != DBNull.Value ? dr.Field<double?>("Longitude") : null,
                                FeatureCodeId = dr["FeatureCodeId"] != DBNull.Value ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                CC2 = dr["CC2"] != DBNull.Value ? dr.Field<string>("CC2") : string.Empty,
                                Admin1Code = dr["Admin1Code"] != DBNull.Value ? dr.Field<string>("Admin1Code") : string.Empty,
                                Admin2Code = dr["Admin2Code"] != DBNull.Value ? dr.Field<string>("Admin2Code") : string.Empty,
                                Admin3Code = dr["Admin3Code"] != DBNull.Value ? dr.Field<string>("Admin3Code") : string.Empty,
                                Admin4Code = dr["Admin4Code"] != DBNull.Value ? dr.Field<string>("Admin4Code") : string.Empty,
                                Population = dr["Population"] != DBNull.Value ? dr.Field<long?>("Population") : null,
                                Elevation = dr["Elevation"] != DBNull.Value ? dr.Field<int?>("Elevation") : null,
                                DEM = dr["DEM"] != DBNull.Value ? dr.Field<int?>("DEM") : null,
                                ModificationDate = dr["ModificationDate"] != DBNull.Value ? dr.Field<DateTime?>("ModificationDate") : null,
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public RawData GetStateInfo(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null)
        {
            string sql = SQLRepositoryHelper.GetStateInfo;
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
                                GeonameId = dr["GeonameId"] != DBNull.Value ? dr.Field<int?>("GeonameId") : null,
                                Name = dr["Name"] != DBNull.Value ? dr.Field<string>("Name") : string.Empty,
                                ASCIIName = dr["ASCIIName"] != DBNull.Value ? dr.Field<string>("ASCIIName") : string.Empty,
                                AlternateNames = dr["AlternateNames"] != DBNull.Value ? dr.Field<string>("AlternateNames") : string.Empty,
                                Latitude = dr["Latitude"] != DBNull.Value ? dr.Field<double?>("Latitude") : null,
                                Longitude = dr["Longitude"] != DBNull.Value ? dr.Field<double?>("Longitude") : null,
                                FeatureCodeId = dr["FeatureCodeId"] != DBNull.Value ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                CC2 = dr["CC2"] != DBNull.Value ? dr.Field<string>("CC2") : string.Empty,
                                Admin1Code = dr["Admin1Code"] != DBNull.Value ? dr.Field<string>("Admin1Code") : string.Empty,
                                Admin2Code = dr["Admin2Code"] != DBNull.Value ? dr.Field<string>("Admin2Code") : string.Empty,
                                Admin3Code = dr["Admin3Code"] != DBNull.Value ? dr.Field<string>("Admin3Code") : string.Empty,
                                Admin4Code = dr["Admin4Code"] != DBNull.Value ? dr.Field<string>("Admin4Code") : string.Empty,
                                Population = dr["Population"] != DBNull.Value ? dr.Field<long?>("Population") : null,
                                Elevation = dr["Elevation"] != DBNull.Value ? dr.Field<int?>("Elevation") : null,
                                DEM = dr["DEM"] != DBNull.Value ? dr.Field<int?>("DEM") : null,
                                ModificationDate = dr["ModificationDate"] != DBNull.Value ? dr.Field<DateTime?>("ModificationDate") : null,
                                RowId = dr.Field<byte[]>("RowId")
                            };
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<RawData> GetCitiesInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null, int? pageSize = null, int? pageNumber = null)
        {
            string sql = SQLRepositoryHelper.GetCitiesInAState;
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
                                GeonameId = dr["GeonameId"] != DBNull.Value ? dr.Field<int?>("GeonameId") : null,
                                Name = dr["Name"] != DBNull.Value ? dr.Field<string>("Name") : string.Empty,
                                ASCIIName = dr["ASCIIName"] != DBNull.Value ? dr.Field<string>("ASCIIName") : string.Empty,
                                AlternateNames = dr["AlternateNames"] != DBNull.Value ? dr.Field<string>("AlternateNames") : string.Empty,
                                Latitude = dr["Latitude"] != DBNull.Value ? dr.Field<double?>("Latitude") : null,
                                Longitude = dr["Longitude"] != DBNull.Value ? dr.Field<double?>("Longitude") : null,
                                FeatureCodeId = dr["FeatureCodeId"] != DBNull.Value ? dr.Field<string>("FeatureCodeId") : string.Empty,
                                CC2 = dr["CC2"] != DBNull.Value ? dr.Field<string>("CC2") : string.Empty,
                                Admin1Code = dr["Admin1Code"] != DBNull.Value ? dr.Field<string>("Admin1Code") : string.Empty,
                                Admin2Code = dr["Admin2Code"] != DBNull.Value ? dr.Field<string>("Admin2Code") : string.Empty,
                                Admin3Code = dr["Admin3Code"] != DBNull.Value ? dr.Field<string>("Admin3Code") : string.Empty,
                                Admin4Code = dr["Admin4Code"] != DBNull.Value ? dr.Field<string>("Admin4Code") : string.Empty,
                                Population = dr["Population"] != DBNull.Value ? dr.Field<long?>("Population") : null,
                                Elevation = dr["Elevation"] != DBNull.Value ? dr.Field<int?>("Elevation") : null,
                                DEM = dr["DEM"] != DBNull.Value ? dr.Field<int?>("DEM") : null,
                                ModificationDate = dr["ModificationDate"] != DBNull.Value ? dr.Field<DateTime?>("ModificationDate") : null,
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public RawData GetCityInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null)
        {
            string sql = SQLRepositoryHelper.GetCitiesInAState;
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
                        result = new RawData()
                        {
                            GeonameId = dt.Rows[0]["GeonameId"] != DBNull.Value ? dt.Rows[0].Field<int?>("GeonameId") : null,
                            Name = dt.Rows[0]["Name"] != DBNull.Value ? dt.Rows[0].Field<string>("Name") : string.Empty,
                            ASCIIName = dt.Rows[0]["ASCIIName"] != DBNull.Value ? dt.Rows[0].Field<string>("ASCIIName") : string.Empty,
                            AlternateNames = dt.Rows[0]["AlternateNames"] != DBNull.Value ? dt.Rows[0].Field<string>("AlternateNames") : string.Empty,
                            Latitude = dt.Rows[0]["Latitude"] != DBNull.Value ? dt.Rows[0].Field<double?>("Latitude") : null,
                            Longitude = dt.Rows[0]["Longitude"] != DBNull.Value ? dt.Rows[0].Field<double?>("Longitude") : null,
                            FeatureCodeId = dt.Rows[0]["FeatureCodeId"] != DBNull.Value ? dt.Rows[0].Field<string>("FeatureCodeId") : string.Empty,
                            CC2 = dt.Rows[0]["CC2"] != DBNull.Value ? dt.Rows[0].Field<string>("CC2") : string.Empty,
                            Admin1Code = dt.Rows[0]["Admin1Code"] != DBNull.Value ? dt.Rows[0].Field<string>("Admin1Code") : string.Empty,
                            Admin2Code = dt.Rows[0]["Admin2Code"] != DBNull.Value ? dt.Rows[0].Field<string>("Admin2Code") : string.Empty,
                            Admin3Code = dt.Rows[0]["Admin3Code"] != DBNull.Value ? dt.Rows[0].Field<string>("Admin3Code") : string.Empty,
                            Admin4Code = dt.Rows[0]["Admin4Code"] != DBNull.Value ? dt.Rows[0].Field<string>("Admin4Code") : string.Empty,
                            Population = dt.Rows[0]["Population"] != DBNull.Value ? dt.Rows[0].Field<long?>("Population") : null,
                            Elevation = dt.Rows[0]["Elevation"] != DBNull.Value ? dt.Rows[0].Field<int?>("Elevation") : null,
                            DEM = dt.Rows[0]["DEM"] != DBNull.Value ? dt.Rows[0].Field<int?>("DEM") : null,
                            ModificationDate = dt.Rows[0]["ModificationDate"] != DBNull.Value ? dt.Rows[0].Field<DateTime?>("ModificationDate") : null,
                            RowId = dt.Rows[0].Field<byte[]>("RowId")
                        };
                    }
                }
            }

            return result;
        }

        public IEnumerable<Country> UpdateCountries(IEnumerable<Upd_VM.Country> countries)
        {
            string sql = SQLRepositoryHelper.UpdateCountries;
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
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr.Field<string>("ISO3Code"),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? null : dr.Field<int?>("ISONumeric"),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr.Field<string>("FIPSCode"),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr.Field<string>("CountryName"),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr.Field<string>("Capital"),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? null : dr.Field<double?>("SqKmArea"),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? null : dr.Field<long?>("TotalPopulation"),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr.Field<string>("ContinentCodeId"),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr.Field<string>("TopLevelDomain"),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyCode"),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyName"),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr.Field<string>("Phone"),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalFormat"),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalRegex"),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr.Field<string>("Languages"),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? null : dr.Field<int?>("GeonameId"),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr.Field<string>("Neighbors"),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr.Field<string>("EquivalentFipsCode"),
                                RowId = dr.Field<byte[]>("RowId"),
                            });
                        }

                    }
                }
            }

            return result;
        }

        public IEnumerable<Country> InsertCountries(IEnumerable<Ins_VM.Country> countries)
        {
            string sql = SQLRepositoryHelper.InsertCountries;
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
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
                                ISO3Code = dr["ISO3Code"] == DBNull.Value ? string.Empty : dr.Field<string>("ISO3Code"),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? null : dr.Field<int?>("ISONumeric"),
                                FIPSCode = dr["FIPSCode"] == DBNull.Value ? string.Empty : dr.Field<string>("FIPSCode"),
                                CountryName = dr["CountryName"] == DBNull.Value ? string.Empty : dr.Field<string>("CountryName"),
                                Capital = dr["Capital"] == DBNull.Value ? string.Empty : dr.Field<string>("Capital"),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? null : dr.Field<double?>("SqKmArea"),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? null : dr.Field<long?>("TotalPopulation"),
                                ContinentCodeId = dr["ContinentCodeId"] == DBNull.Value ? string.Empty : dr.Field<string>("ContinentCodeId"),
                                TopLevelDomain = dr["TopLevelDomain"] == DBNull.Value ? string.Empty : dr.Field<string>("TopLevelDomain"),
                                CurrencyCode = dr["CurrencyCode"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyCode"),
                                CurrencyName = dr["CurrencyName"] == DBNull.Value ? string.Empty : dr.Field<string>("CurrencyName"),
                                Phone = dr["Phone"] == DBNull.Value ? string.Empty : dr.Field<string>("Phone"),
                                PostalFormat = dr["PostalFormat"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalFormat"),
                                PostalRegex = dr["PostalRegex"] == DBNull.Value ? string.Empty : dr.Field<string>("PostalRegex"),
                                Languages = dr["Languages"] == DBNull.Value ? string.Empty : dr.Field<string>("Languages"),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? null : dr.Field<int?>("GeonameId"),
                                Neighbors = dr["Neighbors"] == DBNull.Value ? string.Empty : dr.Field<string>("Neighbors"),
                                EquivalentFipsCode = dr["EquivalentFipsCode"] == DBNull.Value ? string.Empty : dr.Field<string>("EquivalentFipsCode"),
                                RowId = dr.Field<byte[]>("RowId"),
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