USE GestionHotelera;
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
GRANT EXECUTE ON dbo.SP_InsertarTipoHabitacionParaEmpresa TO rol_admin;
GRANT EXECUTE ON dbo.SP_InsertarFactura TO rol_admin;

GRANT EXECUTE ON dbo.SP_ActualizarHospedaje TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarReserva TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarFactura TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarCliente TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarActividad TO rol_admin;
GRANT EXECUTE ON dbo.SP_ActualizarTipoHabitacion TO rol_admin;

GRANT EXECUTE ON dbo.SP_EliminarHabitacion TO rol_admin;
GRANT EXECUTE ON dbo.SP_EliminarActividad TO rol_admin;
GRANT EXECUTE ON dbo.SP_EliminarTipoHabitacion TO rol_admin;

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
GRANT EXECUTE ON dbo.SP_BuscarFacturasPorEmpresa TO rol_admin;
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
GRANT EXECUTE ON dbo.SP_TiposHabitacionPorEmpresa TO rol_admin;

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