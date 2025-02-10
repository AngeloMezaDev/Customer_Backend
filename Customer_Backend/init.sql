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
        created_date DATETIME2  NULL DEFAULT GETDATE(),
        updated_date DATETIME2 NULL,
        is_active BIT  NULL DEFAULT 1,
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
-- Insertar datos coherentes para companies
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
    (1, 'Tecnologia Integral S.A.', '0998589655001', 'Av. Malecon 2000', '0914567890', 'contacto@tecintegral.com.ec', 0),
    (2, 'Servicios Industriales del Sur', '0998741655001', 'Av. Juan Tanca Marengo', '0517654321', 'info@servindustrial.com.ec', 0);
    
    SET IDENTITY_INSERT dbo.companies OFF;
    
    PRINT 'Datos reales para companies insertados.';
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
        is_active,
        is_deleted,
        CreatedDate
    )
    VALUES 
    -- Clientes para Tecnología Integral S.A.
    (1, 'Martin', 'Rodriguez', 'martin.rodriguez@gmail.com', '0912345678', 'Av. Cabildo 1500, CABA', 1, 0, GETDATE()),
    (1, 'Carolina', 'Martinez', 'carolina.martinez@hotmail.com', '0913456789', 'Belgrano 750, CABA', 1, 0, GETDATE()),
    (1, 'Diego', 'Lopez', 'diego.lopez@outlook.com', '0914567890', 'Lavalle 2000, CABA', 1, 0, GETDATE()),
    
    -- Clientes para Servicios Industriales del Sur
    (2, 'Laura', 'Garcia', 'laura.garcia@yahoo.com', '0915678901', 'Alborada XI Etapa', 1, 0, GETDATE()),
    (2, 'Pablo', 'Sanchez', 'pablo.sanchez@gmail.com', '0916789012', 'Mucho lote 2.', 1, 0, GETDATE()),
    (2, 'Ana', 'Fernandez', 'ana.fernandez@gmail.com', '0917890123', 'Suburbio de Guayaquil', 1, 0, GETDATE());
    
    PRINT 'Datos reales para Customers insertados.';
END
GO

-- Verificación final
SELECT 
    DB_NAME() AS CurrentDatabase, 
    (SELECT COUNT(*) FROM dbo.companies) AS CompanyCount,
    (SELECT COUNT(*) FROM dbo.Customers) AS CustomerCount;
GO