USE GestionHotelera;
GO
-- Procedures para insertar
CREATE PROCEDURE SP_InsertarTipoHospedaje(
    @p_nombre varchar(50)
)
AS
BEGIN
    INSERT INTO TipoHospedaje (nombre)
    VALUES (@p_nombre);
END;
GO

-- PROCEDURE para insertar una empresa de hospedaje
CREATE PROCEDURE SP_InsertarEmpresaHospedaje(
    @p_nombre varchar(100),
    @p_cedulaJuridica varchar(15),
    @p_idTipoHospedaje int,
    @p_provincia varchar(30),
    @p_canton varchar(30),
    @p_distrito varchar(30),
    @p_barrio varchar(30),
    @p_senas varchar(100),
    @p_latitud decimal(10, 8),
    @p_longitud decimal(11, 8),
    @p_correo varchar(40)
)
AS
BEGIN
    INSERT INTO EmpresaHospedaje (nombre, cedulaJuridica, idTipoHospedaje, provincia, canton, distrito, barrio, senas, latitud, longitud, correo)
    VALUES (@p_nombre, @p_cedulaJuridica, @p_idTipoHospedaje, @p_provincia, @p_canton, @p_distrito, @p_barrio, @p_senas, @p_latitud, @p_longitud, @p_correo);
END;
GO

-- PROCEDURE para insertar un teléfono de empresa
CREATE PROCEDURE SP_InsertarTelefonoEmpresa(
    @p_idEmpresaHospedaje int,
    @p_numero varchar(20)
)
AS
BEGIN
    INSERT INTO TelefonosEmpresa (idEmpresaHospedaje, numero)
    VALUES (@p_idEmpresaHospedaje, @p_numero);
END;
GO

-- PROCEDURE para insertar una red social
CREATE PROCEDURE SP_InsertarRedSocial(
    @p_idEmpresaHospedaje int,
    @p_idTipoRed int,
    @p_url varchar(200)
)
AS
BEGIN
    INSERT INTO Redes (idEmpresaHospedaje, idTipoRed, url)
    VALUES (@p_idEmpresaHospedaje, @p_idTipoRed, @p_url);
END;
GO

-- PROCEDURE para insertar un servicio de hospedaje
CREATE PROCEDURE SP_InsertarServicio_hospedaje(
    @p_idEmpresaHospedaje int,
    @p_idTipoServicio int
)
AS
BEGIN
    INSERT INTO ServiciosHospedaje (idEmpresaHospedaje, idTipoServicio)
    VALUES (@p_idEmpresaHospedaje, @p_idTipoServicio);
END;
GO

-- PROCEDURE para insertar un tipo de habitación
CREATE PROCEDURE SP_InsertarTipoHabitacion (
    @idEmpresaHospedaje INT,
    @p_nombre VARCHAR(50),
    @p_descripcion VARCHAR(150),
    @p_tipoCama VARCHAR(50),
    @p_precio DECIMAL(10, 2)
)
AS
BEGIN
    INSERT INTO TipoHabitacion (idEmpresaHospedaje, nombre, descripcion, tipoCama, precio)
    VALUES (@idEmpresaHospedaje, @p_nombre, @p_descripcion, @p_tipoCama, @p_precio);
END;
GO

-- PROCEDURE para insertar una comodidad en habitación
CREATE PROCEDURE SP_InsertarComodidad(
    @p_idTipoHabitacion int,
    @p_comodidad varchar(50)
)
AS
BEGIN
    INSERT INTO Comodidades (idTipoHabitacion, comodidad)
    VALUES (@p_idTipoHabitacion, @p_comodidad);
END;
GO

-- PROCEDURE para insertar una foto de habitación
CREATE PROCEDURE SP_InsertarFotoHabitacion(
    @p_idTipoHabitacion int,
    @p_url varchar(200)
)
AS
BEGIN
    INSERT INTO FotosHabitacion (idTipoHabitacion, url)
    VALUES (@p_idTipoHabitacion, @p_url);
END;
GO

-- PROCEDURE para insertar una habitación
CREATE PROCEDURE SP_InsertarHabitacion(
    @p_idEmpresaHospedaje int,
    @p_numero int,
    @p_idTipoHabitacion int
)
AS
BEGIN
    INSERT INTO Habitacion (idEmpresaHospedaje, numero, idTipoHabitacion)
    VALUES (@p_idEmpresaHospedaje, @p_numero, @p_idTipoHabitacion);
