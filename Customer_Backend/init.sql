USE master;
GO

-- Crear la base de datos si no existe
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CompanyManagement')
BEGIN
    CREATE DATABASE CompanyManagement;
    PRINT 'Base de datos CompanyManagement creada.';
END
ELSE
BEGIN
    PRINT 'La base de datos CompanyManagement ya existe.';
END
GO

-- Usar la base de datos
USE CompanyManagement;
GO

-- Crear esquema dbo si no existe
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'dbo')
BEGIN
    EXEC('CREATE SCHEMA dbo')
END
GO

-- Crear tabla Companies
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'companies' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.companies (
        company_id BIGINT IDENTITY(1,1) PRIMARY KEY,
        company_name NVARCHAR(100) NOT NULL,
        tax_id NVARCHAR(20) NOT NULL,
        address NVARCHAR(200) NULL,
        phone NVARCHAR(20) NULL,
        email NVARCHAR(100) NULL,
        created_date DATETIME2 NOT NULL DEFAULT GETDATE(),
        updated_date DATETIME2 NULL,
        is_active BIT NOT NULL DEFAULT 1,
        is_deleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT UK_companies_tax_id UNIQUE (tax_id)
    );

    -- Crear índices
    CREATE INDEX idx_companies_tax_id ON dbo.companies (tax_id);
    CREATE INDEX idx_companies_company_name ON dbo.companies (company_name);

    PRINT 'Tabla companies creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla companies ya existe.';
END
GO

-- Crear tabla Customers
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Customers' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Customers (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        CompanyId BIGINT NOT NULL,
        FirstName NVARCHAR(100) NOT NULL,
        LastName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20) NULL,
        Address NVARCHAR(200) NULL,
        is_active BIT NOT NULL DEFAULT 1,
        is_deleted BIT NOT NULL DEFAULT 0,
        CONSTRAINT FK_Customers_Companies FOREIGN KEY (CompanyId) 
            REFERENCES dbo.companies(company_id)
    );

    PRINT 'Tabla Customers creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla Customers ya existe.';
END
GO

-- Insertar algunos datos de ejemplo (opcional)
IF NOT EXISTS (SELECT TOP 1 * FROM dbo.companies)
BEGIN
    SET IDENTITY_INSERT dbo.companies ON;
    
    INSERT INTO dbo.companies (
        company_id, 
        company_name, 
        tax_id, 
        address, 
        phone, 
        email,
        is_deleted
    )
    VALUES 
    (1, 'Empresa de Ejemplo 1', 'TAX001', 'Dirección 1', '+54 9 11 1234-5678', 'empresa1@example.com', 0),
    (2, 'Empresa de Ejemplo 2', 'TAX002', 'Dirección 2', '+54 9 11 8765-4321', 'empresa2@example.com', 0);
    
    SET IDENTITY_INSERT dbo.companies OFF;
    
    PRINT 'Datos de ejemplo para companies insertados.';
END
GO

IF NOT EXISTS (SELECT TOP 1 * FROM dbo.Customers)
BEGIN
    INSERT INTO dbo.Customers (
        CompanyId, 
        FirstName, 
        LastName, 
        Email, 
        Phone, 
        Address,
        is_deleted
    )
    VALUES 
    (1, 'Juan', 'Pérez', 'juan.perez@example.com', '+54 9 11 1111-2222', 'Calle Falsa 123', 0),
    (2, 'María', 'González', 'maria.gonzalez@example.com', '+54 9 11 3333-4444', 'Avenida Siempre Viva 742', 0);
    
    PRINT 'Datos de ejemplo para Customers insertados.';
END
GO

-- Verificación final
SELECT 
    DB_NAME() AS CurrentDatabase, 
    (SELECT COUNT(*) FROM dbo.companies) AS CompanyCount,
    (SELECT COUNT(*) FROM dbo.Customers) AS CustomerCount;
GO