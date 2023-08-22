using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Diplom_Project
{
    public class Member : IMember
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double AmountPaid { get; set; }
        public int BillId { get; }

        [ForeignKey("BillId")]
        [JsonIgnore]
        public Bill? Bill { get; set; } = null!;
    }
}
