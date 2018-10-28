using GeonamesAPI.DALHelper;
using GeonamesAPI.Domain;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository.Helper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.SQLRepository
{
    public class LanguageCodeSQLRepository : ILanguageCodeRepository
    {
        public LanguageCodeSQLRepository()
        {
            DBDataHelper.ConnectionString = ConfigurationManager.ConnectionStrings["Geonames"].ConnectionString;
        }

        public IEnumerable<LanguageCode> GetLanguageInfo(string iso6393Code = null, string language = null, int? pageNumber = null, int? pageSize = null)
        {
            string sql = SQLRepositoryHelper.GetLanguageInfo;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("ISO6393Code", iso6393Code));
            parameterCollection.Add(new SqlParameter("Language", language));
            parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
            parameterCollection.Add(new SqlParameter("PageSize", pageSize));

            List<LanguageCode> result = new List<LanguageCode>();
            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            result.Add(new LanguageCode()
                            {
                                ISO6391 = dr.Field<string>("ISO6391"),
                                ISO6392 = dr.Field<string>("ISO6392"),
                                ISO6393 = dr.Field<string>("ISO6393"),
                                Language = dr.Field<string>("Language"),
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }
                    }
                }
            }

            return result;
        }

        public IEnumerable<LanguageCode> UpdateLanguages(IEnumerable<Upd_VM.LanguageCode> languageCodes)
        {
            string sql = SQLRepositoryHelper.UpdateLanguages;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable languageCodesInputTable = new DataTable("LanguageCode_TVP");
            languageCodesInputTable.Columns.Add("ISO6393");
            languageCodesInputTable.Columns.Add("ISO6392");
            languageCodesInputTable.Columns.Add("ISO6391");
            languageCodesInputTable.Columns.Add("Language");
            languageCodesInputTable.Columns.Add("RowId", typeof(byte[]));

            foreach (Upd_VM.LanguageCode languageCode in languageCodes)
            {
                languageCodesInputTable.Rows.Add(new object[]
                                { 
                                   languageCode.ISO6393,
                                   languageCode.ISO6392,
                                   languageCode.ISO6391,
                                   languageCode.Language,
                                   languageCode.RowId
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", languageCodesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<LanguageCode> result = new List<LanguageCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (featureCodesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in featureCodesOutputTable.Rows)
                        {
                            result.Add(new LanguageCode()
                            {
                                ISO6391 = dr.Field<string>("ISO6391"),
                                ISO6392 = dr.Field<string>("ISO6392"),
                                ISO6393 = dr.Field<string>("ISO6393"),
                                Language = dr.Field<string>("Language"),
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }

                    }
                }
            }

            return result;
        }

        public IEnumerable<LanguageCode> InsertLanguages(IEnumerable<Ins_VM.LanguageCode> languageCodes)
        {
            string sql = SQLRepositoryHelper.InsertLanguages;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable languageCodesInputTable = new DataTable("LanguageCode_TVP");
            languageCodesInputTable.Columns.Add("ISO6393");
            languageCodesInputTable.Columns.Add("ISO6392");
            languageCodesInputTable.Columns.Add("ISO6391");
            languageCodesInputTable.Columns.Add("Language");
            languageCodesInputTable.Columns.Add("RowId", typeof(byte[]));

            foreach (Ins_VM.LanguageCode languageCode in languageCodes)
            {
                languageCodesInputTable.Rows.Add(new object[]
                                { 
                                   languageCode.ISO6393,
                                   languageCode.ISO6392,
                                   languageCode.ISO6391,
                                   languageCode.Language,
                                   DBNull.Value
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", languageCodesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<LanguageCode> result = new List<LanguageCode>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable featureCodesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (featureCodesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in featureCodesOutputTable.Rows)
                        {
                            result.Add(new LanguageCode()
                            {
                                ISO6391 = dr.Field<string>("ISO6391"),
                                ISO6392 = dr.Field<string>("ISO6392"),
                                ISO6393 = dr.Field<string>("ISO6393"),
                                Language = dr.Field<string>("Language"),
                                RowId = dr.Field<byte[]>("RowId")
                            });
                        }

                    }
                }
            }

            return result;
        }

        public int DeleteLanguageCode(string iso6393Code)
        {
            string sql = SQLRepositoryHelper.DeleteLanuage;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("Input", iso6393Code));

            int result = 0;

            using (DBDataHelper helper = new DBDataHelper())
            {
                result = helper.GetRowsAffected(sql, SQLTextType.Stored_Proc, parameterCollection);
            }

            return result;
        }
    }
}