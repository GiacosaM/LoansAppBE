using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Models
{
    public class ThingViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " El Nombre del Objeto es Obligatorioz")]
        [MaxLength(10, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La categoria es Obligatoria")]
        [MaxLength(10, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        [Display(Name = "Category")]


        public string Category { get; set; }
        
    }
}
