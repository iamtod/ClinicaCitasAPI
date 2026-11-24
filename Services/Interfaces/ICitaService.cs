using ClinicaCitasAPI.DTOs;
using ClinicaCitasAPI.Models;

namespace ClinicaCitasAPI.Services.Interfaces;

public interface ICitaService
{
    IEnumerable<Cita> ObtenerTodas();
    Cita ObtenerPorId(int id);
    Cita Crear(CrearCitaDto dto);
    Cita Actualizar(int id, CrearCitaDto dto);
    void Cancelar(int id);
}