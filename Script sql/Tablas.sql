/* -- Si existe, poner en SINGLE_USER y eliminar
use master;
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'GestionHotelera')
BEGIN
    ALTER DATABASE GestionHotelera SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE GestionHotelera;
END;
*/
-- Crear la base de datos de nuevo
CREATE DATABASE GestionHotelera;
GO
USE GestionHotelera;
GO

create table TipoHospedaje (
    idTipoHospedaje int identity(1,1) primary key,
    nombre varchar(50) not null
);

create table EmpresaHospedaje (
    idEmpresaHospedaje int identity(1,1) primary key,
    nombre varchar(100) not null,
    cedulaJuridica varchar(15) unique not null,
    idTipoHospedaje int not null,
    provincia varchar(30) not null,
    canton varchar(30) not null,
    distrito varchar(30) not null,
    barrio varchar(30),
    senas varchar(250) not null,
    latitud decimal(10, 8),
    longitud decimal(11, 8),
    correo varchar(40) unique,
    foreign key (idTipoHospedaje) references TipoHospedaje(idTipoHospedaje)
);

create table FotosEmpresaHospedaje (
    idFoto int identity(1,1) primary key,
    idEmpresaHospedaje int not null,
    rutaLocal varchar(300) not null,
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje)
);

create table Cliente (
    idCliente int identity(1,1) primary key,
    nombre varchar(50) not null,
    apellido1 varchar(50) not null,
    apellido2 varchar(50) not null,
    fechaNacimiento date not null,
    tipoIdentificacion varchar(20) not null,
    identificacion varchar(30) unique not null,
    pais varchar(30) not null,
    provincia varchar(30),
    canton varchar(30),
    distrito varchar(30),
    correo varchar(40) unique not null
);

create table TipoHabitacion (
    idTipo int identity(1,1) primary key,
    nombre varchar(50) not null,
    descripcion varchar(150) not null,
    tipoCama varchar(50) not null,
    precio decimal(10, 2) check (precio >= 0) not null
);

create table Habitacion (
    idHabitacion int identity(1,1) primary key,
    idEmpresaHospedaje int not null,
    numero int check (numero > 0) not null,
    idTipoHabitacion int not null,
    constraint uq_habitacion_numero_empresa unique (idEmpresaHospedaje, numero),
    foreign key (idTipoHabitacion) references TipoHabitacion(idTipo),
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje) on delete cascade
);

create table Reserva (
    idReserva int identity(10000000,1) primary key,
    idCliente int not null,
    idEmpresaHospedaje int not null,
    idHabitacion int not null,
    fechaIngreso datetime not null,
    cantidadPersonas int not null,
    tieneVehiculo bit not null,
    fechaSalida date not null,
    horaSalida time not null default '12:00:00',
    estado varchar(20) not null default 'ACTIVA',
    check (fechaSalida >= fechaIngreso),
    check (horaSalida <= '12:00:00'),
    foreign key (idCliente) references Cliente(idCliente),
    foreign key (idHabitacion) references Habitacion(idHabitacion) on delete cascade,
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje)
);

create table ReservasEmpresaHospedaje (
    idReservaHospedaje int identity(1,1) primary key,
    idReserva int not null,
    foreign key (idReserva) references Reserva(idReserva) on delete cascade
);

create table TelefonosEmpresa (
    idTelefono int identity(1,1) primary key,
    idEmpresaHospedaje int not null,
    numero varchar(20) not null,
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje) on delete cascade
);

create table TipoRedSocial (
    idTipoRed int identity(1,1) primary key,
    nombre varchar(30) not null
);

create table Redes (
    idRed int identity(1,1) primary key,
    idEmpresaHospedaje int not null,
    idTipoRed int not null,
    url varchar(300) not null,
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje) on delete cascade,
    foreign key (idTipoRed) references TipoRedSocial(idTipoRed)
);

create table TipoServicioHospedaje (
    idTipoServicio int identity(1,1) primary key,
    nombre varchar(50) not null
);

