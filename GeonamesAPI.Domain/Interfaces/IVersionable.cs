namespace GeonamesAPI.Domain.Interfaces
{
    interface IVersionable
    {
        byte[] RowId { get; set; }
    }
}
