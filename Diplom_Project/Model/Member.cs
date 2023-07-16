using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom_Project
{
    public class Member : IMember
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public double AmountPaid { get; set; }
        public int BillId { get; }

        //[ForeignKey("BillId")]
        //public Bill Bill { get; set; } = null!;
    }
}
