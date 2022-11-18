using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Models
{
    public class ThingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = " El Nombre del Objeto es Obligatorioz")]
        [MaxLength(25, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "La categoria es Obligatoria")]
        [MaxLength(25, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        public string Category { get; set; }
        
    }
}
