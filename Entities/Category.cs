using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 25)]
        public string Description { get; set; }

        public List<Thing> Things { get; set; }

    }
}
