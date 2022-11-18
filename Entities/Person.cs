using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Entities
{
    public class Person
    {

        public int Id { get; set; }

        [StringLength(maximumLength: 25)]
        public string Name { get; set; }
        [StringLength(maximumLength: 25)]
        public string Lastname { get; set; }
        [StringLength(maximumLength: 25)]
        public string Email { get; set; }
        [StringLength(maximumLength: 50)]
        public string Telefono { get; set; }

        public List<Loan> Loans { get; set; }



    }
}