END;
GO

-- PROCEDURE para insertar un Cliente
CREATE PROCEDURE SP_InsertarCliente(
    @p_nombre varchar(50),
    @p_apellido1 varchar(50),
    @p_apellido2 varchar(50),
    @p_fechaNacimiento date,
    @p_tipoIdentIFicacion varchar(20),
    @p_identIFicacion varchar(30),
    @p_pais varchar(30),
    @p_provincia varchar(30),
    @p_canton varchar(30),
    @p_distrito varchar(30),
    @p_correo varchar(40)
)
AS
BEGIN
    INSERT INTO Cliente (nombre, apellido1, apellido2, fechaNacimiento, tipoIdentIFicacion, identIFicacion, pais, provincia, canton, distrito, correo)
    VALUES (@p_nombre, @p_apellido1, @p_apellido2, @p_fechaNacimiento, @p_tipoIdentIFicacion, @p_identIFicacion, @p_pais, @p_provincia, @p_canton, @p_distrito, @p_correo);
END;
GO

-- PROCEDURE para insertar un teléfono de Cliente
CREATE PROCEDURE SP_InsertarTelefonoCliente(
    @p_idCliente int,
    @p_numero varchar(20)
)
AS
BEGIN
    INSERT INTO TelefonosCliente (idCliente, numero)
    VALUES (@p_idCliente, @p_numero);
END;
GO

CREATE PROCEDURE SP_InsertarReserva(
    @p_idCliente INT,
    @p_idEmpresaHospedaje INT,
    @p_idHabitacion INT,
    @p_fechaIngreso DATETIME,
    @p_cantidadPersonas INT,
    @p_tieneVehiculo BIT,
    @p_fechaSalida DATE,
    @p_horaSalida TIME
)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO Reserva (
            idCliente, 
            idEmpresaHospedaje, 
            idHabitacion, 
            fechaIngreso, 
            cantidadPersonas, 
            tieneVehiculo, 
            fechaSalida, 
            horaSalida
        )
        VALUES (
            @p_idCliente, 
            @p_idEmpresaHospedaje, 
            @p_idHabitacion, 
            @p_fechaIngreso, 
            @p_cantidadPersonas, 
            @p_tieneVehiculo, 
            @p_fechaSalida, 
            @p_horaSalida
        );

        SELECT 
            SCOPE_IDENTITY() AS idReserva,
            'Reserva creada exitosamente' AS Mensaje;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE SP_InsertarFactura (
    @p_idReserva INT,
    @p_importeTotal DECIMAL(10, 2),
    @p_formaPago VARCHAR(20)
)
AS
BEGIN
    INSERT INTO Factura (idReserva, importeTotal, formaPago)
    VALUES (@p_idReserva, @p_importeTotal, @p_formaPago);
END;
GO


-- PROCEDURE para insertar una empresa de recreación
CREATE PROCEDURE SP_InsertarEmpresaRecreacion(
    @p_nombre varchar(100),
    @p_cedulaJuridica varchar(15),
    @p_correo varchar(40),
    @p_telefono varchar(20),
    @p_encargado varchar(100),
    @p_provincia varchar(30),
    @p_canton varchar(30),
    @p_distrito varchar(30),
    @p_senas varchar(100),
    @p_latitud decimal(10, 8),
    @p_longitud decimal(11, 8)
)
AS
BEGIN
    INSERT INTO EmpresaRecreacion (nombre, cedulaJuridica, correo, telefono, encargado, provincia, canton, distrito, senas, latitud, longitud)
    VALUES (@p_nombre, @p_cedulaJuridica, @p_correo, @p_telefono, @p_encargado, @p_provincia, @p_canton, @p_distrito, @p_senas, @p_latitud, @p_longitud);
END;
GO

-- PROCEDURE para insertar una Actividad recreativa
CREATE PROCEDURE SP_InsertarActividad(
    @p_idTipoActividad int,
    @p_idEmpresaRecreacion int,
    @p_descripcion varchar(150),
    @p_precio decimal(10, 2)
)
AS
BEGIN
    INSERT INTO Actividad (idTipoActividad, idEmpresaRecreacion, descripcion, precio)
    VALUES (@p_idTipoActividad, @p_idEmpresaRecreacion, @p_descripcion, @p_precio);
END;
GO

