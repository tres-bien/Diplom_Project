namespace Diplom_Project
{
    public class DefaultBillPropertys : IBillPropertys
    {
        public int RowId { get; set; }
        public string AmountPaid { get; set; } = string.Empty;
        public string Member { get; set; } = string.Empty;
    }
}
