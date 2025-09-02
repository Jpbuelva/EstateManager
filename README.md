# Documentación Técnica - EstateManager

## Descripción General

**EstateManager** es una aplicación web desarrollada en .NET 8 que permite la gestión de propiedades inmobiliarias, propietarios, imágenes y trazabilidad de cambios. Incluye autenticación JWT y roles de usuario. El proyecto sigue una arquitectura por capas: API, Application, Domain, Infrastructure y Tests.

---

## Ruta del Repositorio Git

- https://github.com/Jpbuelva/EstateManager.git

---

## Estructura del Proyecto

- **EstateManager.API**  
  Proyecto principal de la API REST. Expone endpoints para autenticación y gestión de propiedades.
- **EstateManager.Application**  
  Lógica de negocio, servicios, DTOs y mapeos.
- **EstateManager.Domain**  
  Entidades, interfaces de repositorio y constantes.
- **EstateManager.Infrastructure**  
  Persistencia con Entity Framework Core, migraciones, repositorios y configuración de dependencias.
- **EstateManager.Tests**  
  Pruebas unitarias con NUnit.

---

## Principales Componentes

### 1. Entidades de Dominio

- `EstateManager.Domain.Entities.Property`: Propiedad inmobiliaria.
- `EstateManager.Domain.Entities.Owner`: Propietario.
- `EstateManager.Domain.Entities.PropertyImage`: Imagen asociada a una propiedad.
- `EstateManager.Domain.Entities.PropertyTrace`: Trazabilidad de cambios en la propiedad.

### 2. Persistencia

- `EstateManager.Infrastructure.Persistence.EstateDbContext`: DbContext principal, incluye integración con Identity.
- Configuraciones de entidades en `Configurations`.
- Migraciones en `Migrations`.

### 3. Repositorios

- `EstateManager.Domain.Abstractions.IPropertyRepository`: Interfaz para operaciones CRUD de propiedades.
- `EstateManager.Infrastructure.Repositories.PropertyRepository`: Implementación.
- `EstateManager.Domain.Abstractions.IUnitOfWork`: Interfaz para commit transaccional.
- `EstateManager.Infrastructure.Persistence.UnitOfWork`: Implementación.

### 4. Servicios de Aplicación

- `EstateManager.Application.Services.PropertyService`: Lógica de negocio para propiedades.
- `EstateManager.Application.Services.AuthService`: Registro y login de usuarios con JWT.

### 5. API Controllers

- `EstateManager.API.Controllers.PropertiesController`: Endpoints CRUD de propiedades.
- `EstateManager.API.Controllers.AuthController`: Endpoints de autenticación.

### 6. Seguridad

- Autenticación JWT configurada en `Program.cs`.
- Roles definidos en `EstateManager.Domain.Enum.UserRole`.

### 7. DTOs y Mapeos

- DTOs en `EstateManager.Application.DTOs`.
- Mapeos con AutoMapper en `PropertyProfile`.

---

## Flujo Principal

1. **Autenticación:**  
   - Registro y login vía `AuthController`.
   - JWT generado y validado en cada request.

2. **Gestión de Propiedades:**  
   - CRUD y operaciones específicas (cambio de precio, agregar imagen) vía `PropertiesController`.
   - Cada cambio relevante genera una entrada en la trazabilidad (`PropertyTrace`).

3. **Persistencia:**  
   - Todas las operaciones pasan por repositorios y se confirman con UnitOfWork.

---

## Configuración

- **Base de datos:**  
  SQL Server, cadena de conexión en `appsettings.json`.
- **Swagger:**  
  Documentación interactiva habilitada en desarrollo.
- **JWT:**  
  Clave, issuer y audience configurados en `appsettings.json`.

---

## Migraciones de Entity Framework Core

Para crear y aplicar migraciones ejecuta los siguientes comandos en la terminal, ubicándote en la carpeta `EstateManager.Infrastructure`:

```powershell
# Crear una nueva migración
dotnet ef migrations add InitialCreate --startup-project ../EstateManager.API

# Aplicar la migración a la base de datos
dotnet ef database update --startup-project ../EstateManager.API
```

Asegúrate de tener instalada la herramienta de EF Core CLI:
```powershell
dotnet tool install --global dotnet-ef
```

---

## Pruebas

- Pruebas unitarias en `EstateManager.Tests`.
- Framework: NUnit.

---

## Extensibilidad

- Nuevos servicios y repositorios pueden registrarse en los archivos `DependencyInjection`.
- Nuevas entidades requieren configuración en `EstateDbContext` y su respectiva configuración en `Configurations`.

---

## Archivos y Símbolos Relevantes

- Program.cs
- PropertiesController.cs
- AuthController.cs
- PropertyService.cs
- AuthService.cs
- EstateDbContext.cs
- PropertyRepository.cs
- Property.cs
- Owner.cs
- PropertyImage.cs
- PropertyTrace.cs

 