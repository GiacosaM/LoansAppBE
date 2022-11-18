using System.ComponentModel.DataAnnotations;

namespace BE_LoansApp.Models
{
    public class Thing
    {
        public int Id { get; set; }

        [Required(ErrorMessage = " El Nombre del Objeto es Obligatorios")]
        [MaxLength(25, ErrorMessage = "El nombre no puede exceder los 25 caracteres")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }


    }
}
