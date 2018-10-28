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
using System.Text;

namespace GeonamesAPI.SQLRepository
{
    public class FeatureCategorySQLRepository : IFeatureCategoryRepository
    {
        public FeatureCategorySQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<FeatureCategory> GetFeatureCategories(string featureCategoryId)
        {
            string sql = SQLRepositoryHelper.GetFeatureCategoryInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("FeatureCategoryId", featureCategoryId));

            List<FeatureCategory> result = new List<FeatureCategory>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new FeatureCategory()
                            {
                                FeatureCategoryId = dr["FeatureCategoryId"] != null ? dr.Field<string>("Featurecategoryid") : string.Empty,
                                FeatureCategoryName = dr["FeatureCategoryName"] != null ? dr.Field<string>("FeatureCategoryName") : string.Empty,
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<FeatureCategory> UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featureCategories)
        {
            try
            {
                string sql = SQLRepositoryHelper.UpdateFeatureCategories;
                List<SqlParameter> parameterCollection = new List<SqlParameter>();

                DataTable featureCategoriesInputTable = new DataTable("FeatureCategory_TVP");
                featureCategoriesInputTable.Columns.Add("FeatureCategoryId");
                featureCategoriesInputTable.Columns.Add("FeatureCategoryName");
                featureCategoriesInputTable.Columns.Add("RowId", typeof(byte[]));

                foreach (Upd_VM.FeatureCategory featureCategory in featureCategories)
                {
                    featureCategoriesInputTable.Rows.Add(new object[]
                                { 
                                    featureCategory.FeatureCategoryId,                                    
                                    featureCategory.FeatureCategoryName,
                                    featureCategory.RowId
                                });
                }

                SqlParameter inputData = new SqlParameter("Input", featureCategoriesInputTable);
                inputData.SqlDbType = SqlDbType.Structured;
                parameterCollection.Add(inputData);

                List<FeatureCategory> result = new List<FeatureCategory>();

                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable featureCategoriesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                    {
                        if (featureCategoriesOutputTable.Rows.Count > 0)
                        {
                            foreach (DataRow dr in featureCategoriesOutputTable.Rows)
                            {
                                result.Add(new FeatureCategory()
                                {
                                    FeatureCategoryId = dr["FeatureCategoryId"] != null ? dr.Field<string>("Featurecategoryid") : string.Empty,
                                    FeatureCategoryName = dr["FeatureCategoryName"] != null ? dr.Field<string>("FeatureCategoryName") : string.Empty,
                                    RowId = dr.Field<byte[]>("RowId")
                                });
                            }

                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public IEnumerable<FeatureCategory> InsertFeatureCategories(IEnumerable<Ins_VM.FeatureCategory> featureCategories)
        {
            string sql = SQLRepositoryHelper.InsertFeatureCategories;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable featureCategoriesInputTable = new DataTable("FeatureCategory_TVP");
            featureCategoriesInputTable.Columns.Add("FeatureCategoryId");
            featureCategoriesInputTable.Columns.Add("FeatureCategoryName");

            foreach (Ins_VM.FeatureCategory featureCategory in featureCategories)
            {
                featureCategoriesInputTable.Rows.Add(new object[]
                                { 
                                    featureCategory.FeatureCategoryId,                                    
                                    featureCategory.FeatureCategoryName
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", featureCategoriesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<FeatureCategory> result = new List<FeatureCategory>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable featureCategoriesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (featureCategoriesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in featureCategoriesOutputTable.Rows)
                        {
                            result.Add(new FeatureCategory()
                            {
                                FeatureCategoryId = dr["FeatureCategoryId"] != null ? dr.Field<string>("Featurecategoryid") : string.Empty,
                                FeatureCategoryName = dr["FeatureCategoryName"] != null ? dr.Field<string>("FeatureCategoryName") : string.Empty,
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }

                    }
                }
            }

            return result;
        }

        public int DeleteFeatureCategory(string featureCategoryId)
        {
            string sql = SQLRepositoryHelper.DeleteFeatureCategory;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("Input", featureCategoryId));

            int result = 0;

            using (DBDataHelper helper = new DBDataHelper())
            {
                result = helper.GetRowsAffected(sql, SQLTextType.Stored_Proc, parameterCollection);
            }

            return result;
        }
    }
}