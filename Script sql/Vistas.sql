USE GestionHotelera;
GO

CREATE VIEW VW_InfoCompletaHospedaje AS
SELECT 
	eh.idEmpresaHospedaje,
    eh.nombre AS nombreHotel,
    eh.cedulaJuridica,
    th.nombre AS TipoHospedaje,
    eh.provincia,
    eh.canton,
    eh.distrito,
    eh.barrio,
    eh.senas,
    eh.latitud,
    eh.longitud,
    eh.correo,
    STRING_AGG(tsh.nombre, ', ') AS servicios,
    STRING_AGG(trs.nombre + ': ' + r.url, ' | ') AS redesSociales
FROM EmpresaHospedaje eh
JOIN TipoHospedaje th on eh.idTipoHospedaje = th.idTipoHospedaje
LEFT JOIN ServiciosHospedaje sh on eh.idEmpresaHospedaje = sh.idEmpresaHospedaje
LEFT JOIN TipoServicioHospedaje tsh on sh.idTipoServicio = tsh.idTipoServicio
LEFT JOIN Redes r on eh.idEmpresaHospedaje = r.idEmpresaHospedaje
LEFT JOIN TipoRedSocial trs on r.idTipoRed = trs.idTipoRed
GROUP BY eh.idEmpresaHospedaje, eh.nombre, eh.cedulaJuridica, th.nombre, eh.provincia, 
         eh.canton, eh.distrito, eh.barrio, eh.senas, eh.latitud, eh.longitud, eh.correo;
GO

-- Vista para información de Habitaciones disponibles
CREATE VIEW VW_HabitacionesDisponibles AS
SELECT 
    eh.nombre AS nombreHotel,
    th.nombre AS TipoHabitacion,
    th.descripcion,
    th.tipoCama,
    th.precio,
    h.numero AS numero_Habitacion,
    STRING_AGG(c.comodidad, ', ') AS Comodidades
FROM Habitacion h
JOIN EmpresaHospedaje eh on h.idEmpresaHospedaje = eh.idEmpresaHospedaje
JOIN TipoHabitacion th on h.idTipoHabitacion = th.idTipo
LEFT JOIN Comodidades c on th.idTipo = c.idTipoHabitacion
WHERE h.idHabitacion NOT IN (
    SELECT idHabitacion FROM Reserva 
    WHERE GETDATE() BETWEEN fechaIngreso AND fechASalida
)
GROUP BY eh.nombre, th.nombre, th.descripcion, th.tipoCama, th.precio, h.numero;
GO

-- Vista para información de Reservas
CREATE VIEW VW_DetalleReservas AS
SELECT 
    r.idReserva,
    c.nombre + ' ' + c.apellido1 + ' ' + c.apellido2 AS Cliente,
    eh.nombre AS hotel,
    h.numero AS Habitacion,
    th.nombre AS TipoHabitacion,
    r.fechaIngreso,
    r.fechASalida,
    r.horASalida,
    r.cantidadPersonAS,
    CASE WHEN r.tieneVehiculo = 1 THEN 'Sí' ELSE 'No' END AS vehiculo,
    DATEDIFF(day, r.fechaIngreso, r.fechASalida) AS noches,
    (DATEDIFF(day, r.fechaIngreso, r.fechASalida) * th.precio) AS total_estimado
FROM Reserva r
JOIN Cliente c on r.idCliente = c.idCliente
JOIN Habitacion h on r.idHabitacion = h.idHabitacion
JOIN EmpresaHospedaje eh on h.idEmpresaHospedaje = eh.idEmpresaHospedaje
JOIN TipoHabitacion th on h.idTipoHabitacion = th.idTipo;
GO

-- Vista para Facturación
CREATE VIEW VW_Facturacion AS
SELECT 
    f.idFactura,
    r.idReserva,
    c.nombre + ' ' + c.apellido1 + ' ' + c.apellido2 AS Cliente,
    eh.nombre AS hotel,
    h.numero AS Habitacion,
    f.fecha,
    f.importeTotal,
    f.formaPaGO,
    DATEDIFF(day, r.fechaIngreso, r.fechASalida) AS noches
FROM Factura f
JOIN Reserva r on f.idReserva = r.idReserva
JOIN Cliente c on r.idCliente = c.idCliente
JOIN Habitacion h on r.idHabitacion = h.idHabitacion
JOIN EmpresaHospedaje eh on h.idEmpresaHospedaje = eh.idEmpresaHospedaje;
GO

