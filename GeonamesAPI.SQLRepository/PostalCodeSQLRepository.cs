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
    public class RawPostalSQLRepository : IPostalCodeRepository
    {
        private readonly sqlRepositoryHelper sqlRepositoryHelper;

        public RawPostalSQLRepository(IConfiguration configuration)
        {
            DBDataHelper.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            sqlRepositoryHelper = new sqlRepositoryHelper(configuration);
        }

        public IEnumerable<Domain.RawPostal> GetPostalInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null)
        {
            string sql = sqlRepositoryHelper.GetPostalCodeInfo;

            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
            parameterCollection.Add(new SqlParameter("CountryName", countryName));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));
            parameterCollection.Add(new SqlParameter("PostalCode", postalCode));

            List<RawPostal> result = new List<RawPostal>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new RawPostal()
                            {
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                PostalCode = dr["PostalCode"] == DBNull.Value ? string.Empty : dr["PostalCode"].ToString(),
                                PlaceName = dr["PlaceName"] == DBNull.Value ? string.Empty : dr["PlaceName"].ToString(),
                                Admin1Name = dr["Admin1Name"] == DBNull.Value ? string.Empty : dr["Admin1Name"].ToString(),
                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr["Admin1Code"].ToString(),
                                Admin2Name = dr["Admin2Name"] == DBNull.Value ? string.Empty : dr["Admin2Name"].ToString(),
                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr["Admin2Code"].ToString(),
                                Admin3Name = dr["Admin3Name"] == DBNull.Value ? string.Empty : dr["Admin3Name"].ToString(),
                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr["Admin3Code"].ToString(),
                                Latitude = dr["Latitude"] == DBNull.Value ? 0.0 : double.Parse(dr["Latitude"].ToString()),
                                Longitude = dr["Longitude"] == DBNull.Value ? 0.0 : double.Parse(dr["Longitude"].ToString()),
                                Accuracy = dr["Accuracy"] == DBNull.Value ? 0 : int.Parse(dr["Accuracy"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }


        public IEnumerable<RawPostal> UpdatePostalInfo(IEnumerable<Upd_VM.RawPostal> postalInfo)
        {            
            throw new NotImplementedException();
        }

        public IEnumerable<RawPostal> InsertPostalInfo(IEnumerable<Domain.ViewModels.Insert.RawPostal> postalInfo)
        {
            throw new NotImplementedException();
        }

        public int DeletePostalInfo(RawPostal postalInfo)
        {
            throw new NotImplementedException();
        }
    }
}
