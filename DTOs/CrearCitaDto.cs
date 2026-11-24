using System.ComponentModel.DataAnnotations;

namespace ClinicaCitasAPI.DTOs;

public class CrearCitaDto
{
    [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del paciente debe ser válido.")]
    public int PacienteId { get; set; }

    [Required(ErrorMessage = "El ID del médico es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del médico debe ser válido.")]
    public int MedicoId { get; set; }

    [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
    public DateTime FechaHora { get; set; }

    [Required(ErrorMessage = "El motivo es obligatorio.")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "El motivo debe tener entre 5 y 200 caracteres.")]
    public string Motivo { get; set; } = string.Empty;
}