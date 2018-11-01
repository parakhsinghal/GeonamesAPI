//using GeonamesAPI.DALHelper;
//using GeonamesAPI.Domain;
//using GeonamesAPI.Domain.Interfaces;
//using GeonamesAPI.SQLRepository.Helper;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
//using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

//namespace GeonamesAPI.SQLRepository
//{
//    public class RawPostalSQLRepository : IRawPostalRepository
//    {
//        public RawPostalSQLRepository()
//        {
//            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
//        }

//        public IEnumerable<Domain.RawPostal> GetPostalInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null)
//        {
//            string sql = SQLRepositoryHelper.GetPostalCodeInfo;

//            List<SqlParameter> parameterCollection = new List<SqlParameter>();
//            parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
//            parameterCollection.Add(new SqlParameter("CountryName", countryName));
//            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
//            parameterCollection.Add(new SqlParameter("PageSize", pageSize));
//            parameterCollection.Add(new SqlParameter("PostalCode", postalCode));

//            List<RawPostal> result = new List<RawPostal>();

//            using (DBDataHelper helper = new DBDataHelper())
//            {
//                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in dt.Rows)
//                        {
//                            result.Add(new RawPostal()
//                            {
//                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr.Field<string>("ISOCountryCode"),
//                                PostalCode = dr["PostalCode"] == DBNull.Value?string.Empty:dr.Field<string>("PostalCode"),
//                                PlaceName = dr["PlaceName"] == DBNull.Value ? string.Empty : dr.Field<string>("PlaceName"),
//                                Admin1Name = dr["Admin1Name"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin1Name"),
//                                Admin1Code = dr["Admin1Code"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin1Code"),
//                                Admin2Name = dr["Admin2Name"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin2Name"),
//                                Admin2Code = dr["Admin2Code"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin2Code"),
//                                Admin3Name = dr["Admin3Name"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin3Name"),
//                                Admin3Code = dr["Admin3Code"] == DBNull.Value ? string.Empty : dr.Field<string>("Admin3Code"),
//                                Latitude = dr["Latitude"] == DBNull.Value ? null : dr.Field<double?>("Latitude"),
//                                Longitude = dr["Longitude"] == DBNull.Value ? null : dr.Field<double?>("Longitude"),
//                                Accuracy = dr["Accuracy"] == DBNull.Value ? null : dr.Field<int?>("Accuracy"),
//                                RowId = dr.Field<byte[]>("RowId"),
//                            });
//                        }
//                    }
//                }
//            }

//            return result;
//        }


//        public IEnumerable<RawPostal> UpdatePostalInfo(IEnumerable<Upd_VM.RawPostal> postalInfo)
//        {
//            //string sql = 
//            throw new NotImplementedException();
//        }

//        public IEnumerable<RawPostal> InsertPostalInfo(IEnumerable<Domain.ViewModels.Insert.RawPostal> postalInfo)
//        {
//            throw new NotImplementedException();
//        }

//        public int DeletePostalInfo(RawPostal postalInfo)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
