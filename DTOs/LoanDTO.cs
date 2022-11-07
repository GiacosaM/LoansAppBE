using BE_LoansApp.Entities;

namespace BE_LoansApp.DTOs
{
    public class LoanDTO
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public ThingDTO Thing { get; set; }

        public PersonDTO Person { get; set; }

    }
}
