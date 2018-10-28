using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface ITimeZoneRepository
    {
        IEnumerable<string> GetDistinctTimeZones();

        IEnumerable<GeonamesAPI.Domain.TimeZone> GetTimeZoneDetails(string timeZoneId = null, string isoCountryCode = null, string iso3Code = null, int? isoNumeric = null, string countryName = null, double? latitude = null, double? longitude = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<GeonamesAPI.Domain.TimeZone> GetTimeZoneDetailsByPlaceName(string placeName);

        IEnumerable<GeonamesAPI.Domain.TimeZone> UpdateTimeZones (IEnumerable<Upd_VM.TimeZone> timeZones);

        IEnumerable<GeonamesAPI.Domain.TimeZone> InsertTimeZones (IEnumerable<Ins_VM.TimeZone> timeZones);

        int DeleteTimeZone(string timeZoneId);
    }
}
