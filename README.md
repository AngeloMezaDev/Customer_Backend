# Customer_Backend

Descripción
API REST desarrollada en .NET Core para la gestión de clientes y empresas. Este sistema permite administrar la información de empresas y sus clientes asociados, implementando una arquitectura limpia (Clean Architecture) y siguiendo los principios SOLID.
Tecnologías Utilizadas

.NET Core 7
Entity Framework Core
SQL Server
Swagger para documentación de API

Estructura del Proyecto
CopyCustomer_Backend/
├── src/
│   ├── CustomerBackend.Domain/           # Entidades y reglas de negocio
│   ├── CustomerBackend.Application/      # Casos de uso y lógica de aplicación
│   ├── CustomerBackend.Infrastructure/   # Implementaciones de persistencia
│   └── Customer_Backend/                 # API y configuración
└── tests/                               # Proyectos de pruebas unitarias
Requisitos Previos

.NET Core SDK 7.0 o superior
SQL Server 2019 o superior
Visual Studio 2022 / Visual Studio Code

Configuración de Base de Datos

Actualizar la cadena de conexión en appsettings.json:

jsonCopy{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tu_servidor;Database=CompanyManagement;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}

Ejecutar el script de base de datos incluido en Database/InitialScript.sql

Ejecución del Proyecto
Usando Visual Studio

Abrir la solución Customer_Backend.sln
Establecer el proyecto Customer_Backend como proyecto de inicio
Presionar F5 para ejecutar

Usando CLI
bashCopycd src/Customer_Backend
dotnet restore
dotnet build
dotnet run
Estructura de la Base de Datos
Tabla Companies

company_id (PK)
company_name
tax_id (Unique)
address
phone
email
created_date
updated_date
is_active
is_deleted

Tabla Customers

Id (PK)
CompanyId (FK)
FirstName
LastName
Email
Phone
Address
is_active
is_deleted

Endpoints API
Companies

GET /api/companies - Obtener todas las empresas
GET /api/companies/{id} - Obtener empresa por ID
POST /api/companies - Crear nueva empresa
PUT /api/companies/{id} - Actualizar empresa
DELETE /api/companies/{id} - Eliminar empresa (soft delete)

Customers

GET /api/customers - Obtener todos los clientes
GET /api/customers/{id} - Obtener cliente por ID
POST /api/customers - Crear nuevo cliente
PUT /api/customers/{id} - Actualizar cliente
DELETE /api/customers/{id} - Eliminar cliente (soft delete)

Características

Arquitectura Limpia (Clean Architecture)
Implementación de Repository Pattern
Manejo de errores centralizado
Validaciones de datos
Soft Delete implementado
Documentación con Swagger
Logging

Buenas Prácticas Implementadas

Inyección de Dependencias
Principios SOLID
Manejo de excepciones personalizado
Validaciones de modelo
Uso de DTOs para transferencia de datos
Configuración mediante patrones Builder
