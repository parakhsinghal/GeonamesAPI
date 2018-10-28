using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class Admin2Code : IVersionable
    {
        public string Admin2CodeId { get; set; }
        public string Admin2CodeName { get; set; }
        public string ASCIIName { get; set; }
        public byte[] RowId { get; set; }
    }
}
