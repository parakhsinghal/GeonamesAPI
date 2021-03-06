﻿using GeonamesAPI.DALHelper;
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
    public class TimeZoneSQLRepository : ITimeZoneRepository
    {
        private readonly sqlRepositoryHelper sqlRepositoryHelper;

        public TimeZoneSQLRepository(IConfiguration configuration)
        {
            DBDataHelper.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            sqlRepositoryHelper = new sqlRepositoryHelper(configuration);
        }

        public IEnumerable<string> GetDistinctTimeZones()
        {
            try
            {
                string sql = sqlRepositoryHelper.GetDistinctTimeZones;
                List<string> result = new List<string>();

                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection: null))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(dr["TimeZoneId"].ToString());
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Domain.TimeZone> GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null)
        {
            try
            {
                string sql = sqlRepositoryHelper.GetTimeZoneDetails;
                List<SqlParameter> parameterCollection = new List<SqlParameter>();
                parameterCollection.Add(new SqlParameter("TimeZoneId", timeZoneId));
                parameterCollection.Add(new SqlParameter("ISOCountryCode", isoCountryCode));
                parameterCollection.Add(new SqlParameter("ISO3Code", iso3Code));
                parameterCollection.Add(new SqlParameter("ISONumeric", isoNumeric));
                parameterCollection.Add(new SqlParameter("CountryName", countryName));
                parameterCollection.Add(new SqlParameter("Latitude", latitude));
                parameterCollection.Add(new SqlParameter("Longitude", longitude));
                parameterCollection.Add(new SqlParameter("PageNumber", pageNumber));
                parameterCollection.Add(new SqlParameter("PageSize", pageSize));

                List<Domain.TimeZone> result = new List<Domain.TimeZone>();
                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(new Domain.TimeZone()
                                {
                                    TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr["TimeZoneId"].ToString(),
                                    ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                    GMT = dr["GMT"] == DBNull.Value ? 0 : decimal.Parse(dr["GMT"].ToString()),
                                    DST = dr["DST"] == DBNull.Value ? 0 : decimal.Parse(dr["DST"].ToString()),
                                    RawOffset = dr["RawOffset"] == DBNull.Value ? 0 : decimal.Parse(dr["RawOffset"].ToString()),
                                    RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                                });
                            }
                        }
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Domain.TimeZone> GetTimeZoneDetailsByPlaceName(string placeName)
        {

            try
            {
                string sql = sqlRepositoryHelper.GetTimeZoneDetailsByPlaceName;
                List<SqlParameter> parameterCollection = new List<SqlParameter>();
                parameterCollection.Add(new SqlParameter("PlaceName", placeName));

                List<Domain.TimeZone> result = new List<Domain.TimeZone>();
                using (DBDataHelper helper = new DBDataHelper())
                {
                    using (DataTable dt = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                result.Add(new Domain.TimeZone()
                                {
                                    TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr["TimeZoneId"].ToString(),
                                    ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                    GMT = dr["GMT"] == DBNull.Value ? 0 : decimal.Parse(dr["GMT"].ToString()),
                                    DST = dr["DST"] == DBNull.Value ? 0 : decimal.Parse(dr["DST"].ToString()),
                                    RawOffset = dr["RawOffset"] == DBNull.Value ? 0 : decimal.Parse(dr["RawOffset"].ToString()),
                                    RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
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

        public IEnumerable<GeonamesAPI.Domain.TimeZone> UpdateTimeZones(IEnumerable<Upd_VM.TimeZone> timeZones)
        {
            string sql = sqlRepositoryHelper.UpdateTimeZones;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable timeZonesInputTable = new DataTable("TimeZone_TVP");
            timeZonesInputTable.Columns.Add("ISOCountryCode");
            timeZonesInputTable.Columns.Add("TimeZoneId");
            timeZonesInputTable.Columns.Add("GMT");
            timeZonesInputTable.Columns.Add("DST");
            timeZonesInputTable.Columns.Add("RawOffset");
            timeZonesInputTable.Columns.Add("RowId", typeof(byte[]));

            foreach (Upd_VM.TimeZone timeZone in timeZones)
            {
                timeZonesInputTable.Rows.Add(new object[]
                                {
                                   timeZone.ISOCountryCode,
                                   timeZone.TimeZoneId,
                                   timeZone.GMT,
                                   timeZone.DST,
                                   timeZone.RawOffset,
                                   timeZone.RowId
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", timeZonesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<Domain.TimeZone> result = new List<Domain.TimeZone>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable timeZonesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (timeZonesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in timeZonesOutputTable.Rows)
                        {
                            result.Add(new Domain.TimeZone()
                            {
                                TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr["TimeZoneId"].ToString(),
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                GMT = dr["GMT"] == DBNull.Value ? 0 : decimal.Parse(dr["GMT"].ToString()),
                                DST = dr["DST"] == DBNull.Value ? 0 : decimal.Parse(dr["DST"].ToString()),
                                RawOffset = dr["RawOffset"] == DBNull.Value ? 0 : decimal.Parse(dr["RawOffset"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public IEnumerable<GeonamesAPI.Domain.TimeZone> InsertTimeZones(IEnumerable<Ins_VM.TimeZone> timeZones)
        {
            string sql = sqlRepositoryHelper.InsertTimeZones;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();

            DataTable timeZonesInputTable = new DataTable("TimeZone_TVP");
            timeZonesInputTable.Columns.Add("ISOCountryCode");
            timeZonesInputTable.Columns.Add("TimeZoneId");
            timeZonesInputTable.Columns.Add("GMT");
            timeZonesInputTable.Columns.Add("DST");
            timeZonesInputTable.Columns.Add("RawOffset");

            foreach (Ins_VM.TimeZone timeZone in timeZones)
            {
                timeZonesInputTable.Rows.Add(new object[]
                                {
                                   timeZone.ISOCountryCode,
                                   timeZone.TimeZoneId,
                                   timeZone.GMT,
                                   timeZone.DST,
                                   timeZone.RawOffset
                                });
            }

            SqlParameter inputData = new SqlParameter("Input", timeZonesInputTable);
            inputData.SqlDbType = SqlDbType.Structured;
            parameterCollection.Add(inputData);

            List<Domain.TimeZone> result = new List<Domain.TimeZone>();

            using (DBDataHelper helper = new DBDataHelper())
            {
                using (DataTable timeZonesOutputTable = helper.GetDataTable(sql, SQLTextType.Stored_Proc, parameterCollection))
                {
                    if (timeZonesOutputTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in timeZonesOutputTable.Rows)
                        {
                            result.Add(new Domain.TimeZone()
                            {
                                TimeZoneId = dr["TimeZoneId"] == DBNull.Value ? string.Empty : dr["TimeZoneId"].ToString(),
                                ISOCountryCode = dr["ISOCountryCode"] == DBNull.Value ? string.Empty : dr["ISOCountryCode"].ToString(),
                                GMT = dr["GMT"] == DBNull.Value ? 0 : decimal.Parse(dr["GMT"].ToString()),
                                DST = dr["DST"] == DBNull.Value ? 0 : decimal.Parse(dr["DST"].ToString()),
                                RawOffset = dr["RawOffset"] == DBNull.Value ? 0 : decimal.Parse(dr["RawOffset"].ToString()),
                                RowId = System.Text.Encoding.UTF32.GetBytes(dr["RowId"].ToString())
                            });
                        }

                    }
                }
            }

            return result;
        }

        public int DeleteTimeZone(string timeZoneId)
        {
            string sql = sqlRepositoryHelper.DeleteTimeZone;
            List<SqlParameter> parameterCollection = new List<SqlParameter>();
            parameterCollection.Add(new SqlParameter("Input", timeZoneId));

            int result = 0;

            using (DBDataHelper helper = new DBDataHelper())
            {
                result = helper.GetRowsAffected(sql, SQLTextType.Stored_Proc, parameterCollection);
            }

            return result;
        }
    }
}