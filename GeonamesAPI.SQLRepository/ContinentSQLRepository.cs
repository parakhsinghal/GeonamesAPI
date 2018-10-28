using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using System;

namespace GeonamesAPI.SQLRepository
{
    public class ContinentSQLRepository : IContinentRepository
    {
        public ContinentSQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<Continent> GetContinentInfo(string continentCodeId = null, int? geonameId = null, string continentName = null)
        {
            string sql = SQLRepositoryHelper.GetContinentInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ContinentCodeId", continentCodeId));
            parameterCollection.Add(new SqlParameter("GeonameId", geonameId));
            parameterCollection.Add(new SqlParameter("Continent", continentName));

            List<Continent> result = new List<Continent>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new Continent()
                            {
                                ContinentCodeId = //dr.Field<string>("ContinentCodeId"),
                                ContinentName = dr.Field<string>("Continent"),
                                GeonameId = dr.Field<int>("GeonameId"),
                                ASCIIName = dr.Field<string>("ASCIIName"),
                                AlternateNames = dr.Field<string>("AlternateNames"),
                                Latitude = dr.Field<double?>("Latitude"),
                                Longitude = dr.Field<double?>("Longitude"),
                                FeatureCategoryId = dr.Field<string>("FeatureCategoryId"),
                                FeatureCodeId = dr.Field<string>("FeatureCodeId"),
                                TimeZoneId = dr.Field<string>("TimeZoneId"),
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Country> GetCountriesInAContinent(string continentName = null, string continentCodeId = null, int? geonameId = null,
int? pageNumber = null, int? pageSize = null)
        {
            string sql = SQLRepositoryHelper.GetCountriesInAContinent;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ContinentName", continentName));
            parameterCollection.Add(new SqlParameter("ContinentCodeId", continentCodeId));
            parameterCollection.Add(new SqlParameter("GeonameId", geonameId));
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
                                ISOCountryCode = dr.Field<string>("ISOCountryCode"),
                                ISO3Code = dr.Field<string>("ISO3Code"),
                                ISONumeric = dr.Field<int?>("ISONumeric"),
                                FIPSCode = dr.Field<string>("FIPSCode"),
                                CountryName = dr.Field<string>("CountryName"),
                                Capital = dr.Field<string>("Capital"),
                                SqKmArea = dr.Field<double?>("SqKmArea"),
                                TotalPopulation = dr.Field<long?>("TotalPopulation"),
                                ContinentCodeId = dr.Field<string>("ContinentCodeId"),
                                TopLevelDomain = dr.Field<string>("TopLevelDomain"),
                                CurrencyCode = dr.Field<string>("CurrencyCode"),
                                CurrencyName = dr.Field<string>("CurrencyName"),
                                Phone = dr.Field<string>("Phone"),
                                PostalFormat = dr.Field<string>("PostalFormat"),
                                PostalRegex = dr.Field<string>("PostalRegex"),
                                Languages = dr.Field<string>("Languages"),
                                GeonameId = dr.Field<int?>("GeonameId"),
                                Neighbors = dr.Field<string>("Neighbors"),
                                EquivalentFipsCode = dr.Field<string>("EquivalentFipsCode"),
                                RowId = dr.Field<byte[]>("RowId"),
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<Continent> UpdateContinents(IEnumerable<Upd_VM.Continent> continents)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Continent> InsertContinents(IEnumerable<Ins_VM.Continent> continents)
        {
            throw new NotImplementedException();
        }

        public int DeleteContinent(string continentCodeId)
        {
            throw new NotImplementedException();
        }
    }
}