-- PROCEDURE para insertar un servicio de recreación
CREATE PROCEDURE SP_InsertarServicio(
    @p_idTipoServicio int,
    @p_idEmpresaRecreacion int
)
AS
BEGIN
    INSERT INTO servicio (idTipoServicio, idEmpresaRecreacion)
    VALUES (@p_idTipoServicio, @p_idEmpresaRecreacion);
END;
GO


-- Procedures para eliminar
CREATE PROCEDURE SP_EliminarActividad
  @idActividad int
AS
BEGIN
  BEGIN try
    BEGIN transaction;
    delete FROM Actividad WHERE idActividad = @idActividad;
    commit transaction;
  END try
  BEGIN catch
    rollbaCK transaction;
    throw;
  END catch
END;
GO

CREATE PROCEDURE SP_EliminarHabitacion
  @idHabitacion int
AS
BEGIN
  BEGIN try
    BEGIN transaction;
    DECLARE @idTipoHabitacion int;
    SELECT @idTipoHabitacion = idTipoHabitacion 
    FROM Habitacion 
    WHERE idHabitacion = @idHabitacion;
    delete FROM Factura 
    WHERE idReserva IN (SELECT idReserva FROM Reserva WHERE idHabitacion = @idHabitacion);
    delete FROM Reserva WHERE idHabitacion = @idHabitacion;
    delete FROM Habitacion WHERE idHabitacion = @idHabitacion;
    delete FROM FotosHabitacion WHERE idTipoHabitacion = @idTipoHabitacion;
    delete FROM Comodidades WHERE idTipoHabitacion = @idTipoHabitacion;
    delete FROM TipoHabitacion WHERE idTipo = @idTipoHabitacion;
    commit transaction;
  END try
  BEGIN catch
    rollbaCK transaction;
    throw;
  END catch
END;
GO

-- Procedures para actualizar
CREATE PROCEDURE SP_ActualizarHospedaje
  @id_hospedaje int,
  @nombre varchar(100),
  @cedulaJuridica varchar(15),
  @idTipoHospedaje int,
  @provincia varchar(30),
  @canton varchar(30),
  @distrito varchar(30),
  @barrio varchar(30),
  @senas varchar(100),
  @latitud decimal(10, 8),
  @longitud decimal(11, 8),
  @correo varchar(40)
AS
BEGIN
  update EmpresaHospedaje
  set nombre = @nombre,
      cedulaJuridica = @cedulaJuridica,
      idTipoHospedaje = @idTipoHospedaje,
      provincia = @provincia,
      canton = @canton,
      distrito = @distrito,
      barrio = @barrio,
      senas = @senas,
      latitud = @latitud,
      longitud = @longitud,
      correo = @correo
  WHERE idEmpresaHospedaje = @id_hospedaje;
END;
GO

CREATE PROCEDURE SP_ActualizarCliente
  @idCliente int,
  @nombre varchar(50),
  @apellido1 varchar(50),
  @apellido2 varchar(50),
  @fechaNacimiento date,
  @tipoIdentIFicacion varchar(20),
  @identIFicacion varchar(30),
  @pais varchar(30),
  @provincia varchar(30),
  @canton varchar(30),
  @distrito varchar(30),
  @correo varchar(40)
AS
BEGIN
  update Cliente
  set nombre = @nombre,
      apellido1 = @apellido1,
      apellido2 = @apellido2,
      fechaNacimiento = @fechaNacimiento,
      tipoIdentIFicacion = @tipoIdentIFicacion,
      identIFicacion = @identIFicacion,
      pais = @pais,
      provincia = @provincia,
      canton = @canton,
      distrito = @distrito,
      correo = @correo
  WHERE idCliente = @idCliente;
END;
GO

CREATE PROCEDURE SP_ActualizarReserva
  @idReserva int,
  @idCliente int,
  @idEmpresaHospedaje int,
  @idHabitacion int,
  @fechaIngreso datetime,
  @cantidadPersonAS int,
  @tieneVehiculo bit,
  @fechASalida date,
  @horASalida time
AS
BEGIN
  update Reserva
  set idCliente = @idCliente,
      idEmpresaHospedaje = @idEmpresaHospedaje,
      idHabitacion = @idHabitacion,
      fechaIngreso = @fechaIngreso,
      cantidadPersonAS = @cantidadPersonAS,
      tieneVehiculo = @tieneVehiculo,
      fechASalida = @fechASalida,
      horASalida = @horASalida
  WHERE idReserva = @idReserva;
