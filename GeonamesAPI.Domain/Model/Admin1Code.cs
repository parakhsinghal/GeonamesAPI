using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class Admin1Code:IVersionable
    {
        public string Admin1CodeId { get; set; }
        public string Admin1CodeName { get; set; }
        public string ASCIIName { get; set; }
        public byte[] RowId { get; set; }
    }
}
