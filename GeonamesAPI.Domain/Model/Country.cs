using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class Country : IVersionable
    {
        public string ISOCountryCode { get; set; }
        public string ISO3Code { get; set; }
        public int? ISONumeric { get; set; }
        public string FIPSCode { get; set; }
        public string CountryName { get; set; }
        public string Capital { get; set; }
        public double? SqKmArea { get; set; }
        public long? TotalPopulation { get; set; }
        public string ContinentCodeId { get; set; }
        public string TopLevelDomain { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Phone { get; set; }
        public string PostalFormat { get; set; }
        public string PostalRegex { get; set; }
        public string Languages { get; set; }
        public int? GeonameId { get; set; }
        public string Neighbors { get; set; }
        public string EquivalentFipsCode { get; set; }
        public byte[] RowId { get; set; }
    }
}