END;
GO

CREATE PROCEDURE SP_ActualizarFactura
  @idFactura int,
  @idReserva int,
  @importeTotal decimal(10, 2),
  @formaPaGO varchar(20)
AS
BEGIN
  update Factura
  set idReserva = @idReserva,
      importeTotal = @importeTotal,
      formaPaGO = @formaPaGO
  WHERE idFactura = @idFactura;
END;
GO

CREATE PROCEDURE SP_ActualizarActividad
  @idActividad int,
  @idTipoActividad int,
  @idEmpresaRecreacion int,
  @descripcion varchar(150),
  @precio decimal(10, 2)
AS
BEGIN
  update Actividad
  set idTipoActividad = @idTipoActividad,
      idEmpresaRecreacion = @idEmpresaRecreacion,
      descripcion = @descripcion,
      precio = @precio
  WHERE idActividad = @idActividad;
END;
GO

CREATE PROCEDURE SP_ActualizarHabitacion
  @idHabitacion int,
  @idEmpresaHospedaje int,
  @numero int,
  @idTipoHabitacion int
AS
BEGIN
  -- Actualizar la información de la habitación
  update Habitacion
  set idEmpresaHospedaje = @idEmpresaHospedaje,
      numero = @numero,
      idTipoHabitacion = @idTipoHabitacion
  WHERE idHabitacion = @idHabitacion;
END;
GO

--Filtros estilo Airbnb
CREATE PROCEDURE SP_BuscarHospedajesPorProvincia (@provincia varchar(50))
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE provincia = @provincia;
END
GO

CREATE PROCEDURE SP_BuscarHospedajesPorcanton (@canton varchar(50))
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE canton = @canton;
END
GO

CREATE PROCEDURE SP_BuscarHospedajesPortipo (@TipoHospedaje varchar(50))
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE TipoHospedaje = @TipoHospedaje;
END
GO

CREATE PROCEDURE SP_BuscarHospedajesPorServicios (
    @servicio1 varchar(50),
    @servicio2 varchar(50)
)
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE LOWER(servicios) LIKE '%' + LOWER(@servicio1) + '%'
      AND LOWER(servicios) LIKE '%' + LOWER(@servicio2) + '%';
END
GO


CREATE PROCEDURE SP_BuscarHospedajesPorPrecio (@precio_minimo decimal(10, 2), @precio_maximo decimal(10, 2))
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE precio_minimo between @precio_minimo and @precio_maximo;
END
GO

CREATE PROCEDURE SP_BuscarHospedajesPorNombre (
    @nombreHotel varchar(100)
)
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE LOWER(nombreHotel) LIKE '%' + LOWER(@nombreHotel) + '%';
END
GO

CREATE PROCEDURE SP_BuscarHospedajesConFiltros (
    @provincia varchar(50), 
    @canton varchar(50), 
    @TipoHospedaje varchar(50), 
    @servicio varchar(50), 
    @precio_minimo decimal(10, 2), 
    @precio_maximo decimal(10, 2)
)
AS
BEGIN
    SELECT * FROM VW_BusquedaHospedajes
    WHERE provincia = @provincia
      AND canton = @canton
      AND TipoHospedaje = @TipoHospedaje
      AND LOWER(servicios) LIKE '%' + LOWER(@servicio) + '%'
      AND precio_minimo BETWEEN @precio_minimo AND @precio_maximo;
END
GO


CREATE PROCEDURE SP_BuscarFacturASPorFecha
    @fecha_inicio date,
    @fecha_fin date
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE fecha between @fecha_inicio and @fecha_fin;
END
GO

CREATE PROCEDURE SP_BuscarFacturASPorPaGO
    @formaPaGO varchar(50)
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE formaPaGO = @formaPaGO;
END
GO

CREATE PROCEDURE SP_BuscarFacturASPorimporte
    @importe_minimo decimal(10, 2),
    @importe_maximo decimal(10, 2)
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE importeTotal between @importe_minimo and @importe_maximo;
END
GO

CREATE PROCEDURE SP_BuscarFacturASPorTipoHabitacion
    @TipoHabitacion varchar(50)
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE TipoHabitacion = @TipoHabitacion;
END
GO

CREATE PROCEDURE SP_BuscarFacturASPorNumeroHabitacion
    @Habitacion int
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE Habitacion = @Habitacion;
END
GO

