using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.DTOs
{
    public class CategoryViewModelDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " Debe ingresar una categoria")]
        [StringLength(maximumLength: 25)]
        public string Description { get; set; }
    }
}
