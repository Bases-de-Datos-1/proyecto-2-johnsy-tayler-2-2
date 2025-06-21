namespace HotelesCaribe.Models
{
    public class ModeloEmpresaImagenes
    {
        public int IdEmpresa { get; set; }
        public List<string> ImagenesUrls { get; set; } = new List<string>();
        public List<IFormFile> ArchivosImagenes { get; set; } = new List<IFormFile>();
    }
}