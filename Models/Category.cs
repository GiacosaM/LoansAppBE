using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Models
{
    public class Category
    {

        public int Id { get; set; }

        
        [Display(Name = "Description")]
        [StringLength(maximumLength:25)]
        public string Description { get; set; }

        public List<Thing> Things { get; set; }

    }
}
