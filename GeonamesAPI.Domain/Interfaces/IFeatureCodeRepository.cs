using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface IFeatureCodeRepository
    {
        IEnumerable<FeatureCode> GetFeatureCodes(string featureCodeId, int? pageNumber, int? pageSize);

        IEnumerable<FeatureCode> UpdateFeatureCodes(IEnumerable<Upd_VM.FeatureCode> featureCodes);

        IEnumerable<FeatureCode> InsertFeatureCodes(IEnumerable<Ins_VM.FeatureCode> featureCodes);

        int DeleteFeatureCode(string featureCodeId);
    }
}
