namespace HotelesCaribe.Models
{
    public class HotelesUsuarioResult
    {
        public int idEmpresaHospedaje { get; set; }
        public string nombreHotel { get; set; } = string.Empty;
        public string cedulaJuridica { get; set; } = string.Empty;
        public string ubicacion { get; set; } = string.Empty;
        public string tipoHospedaje { get; set; } = string.Empty;
        public DateTime fechaAsignacion { get; set; }
        public int totalHabitaciones { get; set; }
    }
}
