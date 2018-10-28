using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class FeatureCode : IVersionable
    {
        public string FeatureCodeId { get; set; }
        public string FeatureCodeName { get; set; }
        public string Description { get; set; }
        public byte[] RowId { get; set; }
    }
}
