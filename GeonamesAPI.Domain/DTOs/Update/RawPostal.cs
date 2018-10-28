using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain.ViewModels.Update
{
    public partial class RawPostal : IVersionable
    {
        public string ISOCountryCode { get; set; }
        public string PostalCode { get; set; }
        public string PlaceName { get; set; }
        public string Admin1Name { get; set; }
        public string Admin1Code { get; set; }
        public string Admin2Name { get; set; }
        public string Admin2Code { get; set; }
        public string Admin3Name { get; set; }
        public string Admin3Code { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? Accuracy { get; set; }
        public byte[] RowId { get; set; }

    }
}
