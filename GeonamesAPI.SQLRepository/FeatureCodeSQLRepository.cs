using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using Microsoft.Extensions.Configuration;

namespace GeonamesAPI.SQLRepository
{
    public class FeatureCodeSQLRepository : IFeatureCode
    {
        private readonly sqlRepositoryHelper sqlRepositoryHelper;

        public FeatureCodeSQLRepository(IConfiguration configuration)
        {
            DBDataHelper.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            sqlRepositoryHelper = new sqlRepositoryHelper(configuration);
        }

        public IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize)
        {
            string sql = sqlRepositoryHelper.GetFeatureCodeInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("FeatureCodeId", featureCodeId));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<FeatureCode> result = new List<FeatureCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new FeatureCode()
                            {
                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                Description = dr["Description"] != null ? dr["Description"].ToString() : string.Empty,
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<FeatureCode> UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes)
        {
            string sql = sqlRepositoryHelper.UpdateFeatureCodes;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable featureCodesInputTable = new DataTable("FeatureCode_TVP");
            featureCodesInputTable.Columns.Add("FeatureCodeId");
            featureCodesInputTable.Columns.Add("FeatureCodeName");
            featureCodesInputTable.Columns.Add("Description");
            featureCodesInputTable.Columns.Add("RowId", typeof(byte[]));

            foreach (Upd_VM.FeatureCode featureCode in featureCodes)
            {
                featureCodesInputTable.Rows.Add(new object[]
                                {
                                    featureCode.FeatureCodeId,
                                    featureCode.FeatureCodeName,
                                    featureCode.Description,
                                    featureCode.RowId
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", featureCodesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<FeatureCode> result = new List<FeatureCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (featureCodesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in featureCodesOutputTable.Rows)
                        {
                            result.Add(new FeatureCode()
                            {
                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                Description = dr["Description"] != null ? dr["Description"].ToString() : string.Empty,
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public IEnumerable<FeatureCode> InsertFeatureCodes(IEnumerable<Ins_VM.FeatureCode> featureCodes)
        {
            string sql = sqlRepositoryHelper.InsertFeatureCodes;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable featureCodesInputTable = new DataTable("FeatureCode_TVP");
            featureCodesInputTable.Columns.Add("FeatureCodeId");
            featureCodesInputTable.Columns.Add("FeatureCodeName");
            featureCodesInputTable.Columns.Add("Description");

            foreach (Ins_VM.FeatureCode featureCode in featureCodes)
            {
                featureCodesInputTable.Rows.Add(new object[]
                                {
                                    featureCode.FeatureCodeId,
                                    featureCode.FeatureCodeName,
                                    featureCode.Description
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", featureCodesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<FeatureCode> result = new List<FeatureCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (featureCodesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in featureCodesOutputTable.Rows)
                        {
                            result.Add(new FeatureCode()
                            {
                                FeatureCodeId = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                FeatureCodeName = dr["FeatureCodeId"] != null ? dr["FeatureCodeId"].ToString() : string.Empty,
                                Description = dr["Description"] != null ? dr["Description"].ToString() : string.Empty,
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public int DeleteFeatureCode(string featureCodeId)
        {
            string sql = sqlRepositoryHelper.DeleteFeatureCode;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("Input", featureCodeId));

            int result = 0;

            using (DBDataHelper helper = new DBDataHelper())
            {
                result = helper.GetRowsAffected(sql, SQLTextType.Stored_Proc, parameterCollection);
            }

            return result;
        }
    }
}