using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetAllCountries(int? pageSize = null, int? pageNumber = null);

        Country GetCountryInfo(string isoCountryCode = null, string countryName = null);

        IEnumerable<RawData> GetCountryFeatureCategoryFeatureCode(string featureCategoryId, string isoCountryCode = null, string countryName = null, string featureCodeId = null, int? pageSize = null, int? pageNumber = null);

        IEnumerable<RawData> GetStates(string countryName = null, string isoCountryCode = null, int? pageNumber = null, int? pageSize = null);

        RawData GetStateInfo(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null);

        RawData GetCityInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null);

        IEnumerable<RawData> GetCitiesInState(string countryName = null, string isoCountryCode = null, string stateName = null, int? stateGeonameId = null, int? cityGeonameId = null, string cityName = null, int? pageSize = null, int? pageNumber = null);

        IEnumerable<Country> UpdateCountries(IEnumerable<Upd_VM.Country> countries);

        IEnumerable<Country> InsertCountries(IEnumerable<Ins_VM.Country> countries);

        int DeleteCountry(string isoCountryCode);
    }
}
