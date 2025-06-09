namespace HotelesCaribe.Models
{
    public class ModeloClienteConTelefonos
    {
        public Cliente Cliente { get; set; } = new Cliente();

        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Telefono3 { get; set; }
    }
}
