
CREATE DATABASE FinanzasDB;
GO

USE FinanzasDB;
GO

-- tabla para los ingresos
CREATE TABLE Ingresos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(18, 2) NOT NULL,
    Fecha DATE NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Categoria NVARCHAR(100) NULL
);
GO

--  tabla para los gastos
CREATE TABLE Gastos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Monto DECIMAL(18, 2) NOT NULL,
    Fecha DATE NOT NULL,
    Descripcion NVARCHAR(255) NULL,
    Categoria NVARCHAR(100) NULL
);
GO

--  tabla para el balance
CREATE TABLE Balance (
    Id INT PRIMARY KEY IDENTITY(1,1),
    MontoTotal DECIMAL(18, 2) NOT NULL,
    FechaActualizacion DATE NOT NULL
);
GO
