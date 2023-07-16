using System.Text.Json.Serialization;

namespace Diplom_Project
{
    public class CreateBillRespond
    {
        public string Name { get; set; } = string.Empty;
        public double Total { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
