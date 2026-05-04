# ORM y DTOs

// TODO: 
La solución utiliza una estructura en capas. A continuación se describe, paso a paso, cómo crear y utilizar un objeto tipo DTO en cada capa principal: `Application`, `Business`, `Entities`, `Helpers`, `Interfaces` y `sqlRepository`.

1. Entities (modelo de datos / DTO)
   - Crear la clase DTO que represente los datos que se transferirán entre capas. Guardarla en el proyecto `Entities` o en una carpeta `Entities/DTOs`.
   - Ejemplo:

```csharp
// Entities/DTOs/ConectorDto.cs
public class ConectorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Tipo { get; set; }
}
```

2. Interfaces
   - Definir contratos que operen con DTOs, por ejemplo repositorios o servicios. Esto permite desacoplar implementaciones.

```csharp
// Interfaces/IConectorRepository.cs
public interface IConectorRepository
{
    ConectorDto GetById(int id);
    IEnumerable<ConectorDto> GetAll();
    void Save(ConectorDto dto);
}
```

3. sqlRepository (implementación de acceso a datos)
   - Implementar `IConectorRepository` en `sqlRepository`. Aquí se realiza el mapeo entre entidades de la base de datos (ORM/Model) y los DTOs.
   - Hacer mapeo manual o usar herramientas como AutoMapper.

```csharp
// sqlRepository/ConectorRepository.cs
public class ConectorRepository : IConectorRepository
{
    public ConectorDto GetById(int id)
    {
        // ejemplo simplificado: consultar DB y mapear a DTO
        var entity = /* consultar la entidad desde DbContext */ null;
        return entity == null ? null : new ConectorDto { Id = entity.Id, Nombre = entity.Nombre, Tipo = entity.Tipo };
    }

    // ... GetAll, Save, etc.
}
```

4. Business (lógica de negocio)
   - Consumir `IConectorRepository` y trabajar con `ConectorDto` para aplicar reglas de negocio.

```csharp
// Business/ConectorService.cs
public class ConectorService
{
    private readonly IConectorRepository _repo;
    public ConectorService(IConectorRepository repo) { _repo = repo; }

    public ConectorDto Obtener(int id)
    {
        var dto = _repo.GetById(id);
        // aplicar reglas de negocio
        return dto;
    }
}
```

5. Application (casos de uso / orquestación)
   - Orquestar llamadas a la capa `Business` y preparar/transformar DTOs para la UI o APIs.

```csharp
// Application/ConectorApplication.cs
public class ConectorApplication
{
    private readonly ConectorService _service;
    public ConectorApplication(ConectorService service) { _service = service; }

    public ConectorDto GetConectorForUi(int id)
    {
        var dto = _service.Obtener(id);
        // transformar si es necesario
        return dto;
    }
}
```

6. Helpers
   - Añadir utilidades para mapeos, validaciones y conversiones (por ejemplo, un `Mapper` estático o adaptadores para AutoMapper).

```csharp
// Helpers/ConectorMapper.cs
public static class ConectorMapper
{
    public static ConectorDto ToDto(ConectorEntity e) => new ConectorDto { Id = e.Id, Nombre = e.Nombre, Tipo = e.Tipo };
}
```

// TODO: Buenas prácticas rápidas:
- Mantener DTOs simples y orientados al transporte de datos (sin lógica de negocio).
- Definir interfaces en la capa `Interfaces` para facilitar pruebas unitarias y DI.
- Centralizar mapeos en `Helpers` o usar `AutoMapper` para reducir código repetido.
- En `sqlRepository` mapear explícitamente entre modelos de persistencia y DTOs para evitar fugas de entidad a capas superiores.

Con esta guía se facilita crear, ubicar y consumir DTOs de forma coherente en la arquitectura del proyecto.