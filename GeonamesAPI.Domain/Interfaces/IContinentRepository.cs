using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;
using System.Collections.Generic;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface IContinentRepository
    {
        IEnumerable<Continent> GetContinentInfo(string continentCodeId = null, int? geonameId = null, string continentName = null);

        IEnumerable<Country> GetCountriesInAContinent(string continentName = null, string continentCodeId = null, int? geonameId = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<Continent> UpdateContinents(IEnumerable<Upd_VM.Continent> continents);

        IEnumerable<Continent> InsertContinents(IEnumerable<Ins_VM.Continent> continents);

        int DeleteContinent(string continentCodeId);
    }
}
