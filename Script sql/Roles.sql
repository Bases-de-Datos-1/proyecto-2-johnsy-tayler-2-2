USE GestionHotelera;
GO

-- TABLAS DE USUARIOS Y RELACIONES
CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombreUsuario VARCHAR(50) UNIQUE NOT NULL,
    contrasena VARCHAR(255) NOT NULL,
    rol VARCHAR(20) CHECK (rol IN ('admin', 'usuario')) NOT NULL DEFAULT 'usuario'
);
GO

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
GO

-- CREACIÓN DE ROLES
CREATE ROLE rol_admin;
CREATE ROLE rol_usuario;
GO

-- PERMISOS PARA ADMIN

-- Tablas
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Cliente TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.EmpresaHospedaje TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.EmpresaRecreacion TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Reserva TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Factura TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Habitacion TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.TipoHabitacion TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Comodidades TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.ServiciosHospedaje TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Servicio TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.Actividad TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.UsuarioEmpresa TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.ReservasEmpresaHospedaje TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.FotosEmpresaHospedaje TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.FotosEmpresaRecreacion TO rol_admin;
GRANT SELECT, INSERT, UPDATE, DELETE ON dbo.FotosTipoHabitacion TO rol_admin;
GRANT SELECT ON dbo.Usuarios TO rol_admin;

-- Vistas
GRANT SELECT ON dbo.VW_InfoCompletaHospedaje TO rol_admin;
GRANT SELECT ON dbo.VW_InfoCompletaEmpresaRecreacion TO rol_admin;
GRANT SELECT ON dbo.VW_HabitacionesDisponibles TO rol_admin;
GRANT SELECT ON dbo.VW_BusquedaHospedajes TO rol_admin;
GRANT SELECT ON dbo.VW_DetalleReservas TO rol_admin;
GRANT SELECT ON dbo.VW_ReporteFacturacion TO rol_admin;
GRANT SELECT ON dbo.VW_ActividadesRecreativas TO rol_admin;
GRANT SELECT ON dbo.VW_HabitacionesConComodidades TO rol_admin;
GRANT SELECT ON dbo.VW_ReservasFinalizadasPorTipo TO rol_admin;
GRANT SELECT ON dbo.VW_RangoEdadesClientes TO rol_admin;
GRANT SELECT ON dbo.VW_HotelesDemanda TO rol_admin;
GRANT SELECT ON dbo.VW_HotelesUsuario TO rol_admin;
GRANT SELECT ON dbo.VW_AdminsPorHotel TO rol_admin;
GRANT SELECT ON dbo.VW_LoginUsuarios TO rol_admin;
GRANT SELECT ON dbo.VW_FotosEmpresaHospedaje TO rol_admin;
GRANT SELECT ON dbo.VW_FotosEmpresaRecreacion TO rol_admin;
GRANT SELECT ON dbo.VW_FotosTipoHabitacion TO rol_admin;


-- Procedures
-- Insertar, actualizar, eliminar
GRANT EXECUTE ON dbo.SP_InsertarEmpresaHospedaje TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarEmpresaRecreacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarFactura TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarReserva TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarCliente TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarTipoHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarComodidad TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarActividad TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarServicio_hospedaje TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarServicio TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarRedSocial TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarTelefonoEmpresa TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarTelefonoCliente TO rol_admin;

GRANT EXECUTE ON dbo.SP_ActualizarHospedaje TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarReserva TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarFactura TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarCliente TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarActividad TO rol_admin;

GRANT EXECUTE ON dbo.SP_EliminarHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_EliminarActividad TO rol_admin;

-- Gestión de imágenes
GRANT EXECUTE ON dbo.SP_InsertarFotoEmpresaHospedaje TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarFotoEmpresaRecreacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarFotoTipoHabitacion TO rol_admin;

