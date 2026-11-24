using ClinicaCitasAPI.DTOs;
using ClinicaCitasAPI.Exceptions;
using ClinicaCitasAPI.Models;
using ClinicaCitasAPI.Services.Interfaces;

namespace ClinicaCitasAPI.Services;

public class CitaService : ICitaService
{
    private readonly List<Cita> _citas = new();
    private int _nextId = 1;

    public CitaService()
    {
        // Datos iniciales para facilitar las pruebas
        _citas.Add(new Cita
        {
            Id = _nextId++,
            PacienteId = 1,
            MedicoId = 1,
            FechaHora = DateTime.Now.AddDays(1),
            Motivo = "Consulta general de control",
            Estado = "Programada"
        });
    }

    public IEnumerable<Cita> ObtenerTodas() => _citas;

    public Cita ObtenerPorId(int id)
    {
        var cita = _citas.FirstOrDefault(c => c.Id == id)
            ?? throw new BusinessException($"No se encontró la cita con ID {id}.", 404);
        return cita;
    }

    public Cita Crear(CrearCitaDto dto)
    {
        if (dto.FechaHora <= DateTime.Now)
            throw new BusinessException("La fecha de la cita debe ser futura.", 400);

        bool ocupado = _citas.Any(c =>
            c.MedicoId == dto.MedicoId &&
            c.FechaHora == dto.FechaHora &&
            c.Estado != "Cancelada");

        if (ocupado)
            throw new BusinessException("El médico ya tiene una cita programada en ese horario.", 409);

        var nuevaCita = new Cita
        {
            Id = _nextId++,
            PacienteId = dto.PacienteId,
            MedicoId = dto.MedicoId,
            FechaHora = dto.FechaHora,
            Motivo = dto.Motivo,
            Estado = "Programada"
        };

        _citas.Add(nuevaCita);
        return nuevaCita;
    }

    public Cita Actualizar(int id, CrearCitaDto dto)
    {
        var cita = ObtenerPorId(id);
        cita.PacienteId = dto.PacienteId;
        cita.MedicoId = dto.MedicoId;
        cita.FechaHora = dto.FechaHora;
        cita.Motivo = dto.Motivo;
        return cita;
    }

    public void Cancelar(int id)
    {
        var cita = ObtenerPorId(id);
        _citas.Remove(cita);
    }
}