--Query para la creacion del inventario:

USE DB_ACCESO

CREATE TABLE Articulos (
    IdArticulo INT PRIMARY KEY identity(1,1),
    Descripcion VARCHAR(100),
    Existencia INT,
    TipoInventarioId INT,
	AlmacenID INT,
	TransaccionId INT,
	AsientoId INT,
    CostoUnitario DECIMAL(10, 2),
    Estado BIT
);


CREATE TABLE TiposInventarios (
	TipoInventarioId INT PRIMARY KEY identity(1,1),
	Descripcion VARCHAR(100),
	CuentaContable NVARCHAR(50),
	Estado BIT
);

ALTER TABLE Articulos
ADD CONSTRAINT FK_TipoInventarios
FOREIGN KEY (TipoInventarioId) REFERENCES TiposInventarios(TipoInventarioId);

CREATE TABLE Almacenes (
	AlmacenID INT PRIMARY KEY identity(1,1),
	Descripcion VARCHAR(100),
	Estado BIT
);

ALTER TABLE Articulos
ADD CONSTRAINT FK_Almacenes
FOREIGN KEY (AlmacenID) REFERENCES Almacenes(AlmacenID);

CREATE TABLE TipoTransacciones (
    TipoTransaccionId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50)
);

INSERT INTO TipoTransacciones (Nombre) VALUES ('Entrada');
INSERT INTO TipoTransacciones (Nombre) VALUES ('Salida');
INSERT INTO TipoTransacciones (Nombre) VALUES ('Traslado');
INSERT INTO TipoTransacciones (Nombre) VALUES ('Ajuste');

CREATE TABLE Transacciones (
    TransaccionId INT PRIMARY KEY IDENTITY(1,1),
    TipoTransaccionId INT,
    IdArticulo INT,
    Fecha DATE,
    Cantidad INT,
    Monto DECIMAL(10, 2),
    FOREIGN KEY (TipoTransaccionId) REFERENCES TipoTransacciones(TipoTransaccionId),
    FOREIGN KEY (IdArticulo) REFERENCES Articulos(IdArticulo)
);

CREATE TABLE TipoMovimientos (
    TipoMovimientoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50)
);

CREATE TABLE AsientoContable (
    IdentificadorAsiento INT PRIMARY KEY IDENTITY(1,1),
    Descripcion NVARCHAR(100),
    TipoInventarioId INT,
    CuentaContable NVARCHAR(50),
    TipoMovimientoId INT,
    FechaAsiento DATE,
    MontoAsiento DECIMAL(10, 2),
    Estado BIT,
    FOREIGN KEY (TipoInventarioId) REFERENCES TiposInventarios(TipoInventarioId),
    FOREIGN KEY (TipoMovimientoId) REFERENCES TipoMovimientos(TipoMovimientoId)
);