-- Consultas y filtros
GRANT EXECUTE ON dbo.SP_BuscarHospedajesConFiltros TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarHospedajesPorNombre TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorCliente TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasConFiltros TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorImporte TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorFecha TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorPago TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorTipoHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorNumeroHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorIdentificacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_ReservasFinalizadasPorTipo TO rol_admin;
GRANT EXECUTE ON dbo.SP_ClientesPorRangoEdad TO rol_admin;
GRANT EXECUTE ON dbo.SP_HotelesDemandaZona TO rol_admin;
GRANT EXECUTE ON dbo.SP_InfoCompletaHospedaje_PorCedula TO rol_admin;
GRANT EXECUTE ON dbo.SP_InfoEmpresaRecreacionPorCedula TO rol_admin;
GRANT EXECUTE ON dbo.SP_ObtenerUltimaFacturaPorHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_ReservasPorHotel TO rol_admin;
GRANT EXECUTE ON dbo.SP_ReservasPorCliente TO rol_admin;
GRANT EXECUTE ON dbo.SP_ReservasPorFecha TO rol_admin;

-- Usuarios y gestión
GRANT EXECUTE ON dbo.SP_ListarUsuarios TO rol_admin;
GRANT EXECUTE ON dbo.SP_AsignarRolUsuario TO rol_admin;
GRANT EXECUTE ON dbo.SP_AsignarAdminHotel TO rol_admin;
GRANT EXECUTE ON dbo.SP_VerificarAdminHotel TO rol_admin;
GRANT EXECUTE ON dbo.SP_MostrarHotelesUsuario TO rol_admin;
GRANT EXECUTE ON dbo.SP_CambiarEstadoReserva TO rol_admin;

-- Autenticación
GRANT EXECUTE ON dbo.SP_LoginUsuario TO rol_admin;
GRANT EXECUTE ON dbo.SP_VerificarUsuario TO rol_admin;
GRANT EXECUTE ON dbo.SP_VerificarContrasena TO rol_admin;
GRANT EXECUTE ON dbo.SP_BuscarClientePorCedula TO rol_admin;
GRANT EXECUTE ON dbo.SP_InfoCompletaCliente TO rol_admin;
GO

-- PERMISOS PARA USUARIO
-- Tablas
GRANT SELECT ON dbo.Habitacion TO rol_usuario;
GRANT SELECT ON dbo.EmpresaHospedaje TO rol_usuario;
GRANT SELECT ON dbo.TipoHabitacion TO rol_usuario;
GRANT SELECT ON dbo.TipoHospedaje TO rol_usuario;
GRANT SELECT ON dbo.Comodidades TO rol_usuario;
GRANT SELECT ON dbo.ServiciosHospedaje TO rol_usuario;
GRANT SELECT ON dbo.TipoServicioHospedaje TO rol_usuario;
GRANT SELECT ON dbo.Actividad TO rol_usuario;
GRANT SELECT ON dbo.TipoActividad TO rol_usuario;
GRANT SELECT ON dbo.Servicio TO rol_usuario;
GRANT SELECT ON dbo.TipoServicio TO rol_usuario;

-- Vistas
GRANT SELECT ON dbo.VW_BusquedaHospedajes TO rol_usuario;
GRANT SELECT ON dbo.VW_LoginUsuarios TO rol_usuario;
GRANT SELECT ON dbo.VW_ActividadesRecreativas TO rol_usuario;
GRANT SELECT ON dbo.VW_HabitacionesDisponibles TO rol_usuario;
GRANT SELECT ON dbo.VW_HabitacionesConComodidades TO rol_usuario;
GRANT SELECT ON dbo.VW_InfoCompletaHospedaje TO rol_usuario;
GRANT SELECT ON dbo.VW_InfoCompletaEmpresaRecreacion TO rol_usuario;
GRANT SELECT ON dbo.VW_FotosEmpresaHospedaje TO rol_usuario;
GRANT SELECT ON dbo.VW_FotosEmpresaRecreacion TO rol_usuario;
GRANT SELECT ON dbo.VW_FotosTipoHabitacion TO rol_usuario;


