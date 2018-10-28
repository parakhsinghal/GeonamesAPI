using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface ILanguageCodeRepository
    {
        IEnumerable<LanguageCode> GetLanguageInfo(string iso6393Code = null, string language = null, int? pageNumber = null, int? pageSize = null);

        IEnumerable<LanguageCode> UpdateLanguages(IEnumerable<Upd_VM.LanguageCode> languageCodes);

        IEnumerable<LanguageCode> InsertLanguages (IEnumerable<Ins_VM.LanguageCode> languageCodes);

        int DeleteLanguageCode(string iso6393Code);
    }
}
