# Customer_Backend

Descripción
API REST desarrollada en .NET Core para la gestión de clientes y empresas. Este sistema permite administrar la información de empresas y sus clientes asociados, implementando una arquitectura limpia (Clean Architecture) y siguiendo los principios SOLID.
Tecnologías Utilizadas

.NET Core 7
Entity Framework Core
SQL Server
Swagger para documentación de API

Estructura del Proyecto
Organización de Carpetas
CopyCustomer_Backend/
├── src/
│   ├── CustomerBackend.Domain/              # Capa de Dominio
│   │   ├── Entities/                        # Entidades de dominio
│   │   ├── Interfaces/                      # Interfaces y contratos
│   │   └── ValueObjects/                    # Objetos de valor
│   │
│   ├── CustomerBackend.Application/         # Capa de Aplicación
│   │   ├── DTOs/                           # Objetos de transferencia de datos
│   │   ├── Interfaces/                      # Interfaces de servicios
│   │   ├── Services/                        # Implementación de servicios
│   │   └── Validators/                      # Validadores de datos
│   │
│   ├── CustomerBackend.Infrastructure/      # Capa de Infraestructura
│   │   ├── Data/                           # Contexto y configuración de EF
│   │   ├── Repositories/                    # Implementación de repositorios
│   │   └── Services/                        # Servicios externos
│   │
│   └── Customer_Backend/                    # Capa de Presentación (API)
│       ├── Controllers/                     # Controladores de la API
│       ├── Middleware/                      # Middleware personalizado
│       ├── Extensions/                      # Extensiones de servicios
│       └── Configuration/                   # Archivos de configuración
│
└── tests/                                  # Pruebas
    ├── Customer_Backend.UnitTests/          # Pruebas unitarias
    └── Customer_Backend.IntegrationTests/   # Pruebas de integración
Descripción de Capas
1. Capa de Dominio (CustomerBackend.Domain)

Contiene las entidades del negocio
Define interfaces principales
Reglas de negocio centrales
Sin dependencias externas

2. Capa de Aplicación (CustomerBackend.Application)

Implementa casos de uso
Orquesta flujos de trabajo
Define DTOs y mapeos
Validaciones de datos

3. Capa de Infraestructura (CustomerBackend.Infrastructure)

Implementa persistencia
Acceso a servicios externos
Configuración de Entity Framework
Implementaciones concretas de interfaces

4. Capa de API (Customer_Backend)

Controladores REST
Configuración de la aplicación
Manejo de autenticación
Documentación Swagger

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