-- Procedures
GRANT EXECUTE ON dbo.SP_VerificarContrasena TO rol_usuario;
GRANT EXECUTE ON dbo.SP_LoginUsuario TO rol_usuario;
GRANT EXECUTE ON dbo.SP_VerificarUsuario TO rol_usuario;
GRANT EXECUTE ON dbo.SP_MisReservas TO rol_usuario;
GRANT EXECUTE ON dbo.SP_CambiarContrasena TO rol_usuario;
GRANT EXECUTE ON dbo.SP_InsertarReserva TO rol_usuario;
GRANT EXECUTE ON dbo.SP_BuscarHospedajesConFiltros TO rol_usuario;
GRANT EXECUTE ON dbo.SP_InfoCompletaHospedaje_PorCedula TO rol_usuario;
GRANT EXECUTE ON dbo.SP_BuscarHospedajesPorNombre TO rol_usuario;
GO

-- VISTA LOGIN USUARIOS
CREATE VIEW VW_LoginUsuarios AS
SELECT idUsuario, nombreUsuario, rol
FROM Usuarios;
GO

-- PROCEDIMIENTOS DE USUARIO

-- Registrar usuario
CREATE PROCEDURE SP_RegistrarUsuario
    @nombreUsuario VARCHAR(50),
    @contrasena VARCHAR(255),
    @rol VARCHAR(20) = 'usuario'
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Usuarios WHERE nombreUsuario = @nombreUsuario)
        BEGIN
            RAISERROR('El nombre de usuario ya existe.', 16, 1);
            RETURN;
        END

        INSERT INTO Usuarios (nombreUsuario, contrasena, rol)
        VALUES (@nombreUsuario, @contrasena, @rol);
        
        SELECT 'Usuario registrado exitosamente' AS Mensaje, SCOPE_IDENTITY() AS idUsuario;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
-- LOGINS Y USUARIOS
-- Verificar login
CREATE PROCEDURE SP_LoginUsuario
    @nombreUsuario VARCHAR(50),
    @contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT idUsuario, nombreUsuario, rol
    FROM Usuarios
    WHERE nombreUsuario = @nombreUsuario AND contrasena = @contrasena;
END;
GO

-- Verificar existencia de usuario
CREATE PROCEDURE SP_VerificarUsuario
    @nombreUsuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Usuarios WHERE nombreUsuario = @nombreUsuario)
        SELECT 1 AS Existe;
    ELSE
        SELECT 0 AS Existe;
END;
GO

-- Verificar contraseña de usuario
CREATE PROCEDURE SP_VerificarContrasena
    @nombreUsuario VARCHAR(50),
    @contrasena VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @idUsuario INT;
    DECLARE @rolUsuario VARCHAR(20);
    
    SELECT 
        @idUsuario = idUsuario,
        @rolUsuario = rol
    FROM Usuarios 
    WHERE nombreUsuario = @nombreUsuario AND contrasena = @contrasena;
    
    IF @idUsuario IS NOT NULL
    BEGIN
        SELECT 
            @idUsuario AS idUsuario,
            @nombreUsuario AS nombreUsuario,
            @rolUsuario AS rol,
            1 AS loginExitoso,
            'Inicio de sesion exitoso' AS mensaje;
    END
    ELSE
    BEGIN
        SELECT 
            NULL AS idUsuario,
            NULL AS nombreUsuario,
            NULL AS rol,
            0 AS loginExitoso,
            'Credenciales incorrectas' AS mensaje;
    END
END;
GO

-- PROCEDURES DE ADMIN

