using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface IPostalCodeRepository
    {
        IEnumerable<RawPostal> GetPostalInfo(string isoCountryCode = null, string countryName = null, string postalCode = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<RawPostal> UpdatePostalInfo(IEnumerable<Upd_VM.RawPostal> postalInfo);

        IEnumerable<RawPostal> InsertPostalInfo(IEnumerable<Ins_VM.RawPostal> postalInfo);

        int DeletePostalInfo(RawPostal postalInfo);
    }
}
