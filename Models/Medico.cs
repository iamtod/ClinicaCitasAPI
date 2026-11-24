namespace ClinicaCitasAPI.Models;

public class Paciente : Persona
{
    public string Correo { get; set; } = string.Empty;
}

public class Medico : Persona
{
    public string Especialidad { get; set; } = string.Empty;
    public string NumeroColegiatura { get; set; } = string.Empty;
}