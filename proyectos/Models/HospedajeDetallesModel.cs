namespace HotelesCaribe.Models
{
    public class HospedajeDetallesModel
    {
        public EmpresaHospedaje Hotel { get; set; }
        public List<Habitacion> Habitaciones { get; set; }
        public BusquedaFiltros Filtros { get; set; }
        public List<VwFotosEmpresaHospedaje> FotosHotel { get; set; } = new List<VwFotosEmpresaHospedaje>();
    }

    public class BusquedaFiltros
    {
        public DateTime? FechaEntrada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int NumeroPersonas { get; set; } = 1;
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public bool? WiFiGratuito { get; set; }
        public bool? DesayunoIncluido { get; set; }
        public bool? ParqueoIncluido { get; set; }
        public bool? ConAireAcondicionado { get; set; }
    }
}