# EstateManager

```diff
+ EstateManager es una aplicación web desarrollada en .NET 8 para la gestión de propiedades inmobiliarias, propietarios, imágenes y trazabilidad de cambios.
+ Incluye autenticación JWT y roles de usuario.
+ Arquitectura por capas: API, Application, Domain, Infrastructure y Tests.
+ Se utilizan patrones de diseño como Repository, Unit of Work, y Dependency Injection para mantener el código limpio, escalable y fácil de mantener.

- Repositorio Git: https://github.com/Jpbuelva/EstateManager.git

+ Configuración inicial obligatoria:
  Modificar la cadena de conexión en `appsettings.json` para apuntar a tu base de datos SQL Server antes de ejecutar la aplicación.

+ Migraciones EF Core:
  Ubicarse en EstateManager.Infrastructure.
  Crear migración: dotnet ef migrations add InitialCreate --startup-project ../EstateManager.API
  Aplicar migración: dotnet ef database update --startup-project ../EstateManager.API
  Instalar EF Core CLI: dotnet tool install --global dotnet-ef

+ Estructura del proyecto:
  EstateManager.API: API REST principal con endpoints para autenticación y gestión de propiedades.
  EstateManager.Application: Lógica de negocio, servicios, DTOs y mapeos.
  EstateManager.Domain: Entidades, interfaces de repositorio y constantes.
  EstateManager.Infrastructure: Persistencia con Entity Framework Core, migraciones, repositorios y configuración de dependencias.
  EstateManager.Tests: Pruebas unitarias con NUnit.

+ Entidades de dominio:
  Property, Owner, PropertyImage, PropertyTrace.

+ Persistencia:
  EstateDbContext con integración Identity.
  Configuraciones en Configurations.
  Migraciones en Migrations.

+ Repositorios:
  IPropertyRepository / PropertyRepository, IUnitOfWork / UnitOfWork.

+ Servicios de aplicación:
  PropertyService, AuthService.

+ API Controllers:
  PropertiesController, AuthController.

+ Seguridad:
  JWT configurado en Program.cs.
  Roles definidos en UserRole.

+ DTOs y mapeos:
  DTOs en Application.DTOs, mapeos en PropertyProfile.

+ Flujo principal:
  Registro y login en AuthController, JWT generado y validado en cada request.
  CRUD y operaciones específicas en PropertiesController.
  Trazabilidad en PropertyTrace.
  Persistencia mediante UnitOfWork.

+ Configuración adicional:
  SQL Server en appsettings.json.
  Swagger habilitado.
  JWT configurado en appsettings.json.



+ Pruebas unitarias:
  NUnit en EstateManager.Tests.

+ Extensibilidad:
  Nuevos servicios/repositorios en DependencyInjection.
  Nuevas entidades en EstateDbContext y Configurations.

+ Archivos y clases relevantes:
  Program.cs, PropertiesController.cs, AuthController.cs, PropertyService.cs, AuthService.cs, EstateDbContext.cs, PropertyRepository.cs, Property.cs, Owner.cs, PropertyImage.cs, PropertyTrace.cs
