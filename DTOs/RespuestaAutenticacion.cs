namespace BE_LoansApp.DTOs
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }

        public Boolean OK { get; set; }
    }
}
