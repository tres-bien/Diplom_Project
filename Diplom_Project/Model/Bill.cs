using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Diplom_Project
{
    public class Bill : IBillIdentifier, IBill
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Total { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();
    }
}
