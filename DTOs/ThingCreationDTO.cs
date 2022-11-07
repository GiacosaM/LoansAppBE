using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.DTOs
{
    public class ThingCreationDTO
    {
        [Required(ErrorMessage = "El Campor {0} es requerido")]
        [StringLength(maximumLength:25, ErrorMessage ="El campo {0} no puede exceder la cantidad de {1} caracteres")]
        public string description { get; set; }
    }
}
