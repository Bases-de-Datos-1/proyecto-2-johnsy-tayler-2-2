-- Buscar habitaciones por empresa y número
create nonclustered index ix_habitacion_empresa_numero
on Habitacion(idEmpresaHospedaje, numero);

-- Buscar el estado de una reserva
create nonclustered index ix_reserva_estado
on Reserva(estado);

-- Buscar una fecha de reserva entre dos fechas
create nonclustered index ix_reserva_fechas
on Reserva(fechaIngreso, fechaSalida);

-- Buscar facturas por fecha
create nonclustered index ix_factura_fecha
on Factura(fecha);

-- Buscar el nombre de un tipo de habitación
create nonclustered index ix_tipohabitacion_nombre
on TipoHabitacion(nombre);

-- Buscar la identificacion del cliente
create nonclustered index ix_cliente_identificacion
on Cliente(identificacion);

-- Buscar correo de un cliente
create nonclustered index ix_cliente_correo
on Cliente(correo);

-- Buscar por provincia y nombre de hospedaje
create nonclustered index ix_empresahospedaje_provincia_nombre
on EmpresaHospedaje(provincia, nombre);

-- Buscar redes sociales
create nonclustered index ix_redes_idempresa
on Redes(idEmpresaHospedaje);

-- Buscar actividades por tipo
create nonclustered index ix_actividad_tipo
on Actividad(idTipoActividad);

-- Buscar reservas por cliente
create nonclustered index ix_reserva_idcliente
on Reserva(idCliente);

-- Buscar reservas por empresa hospedaje
create nonclustered index ix_reserva_idempresahospedaje
on Reserva(idEmpresaHospedaje);

-- Buscar reservas por habitación
create nonclustered index ix_reserva_idhabitacion
on Reserva(idHabitacion);

-- Buscar facturas por reserva
create nonclustered index ix_factura_idreserva
on Factura(idReserva);

-- Buscar habitaciones por tipo
create nonclustered index ix_habitacion_idtipohabitacion
on Habitacion(idTipoHabitacion);

-- Buscar servicios hospedaje por empresa
create nonclustered index ix_servicioshospedaje_empresa
on ServiciosHospedaje(idEmpresaHospedaje);

-- Buscar actividades por empresa
create nonclustered index ix_actividad_empresa
on Actividad(idEmpresaRecreacion);

-- Buscar usuarios por nombre
create nonclustered index ix_usuarios_nombre
on Usuarios(nombreUsuario);

-- Buscar tipos de habitacion por empresa
create nonclustered index ix_tipohabitacion_empresa
on tipohabitacion(idempresahospedaje);

-- Buscar habitaciones por empresa y tipo
create nonclustered index ix_habitacion_empresa_tipo
on habitacion(idempresahospedaje, idtipohabitacion);

-- Buscar reservas por cliente y empresa
create nonclustered index ix_reserva_idcliente_empresa
on reserva(idcliente, idempresahospedaje);

-- Buscar cliente por nombre y apellidos
create nonclustered index ix_cliente_nombre_apellidos
on cliente(nombre, apellido1, apellido2);

-- Buscar reservas por estado y fechas
create nonclustered index ix_reserva_estado_fecha
on reserva(estado, fechaingreso, fechasalida);