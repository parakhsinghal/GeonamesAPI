using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{

    public class TimeZone : IVersionable
    {
        public string TimeZoneId { get; set; }
        public string ISOCountryCode { get; set; }        
        public decimal? GMT { get; set; }
        public decimal? DST { get; set; }
        public decimal? RawOffset { get; set; }
        public byte[] RowId { get; set; }
    }
}
