/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE IF NOT EXISTS `casaescuela` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `casaescuela`;

CREATE TABLE IF NOT EXISTS `catnivelescolar` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `catnivelescolar` (`Id`, `Descripcion`) VALUES
	(1, 'Parvularia');

CREATE TABLE IF NOT EXISTS `cattipofamilia` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `cattipofamilia` (`Id`, `Descripcion`) VALUES
	(1, 'Padre');

CREATE TABLE IF NOT EXISTS `cattipoparto` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `cattipoparto` (`Id`, `Descripcion`) VALUES
	(1, 'Cesarea');

CREATE TABLE IF NOT EXISTS `cattipopreceptoria` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `cattipopreceptoria` (`Id`, `Descripcion`) VALUES
	(1, 'SE');

CREATE TABLE IF NOT EXISTS `catvivecon` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `catvivecon` (`Id`, `Descripcion`) VALUES
	(1, 'Madre');

CREATE TABLE IF NOT EXISTS `correlativos` (
  `Id` tinyint NOT NULL AUTO_INCREMENT,
  `Tipo` tinyint NOT NULL DEFAULT '0',
  `Valor` int(10) unsigned zerofill NOT NULL,
  `AliasInicio` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `UltFechaActualizacion` datetime DEFAULT NULL,
  `AliasFin` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IdSucursal` int DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


CREATE TABLE IF NOT EXISTS `estudiantes` (
  `IdEstudiante` int NOT NULL AUTO_INCREMENT,
  `Codigo` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Nombres` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellidos` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FechaNacimiento` date DEFAULT NULL,
  `Sexo` tinyint NOT NULL,
  `NivelEscolarId` int DEFAULT NULL,
  `Grado` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Seccion` varchar(10) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Jornada` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Estado` bit(1) DEFAULT b'1',
  `FechaRegistro` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdEstudiante`),
  KEY `NivelEscolarId` (`NivelEscolarId`),
  CONSTRAINT `estudiantes_ibfk_1` FOREIGN KEY (`NivelEscolarId`) REFERENCES `catnivelescolar` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `estudiantes` (`IdEstudiante`, `Codigo`, `Nombres`, `Apellidos`, `FechaNacimiento`, `Sexo`, `NivelEscolarId`, `Grado`, `Seccion`, `Jornada`, `Estado`, `FechaRegistro`) VALUES
	(1, 'WEQWX', 'walter', 'mena', '2025-12-08', 1, 1, 'a', 'a', 'm', b'1', '2025-12-29 11:10:37'),
	(2, 'WEQWX', 'walter', 'mena', '2025-12-08', 1, 1, 'a', 'a', 'm', b'1', '2025-12-29 11:10:59'),
	(3, 'qwq12121', 'walter ernesto', 'mena', '2025-12-29', 1, 1, 'SEGUNDO', 'a', 'm', b'1', '2025-12-29 11:47:16');


CREATE TABLE IF NOT EXISTS `anamnesis` (
  `IdAnamnesis` int NOT NULL AUTO_INCREMENT,
  `IdEstudiante` int NOT NULL,
  `ViveConId` int DEFAULT NULL,
  `TipoFamiliaId` int DEFAULT NULL,
  `TipoPartoId` int DEFAULT NULL,
  `EmbarazoControlado` bit(1) DEFAULT NULL,
  `ComplicacionesEmbarazo` text COLLATE utf8mb4_unicode_ci,
  `CondicionesSalud` text COLLATE utf8mb4_unicode_ci,
  `DesarrolloLenguaje` text COLLATE utf8mb4_unicode_ci,
  `DesarrolloMotor` text COLLATE utf8mb4_unicode_ci,
  `SituacionFamiliar` text COLLATE utf8mb4_unicode_ci,
  `Observaciones` text COLLATE utf8mb4_unicode_ci,
  `FechaEntrevista` date DEFAULT NULL,
  `Entrevistador` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`IdAnamnesis`),
  KEY `IdEstudiante` (`IdEstudiante`),
  KEY `ViveConId` (`ViveConId`),
  KEY `TipoFamiliaId` (`TipoFamiliaId`),
  KEY `TipoPartoId` (`TipoPartoId`),
  CONSTRAINT `anamnesis_ibfk_1` FOREIGN KEY (`IdEstudiante`) REFERENCES `estudiantes` (`IdEstudiante`),
  CONSTRAINT `anamnesis_ibfk_2` FOREIGN KEY (`ViveConId`) REFERENCES `catvivecon` (`Id`),
  CONSTRAINT `anamnesis_ibfk_3` FOREIGN KEY (`TipoFamiliaId`) REFERENCES `cattipofamilia` (`Id`),
  CONSTRAINT `anamnesis_ibfk_4` FOREIGN KEY (`TipoPartoId`) REFERENCES `cattipoparto` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `anamnesis` (`IdAnamnesis`, `IdEstudiante`, `ViveConId`, `TipoFamiliaId`, `TipoPartoId`, `EmbarazoControlado`, `ComplicacionesEmbarazo`, `CondicionesSalud`, `DesarrolloLenguaje`, `DesarrolloMotor`, `SituacionFamiliar`, `Observaciones`, `FechaEntrevista`, `Entrevistador`) VALUES
	(2, 2, 1, 1, 1, b'1', 'DSDA', 'SDA', 'DFSDFS', 'FSDFSDF', 'DSFSDF', 'SDAFDSAFSA', '2025-12-29', 'WALTER'),
	(3, 3, 1, 1, 1, b'1', 'NO', 'NO', 'NO', 'NO', 'NO', 'NINGUNA', '2025-12-29', 'WALTER MENA');


