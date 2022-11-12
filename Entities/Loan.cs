using BE_LoansApp.Models;

namespace BE_LoansApp.Entities
{
    public class Loan
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        public int ThingId { get; set; }
        public Thing Thing { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
