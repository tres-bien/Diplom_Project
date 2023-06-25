namespace Diplom_Project
{
    public class Bill : IBillIdentifier, IBill, IBillPropertys
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Total { get; set; }
        public string AmountPaid { get; set; } = string.Empty;
        public string Member { get; set; } = string.Empty;
    }
}
