namespace Foxtrot.Models.Contracts
{
    public interface IConcurrencyModel
    {
        public byte[] RowVersion { get; set; }
    }
}