-- PROCEDURE PARA ASIGNAR ADMIN A UN HOTEL
CREATE PROCEDURE SP_AsignarAdminHotel
    @idUsuario INT,
    @idEmpresaHospedaje INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE idUsuario = @idUsuario)
        BEGIN
            RAISERROR('El usuario no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM EmpresaHospedaje WHERE idEmpresaHospedaje = @idEmpresaHospedaje)
        BEGIN
            RAISERROR('La empresa de hospedaje no existe.', 16, 1);
            RETURN;
        END

        IF EXISTS (
            SELECT 1 FROM UsuarioEmpresa 
            WHERE idUsuario = @idUsuario AND idEmpresaHospedaje = @idEmpresaHospedaje AND activo = 1
        )
        BEGIN
            RAISERROR('El usuario ya es administrador de este hotel.', 16, 1);
            RETURN;
        END

        IF EXISTS (
            SELECT 1 FROM UsuarioEmpresa 
            WHERE idUsuario = @idUsuario AND idEmpresaHospedaje = @idEmpresaHospedaje AND activo = 0
        )
        BEGIN
            UPDATE UsuarioEmpresa 
            SET activo = 1, fechaAsignacion = GETDATE()
            WHERE idUsuario = @idUsuario AND idEmpresaHospedaje = @idEmpresaHospedaje;

            SELECT 'Administracion reactivada exitosamente' AS Mensaje;
        END
        ELSE
        BEGIN
            INSERT INTO UsuarioEmpresa (idUsuario, idEmpresaHospedaje)
            VALUES (@idUsuario, @idEmpresaHospedaje);

            SELECT 'Usuario asignado como administrador exitosamente' AS Mensaje;
        END
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- MOSTRAR HOTELES DE USUARIO ADMIN
CREATE PROCEDURE SP_MostrarHotelesUsuario
    @idUsuario INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        e.idEmpresaHospedaje,
        e.nombre AS nombreHotel,
        e.cedulaJuridica,
        CONCAT(e.provincia, ', ', e.canton, ', ', e.distrito) AS ubicacion,
        th.nombre AS tipoHospedaje,
        ue.fechaAsignacion,
        (SELECT COUNT(*) FROM Habitacion h WHERE h.idEmpresaHospedaje = e.idEmpresaHospedaje) AS totalHabitaciones
    FROM UsuarioEmpresa ue
    INNER JOIN EmpresaHospedaje e ON ue.idEmpresaHospedaje = e.idEmpresaHospedaje
    INNER JOIN TipoHospedaje th ON e.idTipoHospedaje = th.idTipoHospedaje
    WHERE ue.idUsuario = @idUsuario AND ue.activo = 1
    ORDER BY e.nombre;

    IF @@ROWCOUNT = 0
    BEGIN
        SELECT 'El usuario no administra ningun hotel actualmente.' AS Mensaje;
    END
END;
GO

-- VERIFICAR ADMIN HOTEL
CREATE PROCEDURE SP_VerificarAdminHotel
    @idUsuario INT,
    @idEmpresaHospedaje INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @esAdmin BIT = 0;
    DECLARE @nombreUsuario VARCHAR(50);
    DECLARE @nombreHotel VARCHAR(100);

    SELECT @nombreUsuario = nombreUsuario FROM Usuarios WHERE idUsuario = @idUsuario;
    SELECT @nombreHotel = nombre FROM EmpresaHospedaje WHERE idEmpresaHospedaje = @idEmpresaHospedaje;

    IF EXISTS (
        SELECT 1 FROM UsuarioEmpresa 
        WHERE idUsuario = @idUsuario AND idEmpresaHospedaje = @idEmpresaHospedaje AND activo = 1
    )
    BEGIN
        SET @esAdmin = 1;
    END

    SELECT 
        @esAdmin AS esAdministrador,
        @nombreUsuario AS nombreUsuario,
        @nombreHotel AS nombreHotel,
        CASE 
            WHEN @esAdmin = 1 THEN 'El usuario SI es administrador de este hotel'
            ELSE 'El usuario NO es administrador de este hotel'
        END AS mensaje;
END;
GO

-- PROCEDIMIENTO PARA CAMBIAR ROL
CREATE PROCEDURE SP_AsignarRolUsuario
    @nombreUsuario VARCHAR(50),
    @rolNuevo VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Usuarios WHERE nombreUsuario = @nombreUsuario)
        BEGIN
            RAISERROR('El usuario no existe.', 16, 1);
            RETURN;
        END

        IF @rolNuevo NOT IN ('admin', 'usuario')
        BEGIN
            RAISERROR('Rol invalido. Use: admin o usuario.', 16, 1);
            RETURN;
        END

        UPDATE Usuarios
        SET rol = @rolNuevo
        WHERE nombreUsuario = @nombreUsuario;

        SELECT 'Rol actualizado exitosamente' AS Mensaje,
               @nombreUsuario AS Usuario,
               @rolNuevo AS NuevoRol;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