-- Vista para Actividades recreativas
CREATE VIEW VW_ActividadesRecreativas AS
SELECT 
    er.nombre AS empresa,
    er.encargado,
    er.telefono,
    er.correo,
    ta.nombre AS TipoActividad,
    a.descripcion,
    a.precio,
    STRING_AGG(ts.nombre, ', ') AS servicios_incluidos
FROM Actividad a
JOIN EmpresaRecreacion er on a.idEmpresaRecreacion = er.idEmpresaRecreacion
JOIN TipoActividad ta on a.idTipoActividad = ta.idTipoActividad
LEFT JOIN servicio s on er.idEmpresaRecreacion = s.idEmpresaRecreacion
LEFT JOIN TipoServicio ts on s.idTipoServicio = ts.idTipoServicio
GROUP BY er.nombre, er.encargado, er.telefono, er.correo, ta.nombre, a.descripcion, a.precio;
GO

-- Vista de hospedajes con filtros estilo AirBnB
CREATE VIEW VW_BusquedaHospedajes AS
SELECT 
    eh.idEmpresaHospedaje,
    eh.nombre AS nombreHotel,
    eh.provincia,
    eh.canton,
    eh.distrito,
    eh.senas, -- se agregó aquí
    th.nombre AS TipoHospedaje,
    eh.correo,
    eh.latitud,
    eh.longitud,
    STRING_AGG(tsh.nombre, ', ') AS servicios,
    MIN(thab.precio) AS precio_minimo
FROM EmpresaHospedaje eh
JOIN TipoHospedaje th ON eh.idTipoHospedaje = th.idTipoHospedaje
JOIN Habitacion h ON eh.idEmpresaHospedaje = h.idEmpresaHospedaje
JOIN TipoHabitacion thab ON h.idTipoHabitacion = thab.idTipo
LEFT JOIN ServiciosHospedaje sh ON eh.idEmpresaHospedaje = sh.idEmpresaHospedaje
LEFT JOIN TipoServicioHospedaje tsh ON sh.idTipoServicio = tsh.idTipoServicio
GROUP BY 
    eh.idEmpresaHospedaje, 
    eh.nombre, 
    eh.provincia, 
    eh.canton, 
    eh.distrito,
    eh.senas, -- se agregó aquí
    th.nombre, 
    eh.correo, 
    eh.latitud, 
    eh.longitud;
GO

-- Vista de Facturación con datos combinados
CREATE VIEW VW_ReporteFacturacion AS
SELECT 
    f.idFactura,
    r.idReserva,
    c.nombre + ' ' + c.apellido1 + ' ' + c.apellido2 AS Cliente,
    eh.nombre AS hotel,
    h.numero AS Habitacion,
    th.nombre AS TipoHabitacion,
    f.fecha,
    f.importeTotal,
    f.formaPaGO
FROM Factura f
JOIN Reserva r on f.idReserva = r.idReserva
JOIN Cliente c on r.idCliente = c.idCliente
JOIN Habitacion h on r.idHabitacion = h.idHabitacion
JOIN TipoHabitacion th on h.idTipoHabitacion = th.idTipo
JOIN EmpresaHospedaje eh on h.idEmpresaHospedaje = eh.idEmpresaHospedaje;
GO

--Vista para Habitaciones con Comodidades Específicas
CREATE VIEW VW_HabitacionesConComodidades AS
SELECT 
    h.idHabitacion,
    eh.nombre AS hotel,
    th.nombre AS TipoHabitacion,
    th.precio,
    STRING_AGG(c.comodidad, ', ') AS Comodidades
FROM Habitacion h
JOIN EmpresaHospedaje eh ON h.idEmpresaHospedaje = eh.idEmpresaHospedaje
JOIN TipoHabitacion th ON h.idTipoHabitacion = th.idTipo
JOIN Comodidades c ON th.idTipo = c.idTipoHabitacion
GROUP BY h.idHabitacion, eh.nombre, th.nombre, th.precio;
GO

CREATE VIEW VW_ReservasFinalizadasPorTipo AS
SELECT 
    th.nombre AS TipoHabitacion,
    COUNT(*) AS total_reservas,
    MIN(r.fechaIngreso) AS primera_fecha,
    MAX(r.fechASalida) AS ultima_fecha
FROM Reserva r
JOIN Habitacion h ON r.idHabitacion = h.idHabitacion
JOIN TipoHabitacion th ON h.idTipoHabitacion = th.idTipo
WHERE r.fechASalida < GETDATE()
GROUP BY th.nombre;
GO