CREATE PROCEDURE SP_BuscarFacturASPorCliente
    @Cliente varchar(100)
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE Cliente like @Cliente;
END
GO

CREATE PROCEDURE SP_BuscarFacturASConFiltros
    @fecha_inicio date,
    @fecha_fin date,
    @TipoHabitacion varchar(50),
    @formaPaGO varchar(50),
    @importe_minimo decimal(10, 2)
AS
BEGIN
    SELECT * FROM VW_ReporteFacturacion
    WHERE fecha between @fecha_inicio and @fecha_fin
    and TipoHabitacion = @TipoHabitacion
    and formaPaGO = @formaPaGO
    and importeTotal > @importe_minimo;
END
GO

-- Filtro de Habitaciones por RanGO de FechAS y Comodidades
CREATE PROCEDURE SP_BuscarHabitacionesDisponiblesConFiltros
    @fecha_inicio DATE,
    @fecha_fin DATE,
    @idEmpresaHospedaje INT = NULL,
    @idTipoHabitacion INT = NULL,
    @precio_maximo DECIMAL(10,2) = NULL,
    @comodidad VARCHAR(50) = NULL
AS
BEGIN
    SELECT 
        h.idHabitacion,
        eh.nombre AS nombreHotel,
        th.nombre AS TipoHabitacion,
        th.precio,
        STRING_AGG(c.comodidad, ', ') AS Comodidades
    FROM Habitacion h
    JOIN EmpresaHospedaje eh ON h.idEmpresaHospedaje = eh.idEmpresaHospedaje
    JOIN TipoHabitacion th ON h.idTipoHabitacion = th.idTipo
    LEFT JOIN Comodidades c ON th.idTipo = c.idTipoHabitacion
    WHERE h.idHabitacion NOT IN (
        SELECT idHabitacion FROM Reserva 
        WHERE @fecha_inicio < fechASalida AND @fecha_fin > fechaIngreso
    )
    AND (@idEmpresaHospedaje IS NULL OR h.idEmpresaHospedaje = @idEmpresaHospedaje)
    AND (@idTipoHabitacion IS NULL OR h.idTipoHabitacion = @idTipoHabitacion)
    AND (@precio_maximo IS NULL OR th.precio <= @precio_maximo)
    AND (@comodidad IS NULL OR EXISTS (
        SELECT 1 FROM Comodidades c2 
        WHERE c2.idTipoHabitacion = th.idTipo AND c2.comodidad LIKE '%' + @comodidad + '%'
    ))
    GROUP BY h.idHabitacion, eh.nombre, th.nombre, th.precio;
END;
GO

-- Filtro de Actividades por Tipo, Precio y Ubicación
CREATE PROCEDURE SP_BuscarActividadesConFiltros
    @TipoActividad VARCHAR(50) = NULL,
    @precio_maximo DECIMAL(10,2) = NULL,
    @provincia VARCHAR(30) = NULL,
    @canton VARCHAR(30) = NULL
AS
BEGIN
    SELECT 
        a.idActividad,
        er.nombre AS empresa,
        ta.nombre AS TipoActividad,
        a.descripcion,
        a.precio,
        er.provincia,
        er.canton
    FROM Actividad a
    JOIN EmpresaRecreacion er ON a.idEmpresaRecreacion = er.idEmpresaRecreacion
    JOIN TipoActividad ta ON a.idTipoActividad = ta.idTipoActividad
    WHERE (@TipoActividad IS NULL OR ta.nombre LIKE '%' + @TipoActividad + '%')
    AND (@precio_maximo IS NULL OR a.precio <= @precio_maximo)
    AND (@provincia IS NULL OR er.provincia = @provincia)
    AND (@canton IS NULL OR er.canton = @canton);
END;
GO

--Búsqueda de FacturAS por IdentIFicación de Cliente
CREATE PROCEDURE SP_BuscarFacturASPorIdentIFicacion
    @identIFicacion VARCHAR(30)
AS
BEGIN
    SELECT 
        f.idFactura,
        r.idReserva,
        c.nombre + ' ' + c.apellido1 AS Cliente,
        c.identIFicacion,
        f.importeTotal,
        f.formaPaGO
    FROM Factura f
    JOIN Reserva r ON f.idReserva = r.idReserva
    JOIN Cliente c ON r.idCliente = c.idCliente
    WHERE c.identIFicacion = @identIFicacion;