-- MOSTRAR TODAS LAS RESERVAS DE UN CLIENTE
CREATE PROCEDURE SP_ReservasPorCliente (
    @idCliente INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.idReserva,
        eh.nombre AS Hotel,
        h.numero AS Habitacion,
        r.fechaIngreso,
        r.fechaSalida,
        r.cantidadPersonas,
        r.estado
    FROM Reserva r
    JOIN Habitacion h ON r.idHabitacion = h.idHabitacion
    JOIN EmpresaHospedaje eh ON r.idEmpresaHospedaje = eh.idEmpresaHospedaje
    WHERE r.idCliente = @idCliente
    ORDER BY r.fechaIngreso DESC;
END;
GO

-- MOSTRAR RESERVAS DENTRO DE RANGO DE FECHAS
CREATE PROCEDURE SP_ReservasPorFecha (
    @fechaInicio DATE,
    @fechaFin DATE
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.idReserva,
        c.nombre + ' ' + c.apellido1 AS Cliente,
        eh.nombre AS Hotel,
        r.fechaIngreso,
        r.fechaSalida,
        r.estado
    FROM Reserva r
    JOIN Cliente c ON r.idCliente = c.idCliente
    JOIN EmpresaHospedaje eh ON r.idEmpresaHospedaje = eh.idEmpresaHospedaje
    WHERE r.fechaIngreso BETWEEN @fechaInicio AND @fechaFin
    ORDER BY r.fechaIngreso;
END;
GO

-- LISTAR USUARIOS CON SU ROL
CREATE PROCEDURE SP_ListarUsuarios
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idUsuario,
        nombreUsuario,
        rol
    FROM Usuarios
    ORDER BY nombreUsuario;
END;
GO

-- CAMIBAR ESTADO DE UNA RESERVA (CANCELADA o FINALIZADA)
CREATE PROCEDURE SP_CambiarEstadoReserva (
    @idReserva INT,
    @nuevoEstado VARCHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @nuevoEstado NOT IN ('ACTIVA', 'CANCELADA')
    BEGIN
        RAISERROR('Estado invalido. Use: ACTIVA o CANCELADA .', 16, 1);
        RETURN;
    END

    UPDATE Reserva
    SET estado = @nuevoEstado
    WHERE idReserva = @idReserva;

    SELECT 'Estado actualizado correctamente' AS Mensaje;
END;
GO

-- PROCEDURES PARA USUARIO NORMAL
-- Ver todas las reservas asociadas a un usuario (cliente)
CREATE PROCEDURE SP_MisReservas (
    @idCliente INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.idReserva,
        eh.nombre AS Hotel,
        h.numero AS Habitacion,
        r.fechaIngreso,
        r.fechaSalida,
        r.estado,
        r.cantidadPersonas,
        r.tieneVehiculo
    FROM Reserva r
    JOIN EmpresaHospedaje eh ON r.idEmpresaHospedaje = eh.idEmpresaHospedaje
    JOIN Habitacion h ON r.idHabitacion = h.idHabitacion
    WHERE r.idCliente = @idCliente
    ORDER BY r.fechaIngreso DESC;
END;
GO

-- CAMIBAR CONTRASEÑA DE USUARIO AUTENTICADO
CREATE PROCEDURE SP_CambiarContrasena (
    @nombreUsuario VARCHAR(50),
    @contrasenaActual VARCHAR(255),
    @nuevaContrasena VARCHAR(255)
)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (
        SELECT 1 FROM Usuarios 
        WHERE nombreUsuario = @nombreUsuario AND contrasena = @contrasenaActual
    )
    BEGIN
        RAISERROR('La contraseña actual es incorrecta.', 16, 1);
        RETURN;
    END

    UPDATE Usuarios
    SET contrasena = @nuevaContrasena
    WHERE nombreUsuario = @nombreUsuario;

    SELECT 'Contraseña actualizada correctamente' AS Mensaje;
END;