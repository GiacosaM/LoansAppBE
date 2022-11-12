using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.DTOs
{
    public class ThingViewModelDTO
    {
        [Required(ErrorMessage = " El Nombre del Objeto es Obligatorio")]
        [MaxLength(10, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = " El Nombre de la Categoria es Obligatorio")]
        [MaxLength(10, ErrorMessage = "El nombre no puede exceder los 10 caracteres")]
        public string Category { get; set; }

    }
}
