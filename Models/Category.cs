using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Models
{
    public class Category
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " Debe ingresar una categoria")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public List<Thing> Things { get; set; }

    }
}
