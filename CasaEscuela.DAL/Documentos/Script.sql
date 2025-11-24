CREATE TABLE usuarios (
    Id INT NOT NULL AUTO_INCREMENT,
    DUI VARCHAR(10) NOT NULL,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(120) NOT NULL,
    Password VARCHAR(120) NOT NULL,
    FechRegistro DATETIME NOT NULL,
    FechaValidez DATETIME NOT NULL,
    CargoDirectiva VARCHAR(50),
    Estado TINYINT,
    PRIMARY KEY (Id)
);

CREATE TABLE permisos (
    Id INT NOT NULL AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Modulo VARCHAR(100) NOT NULL,
    PRIMARY KEY (Id)
);

CREATE TABLE usuariopermisos (
    IdUsuario INT NOT NULL,
    IdPermiso INT NOT NULL,
    PRIMARY KEY (IdUsuario, IdPermiso),
    INDEX IdPermiso (IdPermiso),
    CONSTRAINT usuariopermisos_ibfk_1 FOREIGN KEY (IdUsuario)
        REFERENCES usuarios (Id),
    CONSTRAINT usuariopermisos_ibfk_2 FOREIGN KEY (IdPermiso)
        REFERENCES permisos (Id)
);

CREATE TABLE correlativos (
    Id TINYINT NOT NULL AUTO_INCREMENT,
    Tipo TINYINT NOT NULL DEFAULT 0,
    Valor INT(10) UNSIGNED ZEROFILL NOT NULL,
    AliasInicio VARCHAR(50),
    UltFechaActualizacion DATETIME,
    AliasFin VARCHAR(50),
    IdSucursal INT,
    PRIMARY KEY (Id)
);

