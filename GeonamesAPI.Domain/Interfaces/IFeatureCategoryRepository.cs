using System.Collections.Generic;
using Upd_VM = GeonamesAPI.Domain.ViewModels.Update;
using Ins_VM = GeonamesAPI.Domain.ViewModels.Insert;

namespace GeonamesAPI.Domain.Interfaces
{
    public interface IFeatureCategoryRepository
    {
        IEnumerable<FeatureCategory> GetFeatureCategories(string featureCategoryId);

        IEnumerable<FeatureCategory> UpdateFeatureCategories(IEnumerable<Upd_VM.FeatureCategory> featureCategories);

        IEnumerable<FeatureCategory> InsertFeatureCategories(IEnumerable<Ins_VM.FeatureCategory> featureCategories);

        int DeleteFeatureCategory(string featureCategoryId);
    }
}