CREATE TABLE IF NOT EXISTS `estudiantefamiliares` (
  `IdFamiliar` int NOT NULL AUTO_INCREMENT,
  `IdEstudiante` int NOT NULL,
  `TipoParentesco` tinyint NOT NULL,
  `Nombres` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Apellidos` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Edad` int DEFAULT NULL,
  `Escolaridad` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Ocupacion` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ViveConEstudiante` bit(1) DEFAULT NULL,
  `Telefono` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`IdFamiliar`),
  KEY `IdEstudiante` (`IdEstudiante`),
  CONSTRAINT `estudiantefamiliares_ibfk_1` FOREIGN KEY (`IdEstudiante`) REFERENCES `estudiantes` (`IdEstudiante`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `estudiantefamiliares` (`IdFamiliar`, `IdEstudiante`, `TipoParentesco`, `Nombres`, `Apellidos`, `Edad`, `Escolaridad`, `Ocupacion`, `ViveConEstudiante`, `Telefono`) VALUES
	(1, 2, 1, 'WASA', 'ASAS', 5, '33EWDE', NULL, b'1', '33'),
	(2, 3, 1, 'WALTER', 'MENA', 34, 'NADA', 'NADA', b'1', '3423424');

CREATE TABLE IF NOT EXISTS `estudiantepreceptorias` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdEstudiante` int NOT NULL,
  `TipoPreceptoriaId` int DEFAULT NULL,
  `Fecha` date NOT NULL,
  `ProcesosTrabajados` text COLLATE utf8mb4_unicode_ci,
  `ProcesosDificultad` text COLLATE utf8mb4_unicode_ci,
  `MetasSiguientes` text COLLATE utf8mb4_unicode_ci,
  `Recomendaciones` text COLLATE utf8mb4_unicode_ci,
  `EstadoPreceptoria` tinyint DEFAULT NULL,
  `FechaCreacion` datetime DEFAULT CURRENT_TIMESTAMP,
  `FechaActualizacion` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdEstudiante` (`IdEstudiante`),
  KEY `TipoPreceptoriaId` (`TipoPreceptoriaId`),
  CONSTRAINT `estudiantepreceptorias_ibfk_1` FOREIGN KEY (`IdEstudiante`) REFERENCES `estudiantes` (`IdEstudiante`),
  CONSTRAINT `estudiantepreceptorias_ibfk_2` FOREIGN KEY (`TipoPreceptoriaId`) REFERENCES `cattipopreceptoria` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `estudiantepreceptorias` (`Id`, `IdEstudiante`, `TipoPreceptoriaId`, `Fecha`, `ProcesosTrabajados`, `ProcesosDificultad`, `MetasSiguientes`, `Recomendaciones`, `EstadoPreceptoria`, `FechaCreacion`, `FechaActualizacion`) VALUES
	(1, 2, 1, '2025-12-29', 'ASDAS', 'SDASD', 'SADASDA', 'SDAS', 2, '2025-12-29 11:12:34', NULL),
	(2, 2, 1, '2025-12-29', 'nada', 'leer', 'lectura', 'app', 1, '2025-12-29 11:27:07', NULL);


CREATE TABLE IF NOT EXISTS `permisos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Modulo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=129 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `permisos` (`Id`, `Nombre`, `Modulo`) VALUES
	(65, 'ESTUDIANTES_INDEX', 'ESTUDIANTES'),
	(66, 'ESTUDIANTES_CREAR', 'ESTUDIANTES'),
	(67, 'ESTUDIANTES_EDITAR', 'ESTUDIANTES'),
	(68, 'ESTUDIANTES_VER', 'ESTUDIANTES'),
	(69, 'ESTUDIANTES_ELIMINAR', 'ESTUDIANTES'),
	(70, 'EXPEDIENTES_INDEX', 'EXPEDIENTES'),
	(71, 'EXPEDIENTES_CREAR', 'EXPEDIENTES'),
	(72, 'EXPEDIENTES_EDITAR', 'EXPEDIENTES'),
	(73, 'EXPEDIENTES_VER', 'EXPEDIENTES'),
	(74, 'EXPEDIENTES_ELIMINAR', 'EXPEDIENTES'),
	(75, 'DOCUMENTOS_INDEX', 'DOCUMENTOS'),
	(76, 'DOCUMENTOS_CREAR', 'DOCUMENTOS'),
	(77, 'DOCUMENTOS_EDITAR', 'DOCUMENTOS'),
	(78, 'DOCUMENTOS_VER', 'DOCUMENTOS'),
	(79, 'DOCUMENTOS_ELIMINAR', 'DOCUMENTOS'),
	(80, 'ANAMNESIS_INDEX', 'ANAMNESIS'),
	(81, 'ANAMNESIS_CREAR', 'ANAMNESIS'),
	(82, 'ANAMNESIS_EDITAR', 'ANAMNESIS'),
	(83, 'ANAMNESIS_VER', 'ANAMNESIS'),
	(84, 'ANAMNESIS_ELIMINAR', 'ANAMNESIS'),
	(85, 'PRECEPTORIA_INDEX', 'PRECEPTORIA'),
	(86, 'PRECEPTORIA_CREAR', 'PRECEPTORIA'),
	(87, 'PRECEPTORIA_EDITAR', 'PRECEPTORIA'),
	(88, 'PRECEPTORIA_VER', 'PRECEPTORIA'),
	(89, 'PRECEPTORIA_ELIMINAR', 'PRECEPTORIA'),
	(90, 'PSICOLOGICO_INDEX', 'PSICOLOGICO'),
	(91, 'PSICOLOGICO_CREAR', 'PSICOLOGICO'),
	(92, 'PSICOLOGICO_EDITAR', 'PSICOLOGICO'),
	(93, 'PSICOLOGICO_VER', 'PSICOLOGICO'),
	(94, 'PSICOLOGICO_ELIMINAR', 'PSICOLOGICO'),
	(95, 'POSTCLASE_INDEX', 'POSTCLASE'),
	(96, 'POSTCLASE_CREAR', 'POSTCLASE'),
	(97, 'POSTCLASE_EDITAR', 'POSTCLASE'),
	(98, 'POSTCLASE_VER', 'POSTCLASE'),
	(99, 'POSTCLASE_ELIMINAR', 'POSTCLASE'),
	(100, 'USUARIO_INDEX', 'USUARIO'),
	(101, 'USUARIO_CREAR', 'USUARIO'),
	(102, 'USUARIO_EDITAR', 'USUARIO'),
	(103, 'USUARIO_VER', 'USUARIO'),
	(104, 'USUARIO_ELIMINAR', 'USUARIO'),
	(105, 'SEGURIDAD_USUARIO', 'USUARIO'),
	(106, 'MENU_ESTUDIANTES_GESTION', 'ESTUDIANTES'),
	(107, 'MENU_EXPEDIENTE_GENERAL', 'EXPEDIENTES'),
	(108, 'MENU_EXPEDIENTE_DOCUMENTOS', 'EXPEDIENTES'),
	(109, 'MENU_EXPEDIENTE_CRONOLOGIA', 'EXPEDIENTES'),
	(110, 'EXPEDIENTE_VER', 'EXPEDIENTES'),
	(111, 'EXPEDIENTE_EDITAR', 'EXPEDIENTES'),
	(112, 'DOCUMENTO_SUBIR', 'DOCUMENTOS'),
	(113, 'DOCUMENTO_VER', 'DOCUMENTOS'),
	(114, 'DOCUMENTO_DESCARGAR', 'DOCUMENTOS'),
	(115, 'DOCUMENTO_ELIMINAR', 'DOCUMENTOS'),
	(116, 'MENU_ANAMNESIS_REGISTRAR', 'ANAMNESIS'),
	(117, 'MENU_ANAMNESIS_HISTORIAL', 'ANAMNESIS'),
	(118, 'MENU_PRECEPTORIA_REGISTRAR', 'PRECEPTORIA'),
	(119, 'MENU_PRECEPTORIA_HISTORIAL', 'PRECEPTORIA'),
	(120, 'MENU_PSICOLOGICO_NUEVO', 'PSICOLOGICO'),
	(121, 'MENU_PSICOLOGICO_HISTORIAL', 'PSICOLOGICO'),
	(122, 'MENU_POSTCLASE_NUEVO', 'POSTCLASE'),
	(123, 'MENU_POSTCLASE_HISTORIAL', 'POSTCLASE'),
	(124, 'MENU_USUARIOS', 'SEGURIDAD'),
	(125, 'USUARIOS_CREAR', 'SEGURIDAD'),
	(126, 'USUARIOS_EDITAR', 'SEGURIDAD'),
	(127, 'USUARIOS_VER', 'SEGURIDAD'),
	(128, 'USUARIOS_ELIMINAR', 'SEGURIDAD');

CREATE TABLE IF NOT EXISTS `usuarios` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DUI` varchar(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellido` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Email` varchar(120) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Password` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '',
  `FechRegistro` datetime NOT NULL,
  `FechaValidez` datetime NOT NULL,
  `CargoDirectiva` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Estado` tinyint DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `usuarios` (`Id`, `DUI`, `Nombre`, `Apellido`, `Email`, `Password`, `FechRegistro`, `FechaValidez`, `CargoDirectiva`, `Estado`) VALUES
	(3, '000000000', 'Super', 'Administrador', 'root@admin.com', 'root123', '2025-11-23 22:17:05', '2035-11-23 22:17:05', NULL, 1),
	(4, '000000004', 'WALTER ERNESTO', 'Mena Calderon ', 'admindev@gmail.com', 'admin123', '2025-11-24 08:10:40', '2030-11-24 08:10:40', NULL, 1);


CREATE TABLE IF NOT EXISTS `usuariopermisos` (
  `IdUsuario` int NOT NULL,
  `IdPermiso` int NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdPermiso`),
  KEY `IdPermiso` (`IdPermiso`),
  CONSTRAINT `usuariopermisos_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`Id`),
  CONSTRAINT `usuariopermisos_ibfk_2` FOREIGN KEY (`IdPermiso`) REFERENCES `permisos` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `usuariopermisos` (`IdUsuario`, `IdPermiso`) VALUES
	(3, 65),
	(3, 66),
	(3, 67),
	(3, 68),
	(3, 69),
	(3, 70),
	(3, 71),
	(3, 72),
	(3, 73),
	(3, 74),
	(3, 75),
	(3, 76),
	(3, 77),
	(3, 78),
	(3, 79),
	(3, 80),
	(3, 81),
	(3, 82),
	(3, 83),
	(3, 84),
	(3, 85),
	(3, 86),
	(3, 87),
	(3, 88),
	(3, 89),
	(3, 90),
	(3, 91),
	(3, 92),
	(3, 93),
	(3, 94),
	(3, 95),
	(3, 96),
	(3, 97),
	(3, 98),
	(3, 99),
	(3, 100),
	(3, 101),
	(3, 102),
	(3, 103),
	(3, 104),
	(3, 105),
	(3, 106),
	(3, 107),
	(3, 108),
	(3, 109),
	(3, 110),
	(3, 111),
	(3, 112),
	(3, 113),
	(3, 114),
	(3, 115),
	(3, 116),
	(3, 117),
	(3, 118),
	(3, 119),
	(3, 120),
	(3, 121),
	(3, 122),
	(3, 123),
	(3, 124),
	(3, 125),
	(3, 126),
	(3, 127),
	(3, 128),
	(4, 65),
	(4, 66),
	(4, 67),
	(4, 68),
	(4, 69),
	(4, 106),
	(4, 107),
	(4, 108);

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;






CREATE TABLE `preceptoriaadjuntos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PreceptoriaId` int NOT NULL,
  `NombreArchivo` varchar(255) NOT NULL,
  `Contenido` longblob NOT NULL,
  `ContentType` varchar(100) DEFAULT NULL,
  `FechaCreacion` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_PreceptoriaAdjuntos_PreceptoriaId` (`PreceptoriaId`),
  CONSTRAINT `FK_PreceptoriaAdjuntos_EstudiantePreceptorias` FOREIGN KEY (`PreceptoriaId`) REFERENCES `estudiantepreceptorias` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=UTF8MB4;


CREATE TABLE `anamnesisadjuntos` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AnamnesisId` int NOT NULL,
  `NombreArchivo` varchar(255) NOT NULL,
  `Contenido` longblob NOT NULL,
  `ContentType` varchar(100) DEFAULT NULL,
  `FechaCreacion` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AnamnesisAdjuntos_AnamnesisId` (`AnamnesisId`),
  CONSTRAINT `FK_AnamnesisAdjuntos_Anamnesis` FOREIGN KEY (`AnamnesisId`) REFERENCES `anamnesis` (`IdAnamnesis`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;