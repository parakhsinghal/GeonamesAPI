//using GeonamesAPI.DALHelper;
//using GeonamesAPI.Domain;
//using GeonamesAPI.Domain.Interfaces;
//using GeonamesAPI.SQLRepository.Helper;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
//using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
//using System;

//namespace GeonamesAPI.SQLRepository
//{
//    public class FeatureCodeSQLRepository : IFeatureCode
//    {
//        public FeatureCodeSQLRepository()
//        {
//            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
//        }

//        public IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize)
//        {
//            string sql = SQLRepositoryHelper.GetFeatureCodeInfo;
//            List<SqlParameter> parameterCollection = new List<SqlParameter>();
//            parameterCollection.Add(new SqlParameter("FeatureCodeId", featureCodeId));
//            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
//            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

//            List<FeatureCode> result = new List<FeatureCode>();

//            using (DBDataHelper helper = new DBDataHelper())
//            {
//                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in dt.Rows)
//                        {
//                            result.Add(new FeatureCode()
//                            {
//                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                Description = dr["Description"] != null ? dr.Field<string>("Description") : string.Empty,
//                                RowId = dr.Field<byte[]>("RowId")
//                            });
//                        }
//                    }
//                }
//            }

//            return result;
//        }

//        public IEnumerable<FeatureCode> UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes)
//        {
//            string sql = SQLRepositoryHelper.UpdateFeatureCodes;
//            List<SqlParameter> parameterCollection = new List<SqlParameter>();

//            DataTable featureCodesInputTable = new DataTable("FeatureCode_TVP");
//            featureCodesInputTable.Columns.Add("FeatureCodeId");
//            featureCodesInputTable.Columns.Add("FeatureCodeName");
//            featureCodesInputTable.Columns.Add("Description");
//            featureCodesInputTable.Columns.Add("RowId", typeof(byte[]));

//            foreach (Upd_VM.FeatureCode featureCode in featureCodes)
//            {
//                featureCodesInputTable.Rows.Add(new object[]
//                                { 
//                                    featureCode.FeatureCodeId,
//                                    featureCode.FeatureCodeName,
//                                    featureCode.Description,
//                                    featureCode.RowId
//                                });
//            }

//            SqlParameter inputData = new SqlParameter("Input", featureCodesInputTable);
//            inputData.SqlDbType = SqlDbType.Structured;
//            parameterCollection.Add(inputData);

//            List<FeatureCode> result = new List<FeatureCode>();

//            using (DBDataHelper helper = new DBDataHelper())
//            {
//                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
//                {
//                    if (featureCodesOutputTable.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in featureCodesOutputTable.Rows)
//                        {
//                            result.Add(new FeatureCode()
//                            {
//                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                Description = dr["Description"] != null ? dr.Field<string>("Description") : string.Empty,
//                                RowId = dr.Field<byte[]>("RowId")
//                            });
//                        }

//                    }
//                }
//            }

//            return result;
//        }

//        public IEnumerable<FeatureCode> InsertFeatureCodes(IEnumerable<Ins_VM.FeatureCode> featureCodes)
//        {
//            string sql = SQLRepositoryHelper.InsertFeatureCodes;
//            List<SqlParameter> parameterCollection = new List<SqlParameter>();

//            DataTable featureCodesInputTable = new DataTable("FeatureCode_TVP");
//            featureCodesInputTable.Columns.Add("FeatureCodeId");
//            featureCodesInputTable.Columns.Add("FeatureCodeName");
//            featureCodesInputTable.Columns.Add("Description");

//            foreach (Ins_VM.FeatureCode featureCode in featureCodes)
//            {
//                featureCodesInputTable.Rows.Add(new object[]
//                                { 
//                                    featureCode.FeatureCodeId,
//                                    featureCode.FeatureCodeName,
//                                    featureCode.Description
//                                });
//            }

//            SqlParameter inputData = new SqlParameter("Input", featureCodesInputTable);
//            inputData.SqlDbType = SqlDbType.Structured;
//            parameterCollection.Add(inputData);

//            List<FeatureCode> result = new List<FeatureCode>();

//            using (DBDataHelper helper = new DBDataHelper())
//            {
//                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
//                {
//                    if (featureCodesOutputTable.Rows.Count > 0)
//                    {
//                        foreach (DataRow dr in featureCodesOutputTable.Rows)
//                        {
//                            result.Add(new FeatureCode()
//                            {
//                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr.Field<string>("FeatureCodeId") : string.Empty,
//                                Description = dr["Description"] != null ? dr.Field<string>("Description") : string.Empty,
//                                RowId = dr.Field<byte[]>("RowId")
//                            });
//                        }

//                    }
//                }
//            }

//            return result;
//        }

//        public int DeleteFeatureCode(string featureCodeId)
//        {
//            string sql = SQLRepositoryHelper.DeleteFeatureCode;
//            List<SqlParameter> parameterCollection = new List<SqlParameter>();
//            parameterCollection.Add(new SqlParameter("Input", featureCodeId));

//            int result = 0;

//            using (DBDataHelper helper = new DBDataHelper())
//            {
//                result = helper.GetRowsAffected(sql, SQLTextType.Stored_Proc, parameterCollection);
//            }

//            return result;
//        }
//    }
//}