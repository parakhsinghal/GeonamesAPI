using System;
using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class RawData : IVersionable
    {
        public int? GeonameId { get; set; }
        public string Name { get; set; }
        public string ASCIIName { get; set; }
        public string AlternateNames { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string FeatureCodeId { get; set; }
        public string CC2 { get; set; }
        public string Admin1Code { get; set; }
        public string Admin2Code { get; set; }
        public string Admin3Code { get; set; }
        public string Admin4Code { get; set; }
        public long? Population { get; set; }
        public int? Elevation { get; set; }
        public int? DEM { get; set; }
        public DateTime? ModificationDate { get; set; }
        public byte[] RowId { get; set; }
    }
}