CREATE VIEW VW_RangoEdadesClientes AS
SELECT 
    c.idCliente,
    c.nombre + ' ' + c.apellido1 AS Cliente,
    DATEDIFF(YEAR, c.fechaNacimiento, GETDATE()) AS Edad
FROM Reserva r
JOIN Cliente c ON r.idCliente = c.idCliente;
GO

CREATE VIEW VW_HotelesDemanda AS
SELECT 
    eh.nombre AS Hotel,
    eh.provincia,
    eh.canton,
    COUNT(*) AS total_reservas
FROM Reserva r
JOIN EmpresaHospedaje eh ON r.idEmpresaHospedaje = eh.idEmpresaHospedaje
GROUP BY eh.nombre, eh.provincia, eh.canton;
GO

CREATE VIEW VW_InfoCompletaEmpresaRecreacion AS
SELECT 
    er.idEmpresaRecreacion,
    er.nombre,
    er.cedulaJuridica,
    er.correo,
    er.telefono,
    er.encargado,
    er.provincia,
    er.canton,
    er.distrito,
    er.senas,
    er.latitud,
    er.longitud,
    STRING_AGG(ts.nombre, ', ') AS servicios,
    STRING_AGG(ta.nombre + ': ' + a.descripcion + ' ($' + CAST(a.precio AS VARCHAR) + ')', ' | ') AS actividades
FROM EmpresaRecreacion er
LEFT JOIN servicio s ON er.idEmpresaRecreacion = s.idEmpresaRecreacion
LEFT JOIN TipoServicio ts ON s.idTipoServicio = ts.idTipoServicio
LEFT JOIN Actividad a ON er.idEmpresaRecreacion = a.idEmpresaRecreacion
LEFT JOIN TipoActividad ta ON a.idTipoActividad = ta.idTipoActividad
GROUP BY er.idEmpresaRecreacion, er.nombre, er.cedulaJuridica, er.correo, er.telefono, er.encargado,
         er.provincia, er.canton, er.distrito, er.senas, er.latitud, er.longitud;

go

-- VISTA PARA MOSTRAR HOTELES QUE ADMINISTRA CADA USUARIO
CREATE VIEW VW_HotelesUsuario AS
SELECT 
    u.idUsuario,
    u.nombreUsuario,
    u.rol,
    e.idEmpresaHospedaje,
    e.nombre AS nombreHotel,
    e.cedulaJuridica,
    e.provincia,
    e.canton,
    th.nombre AS tipoHospedaje,
    ue.fechaAsignacion,
    ue.activo AS esAdminActivo
FROM Usuarios u
INNER JOIN UsuarioEmpresa ue ON u.idUsuario = ue.idUsuario
INNER JOIN EmpresaHospedaje e ON ue.idEmpresaHospedaje = e.idEmpresaHospedaje
INNER JOIN TipoHospedaje th ON e.idTipoHospedaje = th.idTipoHospedaje
WHERE ue.activo = 1;
GO

-- VISTA PARA LOGIN DE USUARIOS
CREATE VIEW VW_LoginUsuarios AS
SELECT idUsuario, nombreUsuario, rol
FROM Usuarios;
GO
-- VISTA RESUMEN DE ADMINISTRADORES POR HOTEL
CREATE VIEW VW_AdminsPorHotel AS
SELECT 
    e.idEmpresaHospedaje,
    e.nombre AS nombreHotel,
    e.provincia,
    e.canton,
    COUNT(ue.idUsuario) AS totalAdmins,
    STRING_AGG(u.nombreUsuario, ', ') AS administradores
FROM EmpresaHospedaje e
LEFT JOIN UsuarioEmpresa ue ON e.idEmpresaHospedaje = ue.idEmpresaHospedaje AND ue.activo = 1
LEFT JOIN Usuarios u ON ue.idUsuario = u.idUsuario
GROUP BY e.idEmpresaHospedaje, e.nombre, e.provincia, e.canton;
GO

-- FOTOS DE EMPRESA HOSPEDAJE
CREATE VIEW VW_FotosEmpresaHospedaje AS
SELECT idFoto, idEmpresaHospedaje, rutaLocal
FROM FotosEmpresaHospedaje;
GO

-- FOTOS DE EMPRESA RECREACION
CREATE VIEW VW_FotosEmpresaRecreacion AS
SELECT idFoto, idEmpresaRecreacion, rutaLocal
FROM FotosEmpresaRecreacion;
GO

-- FOTOS DE TIPO HABITACION
CREATE VIEW VW_FotosTipoHabitacion AS
SELECT idFoto, idTipoHabitacion, rutaLocal
FROM FotosTipoHabitacion;