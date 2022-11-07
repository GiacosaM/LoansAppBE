namespace BE_LoansApp.Entities
{
    public class Person
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public List<Loan> Loans { get; set; }



    }
}