create table ServiciosHospedaje (
    idServicio int identity(1,1) primary key,
    idEmpresaHospedaje int not null,
    idTipoServicio int not null,
    foreign key (idEmpresaHospedaje) references EmpresaHospedaje(idEmpresaHospedaje) on delete cascade,
    foreign key (idTipoServicio) references TipoServicioHospedaje(idTipoServicio)
);

create table Comodidades (
    idComodidad int identity(1,1) primary key,
    idTipoHabitacion int not null,
    comodidad varchar(50) not null,
    foreign key (idTipoHabitacion) references TipoHabitacion(idTipo)
);

create table FotosTipoHabitacion (
    idFoto int identity(1,1) primary key,
    idTipoHabitacion int not null,
    rutaLocal varchar(300) not null,
    foreign key (idTipoHabitacion) references TipoHabitacion(idTipo) on delete cascade
);

create table TelefonosCliente (
    idTelefono int identity(1,1) primary key,
    idCliente int not null,
    numero varchar(20) not null,
    foreign key (idCliente) references Cliente(idCliente) on delete cascade
);

create table Factura (
    idFactura int identity(1000, 1) primary key,
    idReserva int not null,
    fecha datetime not null default getdate(),
    importeTotal decimal(10, 2) not null,
    formaPago varchar(20) not null,
    foreign key (idReserva) references Reserva(idReserva) on delete cascade
);

create table TipoActividad (
    idTipoActividad int identity(1,1) primary key,
    nombre varchar(50) not null
);

create table EmpresaRecreacion (
    idEmpresaRecreacion int identity(1,1) primary key,
    nombre varchar(100) not null,
    cedulaJuridica varchar(15) unique not null,
    correo varchar(40) unique not null,
    telefono varchar(20) not null,
    encargado varchar(100) not null,
    provincia varchar(30) not null,
    canton varchar(30) not null,
    distrito varchar(30) not null,
    senas varchar(250) not null,
    latitud decimal(10, 8),
    longitud decimal(11, 8)
);

create table FotosEmpresaRecreacion (
    idFoto int identity(1,1) primary key,
    idEmpresaRecreacion int not null,
    rutaLocal varchar(300) not null,
    foreign key (idEmpresaRecreacion) references EmpresaRecreacion(idEmpresaRecreacion) on delete cascade
);

create table Actividad (
    idActividad int identity(1,1) primary key,
    idTipoActividad int not null,
    idEmpresaRecreacion int not null,
    descripcion varchar(150) not null,
    precio decimal(10, 2) not null check (precio >= 0),
    foreign key (idTipoActividad) references TipoActividad(idTipoActividad),
    foreign key (idEmpresaRecreacion) references EmpresaRecreacion(idEmpresaRecreacion)
);

create table TipoServicio (
    idTipoServicio int identity(1,1) primary key,
    nombre varchar(50) not null
);

create table Servicio (
    idServicio int identity(1,1) primary key,
    idTipoServicio int not null,
    idEmpresaRecreacion int not null,
    foreign key (idTipoServicio) references TipoServicio(idTipoServicio),
    foreign key (idEmpresaRecreacion) references EmpresaRecreacion(idEmpresaRecreacion)
);

-- TABLAS DE USUARIOS Y RELACIONES
CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombreUsuario VARCHAR(50) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(20) CHECK (rol IN ('admin', 'usuario')) NOT NULL DEFAULT 'usuario'
);

CREATE TABLE UsuarioEmpresa (
    idUsuarioEmpresa INT PRIMARY KEY IDENTITY(1,1),
    idUsuario INT NOT NULL,
    idEmpresaHospedaje INT NOT NULL,
    fechaAsignacion DATETIME DEFAULT GETDATE(),
    activo BIT DEFAULT 1,
    CONSTRAINT uq_usuario_empresa UNIQUE (idUsuario, idEmpresaHospedaje),
    FOREIGN KEY (idUsuario) REFERENCES Usuarios(idUsuario) ON DELETE CASCADE,
    FOREIGN KEY (idEmpresaHospedaje) REFERENCES EmpresaHospedaje(idEmpresaHospedaje) ON DELETE CASCADE
);