using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class AlternateName : IVersionable
    {
        public int AlternateNameId { get; set; }
        public int GeonameId { get; set; }
        public string ISO6393LanguageCode { get; set; }
        public string AlternateName1 { get; set; }
        public bool? IsPreferredName { get; set; }
        public bool? IsShortName { get; set; }
        public bool? IsColloquial { get; set; }
        public bool? IsHistoric { get; set; }
        public byte[] RowId { get; set; }
    }
}
