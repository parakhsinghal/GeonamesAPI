using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using System;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace GeonamesAPI.SQLRepository
{
    public class ContinentSQLRepository : IContinentRepository
    {
        private readonly sqlRepositoryHelper sqlRepositoryHelper;

        public ContinentSQLRepository(IConfiguration configuration)
        {
            DBDataHelper.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            sqlRepositoryHelper = new sqlRepositoryHelper(configuration);
        }

        public IEnumerable<Continent> GetContinentInfo(string continentCodeId = null, int? geonameId = null, string continentName = null)
        {
            string sql = sqlRepositoryHelper.GetContinentInfo;
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
                                ContinentCodeId = dr["ContinentCodeId"].ToString(),
                                ContinentName = dr["Continent"].ToString(),
                                GeonameId = int.Parse(dr["GeonameId"].ToString()),
                                ASCIIName = dr["ASCIIName"].ToString(),
                                AlternateNames = dr["AlternateNames"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0f : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0f : double.Parse(dr["Longitude"].ToString()),
                                FeatureCategoryId = dr["FeatureCategoryId"].ToString(),
                                FeatureCodeId = dr["FeatureCodeId"].ToString(),
                                TimeZoneId = dr["TimeZoneId"].ToString(),
                                RowId = dr["RowId"] as byte[]
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
            string sql = sqlRepositoryHelper.GetCountriesInAContinent;
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
                                ISOCountryCode = dr["ISOCountryCode"].ToString(),
                                ISO3Code = dr["ISO3Code"].ToString(),
                                ISONumeric = dr["ISONumeric"] == DBNull.Value ? 0 : int.Parse(dr["ISONumeric"].ToString()),
                                FIPSCode = dr["FIPSCode"].ToString(),
                                CountryName = dr["CountryName"].ToString(),
                                Capital = dr["Capital"].ToString(),
                                SqKmArea = dr["SqKmArea"] == DBNull.Value ? 0f : double.Parse(dr["SqKmArea"].ToString()),
                                TotalPopulation = dr["TotalPopulation"] == DBNull.Value ? 0 : long.Parse(dr["TotalPopulation"].ToString()),
                                ContinentCodeId = dr["ContinentCodeId"].ToString(),
                                TopLevelDomain = dr["TopLevelDomain"].ToString(),
                                CurrencyCode = dr["CurrencyCode"].ToString(),
                                CurrencyName = dr["CurrencyName"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                PostalFormat = dr["PostalFormat"].ToString(),
                                PostalRegex = dr["PostalRegex"].ToString(),
                                Languages = dr["Languages"].ToString(),
                                GeonameId = dr["GeonameId"] == DBNull.Value ? 0 : int.Parse(dr["GeonameId"].ToString()),
                                Neighbors = dr["Neighbors"].ToString(),
                                EquivalentFipsCode = dr["EquivalentFipsCode"].ToString(),
                                RowId = dr["RowId"] as byte[]
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