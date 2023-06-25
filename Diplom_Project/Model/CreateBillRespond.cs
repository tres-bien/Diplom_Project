using System.Text.Json.Serialization;

namespace Diplom_Project
{
    public class CreateBillRespond : IBill
    {
        public string Name { get; set; } = string.Empty;
        public double Total { get; set; }

        [JsonConverter(typeof(BillPropertysConverter))]
        public List<IBillPropertys> Members { get; set; }
    }
}
