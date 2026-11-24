namespace ClinicaCitasAPI.DTOs;

public class ApiResponse<T>
{
    public bool Exito { get; set; }
    public string Mensaje { get; set; } = string.Empty;
    public T? Datos { get; set; }

    public static ApiResponse<T> Ok(T datos, string mensaje = "Operación exitosa")
        => new() { Exito = true, Mensaje = mensaje, Datos = datos };

    public static ApiResponse<T> Error(string mensaje)
        => new() { Exito = false, Mensaje = mensaje, Datos = default };
}