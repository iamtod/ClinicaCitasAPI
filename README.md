# ClinicaCitasAPI

API RESTful desarrollada en C# con .NET Core para gestionar las citas médicas de una clínica privada. Permite registrar, consultar, actualizar y cancelar citas de manera ordenada mediante una arquitectura en capas, validaciones de entrada, manejo centralizado de excepciones y persistencia simulada en memoria.

## Tecnologías utilizadas

- C# y .NET Core (ASP.NET Core Web API)
- OpenAPI y Scalar (documentación interactiva en el navegador)
- Postman (pruebas funcionales)

## Estructura del proyecto

```text
ClinicaCitasAPI/
├── Controllers/
│   └── CitasController.cs          # Endpoints RESTful (GET, POST, PUT, DELETE)
├── DTOs/
│   ├── ApiResponse.cs              # Formato estandarizado de respuestas JSON
│   └── CrearCitaDto.cs             # Objeto de transferencia con Data Annotations
├── Exceptions/
│   └── BusinessException.cs        # Excepción personalizada para reglas de negocio
├── Middlewares/
│   └── ExceptionMiddleware.cs      # Manejo centralizado de errores
├── Models/
│   ├── Persona.cs                  # Clase base abstracta
│   ├── Paciente.cs                 # Hereda de Persona
│   ├── Medico.cs                   # Hereda de Persona
│   └── Cita.cs                     # Entidad principal de cita médica
├── Services/
│   ├── Interfaces/
│   │   └── ICitaService.cs         # Contrato del servicio
│   └── CitaService.cs              # Lógica de negocio y lista en memoria
├── Program.cs                      # Configuración de servicios y pipeline HTTP
└── README.md
```

## Requisitos previos

- .NET SDK 9.0 o superior
- Git
- Visual Studio Code (recomendado con la extensión C# Dev Kit)
- Postman (para ejecutar las pruebas funcionales)

## Instalación y ejecución

Sigue estos pasos desde una terminal (PowerShell, CMD o bash):

1. Clonar el repositorio:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   ```

2. Ingresar a la carpeta del proyecto:
   ```bash
   cd ClinicaCitasAPI
   ```

3. Restaurar las dependencias del proyecto:
   ```bash
   dotnet restore
   ```

4. Ejecutar la aplicación:
   ```bash
   dotnet run
   ```

5. Verificar en la consola el puerto asignado (por ejemplo, `http://localhost:5059`).

6. Abrir la documentación interactiva en el navegador:
   ```text
   http://localhost:5059/scalar/v1
   ```

## Comportamiento de la API

- **Persistencia en memoria:** Los datos se almacenan en una colección `List<Cita>` registrada como servicio `Singleton` en `Program.cs`. Esto permite conservar los registros durante toda la ejecución sin requerir una base de datos externa. Al iniciar, la API precarga una cita de ejemplo con `Id = 1`.
- **Validaciones:** Los datos enviados en `POST` y `PUT` se validan automáticamente con anotaciones (`[Required]`, `[Range]`, `[StringLength]`). Adicionalmente, la capa de servicio impide registrar citas en fechas pasadas o asignar dos citas al mismo médico en el mismo horario.
- **Respuestas y errores:** Todas las respuestas siguen la estructura `ApiResponse` (`exito`, `mensaje`, `datos`). Cualquier error de negocio o excepción no controlada es interceptado por `ExceptionMiddleware` y devuelto con el código de estado HTTP correspondiente (`400`, `404`, `409` o `500`).

## Endpoints disponibles

URL base local: `http://localhost:5059`

| Método | Ruta | Descripción | Códigos HTTP |
| :--- | :--- | :--- | :--- |
| GET | `/api/Citas` | Lista todas las citas registradas | 200 |
| GET | `/api/Citas/{id}` | Obtiene una cita específica por su ID | 200, 404 |
| POST | `/api/Citas` | Registra una nueva cita médica | 201, 400, 409 |
| PUT | `/api/Citas/{id}` | Actualiza los datos de una cita existente | 200, 400, 404 |
| DELETE | `/api/Citas/{id}` | Elimina una cita por su ID | 204, 404 |

## Ejemplos de uso

### 1. Registrar una cita (`POST /api/Citas`)

Cuerpo de la solicitud (JSON):
```json
{
  "pacienteId": 2,
  "medicoId": 1,
  "fechaHora": "2026-12-10T10:30:00",
  "motivo": "Control de cardiología"
}
```

Respuesta exitosa (`201 Created`):
```json
{
  "exito": true,
  "mensaje": "Cita registrada correctamente.",
  "datos": {
    "id": 2,
    "pacienteId": 2,
    "medicoId": 1,
    "fechaHora": "2026-12-10T10:30:00",
    "motivo": "Control de cardiología",
    "estado": "Programada"
  }
}
```

### 2. Actualizar una cita (`PUT /api/Citas/2`)

Cuerpo de la solicitud (JSON):
```json
{
  "pacienteId": 2,
  "medicoId": 1,
  "fechaHora": "2026-12-10T11:00:00",
  "motivo": "Control de cardiología reprogramado"
}
```

### 3. Consulta de recurso inexistente (`GET /api/Citas/999`)

Respuesta controlada por `ExceptionMiddleware` (`404 Not Found`):
```json
{
  "Exito": false,
  "Mensaje": "No se encontró la cita con ID 999.",
  "Datos": null
}
```

## Pruebas funcionales

Las pruebas funcionales se pueden ejecutar de dos maneras:

1. **Con Scalar:** Abrir `http://localhost:5059/scalar/v1`, seleccionar el endpoint deseado y presionar `Test Request`.
2. **Con Postman:** Configurar peticiones hacia `http://localhost:5059/api/Citas` enviando el encabezado `Content-Type: application/json` en las solicitudes `POST` y `PUT`.

Casos de prueba recomendados para validar el funcionamiento completo:
- `GET /api/Citas` para verificar el registro inicial precargado.
- `POST /api/Citas` con datos válidos (esperado: `201 Created`).
- `POST /api/Citas` con un motivo menor a 5 caracteres o una fecha pasada (esperado: `400 Bad Request`).
- `GET /api/Citas/999` para comprobar el manejo centralizado de errores (esperado: `404 Not Found`).
- `PUT /api/Citas/2` para verificar la actualización en memoria (esperado: `200 OK`).
- `DELETE /api/Citas/2` para confirmar la eliminación del registro (esperado: `204 No Content`).

## Equipo de desarrollo

- Integrantes:
Alvarez Franco, Walter
Virrueta Diaz, Victor

GitHub: https://github.com/iamtod/ClinicaCitasAPI