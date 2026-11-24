using ClinicaCitasAPI.DTOs;
using ClinicaCitasAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaCitasAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly ICitaService _citaService;

    public CitasController(ICitaService citaService) => _citaService = citaService;

    [HttpGet]
    public IActionResult ObtenerTodas()
        => Ok(ApiResponse<object>.Ok(_citaService.ObtenerTodas()));

    [HttpGet("{id:int}")]
    public IActionResult ObtenerPorId(int id)
        => Ok(ApiResponse<object>.Ok(_citaService.ObtenerPorId(id)));

    [HttpPost]
    public IActionResult Crear([FromBody] CrearCitaDto dto)
    {
        var creada = _citaService.Crear(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = creada.Id },
            ApiResponse<object>.Ok(creada, "Cita registrada correctamente."));
    }

    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, [FromBody] CrearCitaDto dto)
    {
        var actualizada = _citaService.Actualizar(id, dto);
        return Ok(ApiResponse<object>.Ok(actualizada, "Cita actualizada correctamente."));
    }

    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        _citaService.Cancelar(id);
        return NoContent();
    }
}