namespace BE_LoansApp.Entities
{
    public class Thing
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


    }
}
