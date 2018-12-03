-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         10.2.13-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             9.5.0.5314
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Volcando estructura de base de datos para puntoventas
CREATE DATABASE IF NOT EXISTS `puntoventas` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `puntoventas`;

-- Volcando estructura para tabla puntoventas.cajas
CREATE TABLE IF NOT EXISTS `cajas` (
  `IdCaja` int(11) NOT NULL AUTO_INCREMENT,
  `Caja` int(11) DEFAULT NULL,
  `Estado` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`IdCaja`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.cajasregistros
CREATE TABLE IF NOT EXISTS `cajasregistros` (
  `IdCajaTempo` int(11) NOT NULL AUTO_INCREMENT,
  `IdUsuario` int(11) DEFAULT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `Apellido` varchar(50) DEFAULT NULL,
  `Usuario` varchar(50) DEFAULT NULL,
  `Rol` varchar(50) DEFAULT NULL,
  `IdCaja` int(11) DEFAULT NULL,
  `Caja` int(11) DEFAULT NULL,
  `Estado` tinyint(1) DEFAULT NULL,
  `Hora` varchar(50) DEFAULT NULL,
  `Fecha` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdCajaTempo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.clientes
CREATE TABLE IF NOT EXISTS `clientes` (
  `IdCliente` int(11) NOT NULL AUTO_INCREMENT,
  `Id` varchar(50) DEFAULT NULL,
  `Nombre` varchar(50) DEFAULT NULL,
  `Apellido` varchar(50) DEFAULT NULL,
  `Direccion` varchar(50) DEFAULT NULL,
  `Telefono` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`IdCliente`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.proveedores
CREATE TABLE IF NOT EXISTS `proveedores` (
  `IdProveedor` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT NULL,
  `Telefono` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdProveedor`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.reportesclientes
CREATE TABLE IF NOT EXISTS `reportesclientes` (
  `idRegistro` int(11) NOT NULL AUTO_INCREMENT,
  `idCliente` int(11) DEFAULT NULL,
  `SaldoActual` varchar(50) DEFAULT NULL,
  `FechaActual` varchar(50) DEFAULT NULL,
  `UltimoPago` varchar(50) DEFAULT NULL,
  `FechaPago` varchar(50) DEFAULT NULL,
  `Id` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idRegistro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.reportesproveedores
CREATE TABLE IF NOT EXISTS `reportesproveedores` (
  `idRegistro` int(11) NOT NULL AUTO_INCREMENT,
  `idProveedor` int(11) DEFAULT NULL,
  `SaldoActual` varchar(50) DEFAULT NULL,
  `FechaActual` varchar(50) DEFAULT NULL,
  `UltimoPago` varchar(50) DEFAULT NULL,
  `FechaPago` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`idRegistro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
-- Volcando estructura para tabla puntoventas.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `IdUsuario` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) DEFAULT NULL,
  `Apellido` varchar(50) DEFAULT NULL,
  `Telefono` varchar(50) DEFAULT NULL,
  `Direccion` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Usuario` varchar(50) DEFAULT NULL,
  `Password` varchar(250) DEFAULT NULL,
  `Rol` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdUsuario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- La exportación de datos fue deseleccionada.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