END;
GO

CREATE PROCEDURE SP_ObtenerUltimaFacturaPorHabitacion
    @idHabitacion INT
AS
BEGIN
    SELECT 
        h.idHabitacion,
        h.numero AS numero_Habitacion,
        th.nombre AS TipoHabitacion,
        th.descripcion,
        th.tipoCama,
        th.precio,
        eh.nombre AS nombreHotel,
        f.idFactura,
        f.fecha AS fecha_Factura,
        f.importeTotal,
        f.formaPaGO,
        r.idReserva,
        c.nombre + ' ' + c.apellido1 + ' ' + c.apellido2 AS Cliente,
        r.fechaIngreso,
        r.fechASalida
    FROM Habitacion h
    JOIN TipoHabitacion th ON h.idTipoHabitacion = th.idTipo
    JOIN EmpresaHospedaje eh ON h.idEmpresaHospedaje = eh.idEmpresaHospedaje
    LEFT JOIN Reserva r ON h.idHabitacion = r.idHabitacion
    LEFT JOIN Factura f ON r.idReserva = f.idReserva
    LEFT JOIN Cliente c ON r.idCliente = c.idCliente
    WHERE h.idHabitacion = @idHabitacion
    AND f.idFactura = (
        SELECT TOP 1 idFactura 
        FROM Factura f2
        JOIN Reserva r2 ON f2.idReserva = r2.idReserva
        WHERE r2.idHabitacion = @idHabitacion
        ORDER BY f2.fecha DESC
    );
END;
GO
CREATE PROCEDURE SP_ReservasFinalizadasPorTipo
    @fecha_inicio DATE,
    @fecha_fin DATE
AS
BEGIN
    SELECT * FROM VW_ReservasFinalizadasPorTipo
    WHERE ultima_fecha BETWEEN @fecha_inicio AND @fecha_fin;
END;
GO

CREATE PROCEDURE SP_ClientesPorRangoEdad
    @edad_min INT,
    @edad_max INT
AS
BEGIN
    SELECT * FROM VW_RangoEdadesClientes
    WHERE Edad BETWEEN @edad_min AND @edad_max;
END;
GO

CREATE PROCEDURE SP_HotelesDemandaZona
    @provincia VARCHAR(30) = NULL,
    @canton VARCHAR(30) = NULL
AS
BEGIN
    SELECT * FROM VW_HotelesDemanda
    WHERE (@provincia IS NULL OR provincia = @provincia)
      AND (@canton IS NULL OR canton = @canton);
END;
GO

CREATE PROCEDURE SP_InfoEmpresaRecreacionPorCedula
    @cedulaJuridica VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM VW_InfoCompletaEmpresaRecreacion
    WHERE cedulaJuridica = @cedulaJuridica;
END;
GO

CREATE PROCEDURE SP_InfoCompletaHospedaje_PorCedula
    @cedulaJuridica VARCHAR(15)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM VW_InfoCompletaHospedaje
    WHERE cedulaJuridica = @cedulaJuridica;
END;
GO

