using GeonamesAPI.Domain.Interfaces;

namespace GeonamesAPI.Domain
{
    public class Hierarchy : IVersionable
    {
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public string Type { get; set; }
        public byte[] RowId { get; set; }
    }
}
