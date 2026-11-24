namespace ClinicaCitasAPI.Models;

public abstract class Persona
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Dni { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}