CREATE PROCEDURE SP_BuscarClientePorCedula (
    @cedula VARCHAR(30)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        idCliente,
        nombre,
        apellido1,
        apellido2,
        tipoIdentificacion,
        identificacion,
        correo
    FROM Cliente
    WHERE identificacion = @cedula;
END;
GO

CREATE PROCEDURE SP_InfoCompletaCliente (
    @idCliente INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.idCliente,
        c.nombre,
        c.apellido1,
        c.apellido2,
        c.fechaNacimiento,
        c.tipoIdentificacion,
        c.identificacion,
        c.pais,
        c.provincia,
        c.canton,
        c.distrito,
        c.correo,
        t.numero AS telefono
    FROM Cliente c
    LEFT JOIN TelefonosCliente t ON c.idCliente = t.idCliente
    WHERE c.idCliente = @idCliente;
END;
GO

CREATE PROCEDURE SP_ReservasPorHotel (
    @idEmpresaHospedaje INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.idReserva,
        c.nombre + ' ' + c.apellido1 AS cliente,
        h.numero AS habitacion,
        th.nombre AS tipoHabitacion,
        r.fechaIngreso,
        r.fechaSalida,
        r.cantidadPersonas,
        r.tieneVehiculo,
        r.estado
    FROM Reserva r
    INNER JOIN Cliente c ON r.idCliente = c.idCliente
    INNER JOIN Habitacion h ON r.idHabitacion = h.idHabitacion
    INNER JOIN TipoHabitacion th ON h.idTipoHabitacion = th.idTipo
    WHERE r.idEmpresaHospedaje = @idEmpresaHospedaje
    ORDER BY r.fechaIngreso DESC;
END;
GO

-- INSERTAR IMAGEN PARA EMPRESA HOSPEDAJE
CREATE PROCEDURE SP_InsertarFotoEmpresaHospedaje (
    @idEmpresaHospedaje INT,
    @rutaLocal VARCHAR(300)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO FotosEmpresaHospedaje (idEmpresaHospedaje, rutaLocal)
    VALUES (@idEmpresaHospedaje, @rutaLocal);
END;
GO

-- INSERTAR IMAGEN PARA EMPRESA RECREACION
CREATE PROCEDURE SP_InsertarFotoEmpresaRecreacion (
    @idEmpresaRecreacion INT,
    @rutaLocal VARCHAR(300)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO FotosEmpresaRecreacion (idEmpresaRecreacion, rutaLocal)
    VALUES (@idEmpresaRecreacion, @rutaLocal);
END;
GO

-- INSERTAR IMAGEN PARA TIPOHABITACION
CREATE PROCEDURE SP_InsertarFotoTipoHabitacion (
    @idTipoHabitacion INT,
    @rutaLocal VARCHAR(300)
)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO FotosTipoHabitacion (idTipoHabitacion, rutaLocal)
    VALUES (@idTipoHabitacion, @rutaLocal);
END;
GO

CREATE PROCEDURE SP_InsertarTipoHabitacionParaEmpresa (
    @idEmpresaHospedaje INT,
    @numeroHabitacion INT,
    @nombre VARCHAR(50),
    @descripcion VARCHAR(150),
    @tipoCama VARCHAR(50),
    @precio DECIMAL(10, 2)
)
AS
BEGIN
    SET NOCOUNT ON;

    -- Insertar tipo personalizado
    INSERT INTO TipoHabitacion (nombre, descripcion, tipoCama, precio)
    VALUES (@nombre, @descripcion, @tipoCama, @precio);

    DECLARE @idTipoHabitacion INT = SCOPE_IDENTITY();

    -- Insertar habitación asociada a la empresa y tipo
    INSERT INTO Habitacion (idEmpresaHospedaje, numero, idTipoHabitacion)
    VALUES (@idEmpresaHospedaje, @numeroHabitacion, @idTipoHabitacion);
END;
GO

CREATE PROCEDURE SP_ActualizarTipoHabitacion (
    @idTipoHabitacion INT,
    @nombre VARCHAR(50),
    @descripcion VARCHAR(150),
    @tipoCama VARCHAR(50),
    @precio DECIMAL(10, 2)
)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TipoHabitacion
    SET nombre = @nombre,
        descripcion = @descripcion,
        tipoCama = @tipoCama,
        precio = @precio
    WHERE idTipo = @idTipoHabitacion;
END;
GO

CREATE PROCEDURE SP_EliminarTipoHabitacion (
    @idTipoHabitacion INT
)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM TipoHabitacion WHERE idTipo = @idTipoHabitacion;
END;
GO

CREATE PROCEDURE SP_TiposHabitacionPorEmpresa (
    @idEmpresaHospedaje INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.idTipo,
        h.numero AS numeroHabitacion,
        t.nombre,
        t.descripcion,
        t.tipoCama,
        t.precio
    FROM Habitacion h
    INNER JOIN TipoHabitacion t ON h.idTipoHabitacion = t.idTipo
    WHERE h.idEmpresaHospedaje = @idEmpresaHospedaje
    ORDER BY h.numero;
END;
GO


CREATE PROCEDURE SP_BuscarFacturasPorEmpresa (
    @idEmpresaHospedaje INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        f.idFactura,
        f.fecha,
        f.importeTotal,
        f.formaPago,
        r.idReserva,
        c.nombre + ' ' + c.apellido1 AS cliente
    FROM Factura f
    INNER JOIN Reserva r ON f.idReserva = r.idReserva
    INNER JOIN Cliente c ON r.idCliente = c.idCliente
    WHERE r.idEmpresaHospedaje = @idEmpresaHospedaje
    ORDER BY f.fecha DESC;
END;
GO

-- PROCEDURES DE USUARIO

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