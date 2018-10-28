using System;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class Continent : IVersionable
    {
        public string ContinentCodeId { get; set; }
        public string ContinentName { get; set; }
        public int GeonameId { get; set; }
        public string ASCIIName { get; set; }
        public string AlternateNames { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FeatureCategoryId { get; set; }
        public string FeatureCodeId { get; set; }
        public string TimeZoneId { get; set; }
        public byte[] RowId { get;  set; }
        public string RowVersion { get { return Convert.ToBase64String(RowId); } }
    }
}
