use GestionHotelera;
go

-- Generar factura automáticamente cuando una reserva cambia a 'CANCELADA'
create trigger tr_reserva_canceladagenerafactura
on Reserva
after update
as
begin
    set nocount on;

    if exists (
        select 1
        from inserted i
        join deleted d on i.idReserva = d.idReserva
        where i.estado = 'CANCELADA' and d.estado <> 'CANCELADA'
    )
    begin
        insert into Factura (idReserva, importeTotal, formaPago)
        select 
            i.idReserva,
            datediff(day, r.fechaIngreso, r.fechaSalida) * th.precio as importeTotal,
            'Pagado'
        from inserted i
        join Reserva r on i.idReserva = r.idReserva
        join Habitacion h on r.idHabitacion = h.idHabitacion
        join TipoHabitacion th on h.idTipoHabitacion = th.idTipo
        where i.estado = 'CANCELADA';
    end
end;
go

-- Evitar eliminar clientes del sistema
create trigger tr_evitardeletecliente
on Cliente
instead of delete
as
begin
    raiserror ('No se permite eliminar clientes del sistema.', 16, 1);
end;
go

-- Solo permitir eliminar reservas si están en estado 'CANCELADA'
create trigger tr_bloqueardeletereserva
on Reserva
after delete
as
begin
    set nocount on;

    if exists (
        select 1
        from deleted
        where estado <> 'CANCELADA'
    )
    begin
        rollback transaction;
        raiserror ('Solo se pueden eliminar reservas en estado CANCELADA.', 16, 1);
    end
end;
go

-- Bloquear eliminación de facturas 
create trigger tr_bloqueardeletefactura
on Factura
after delete
as
begin
    set nocount on;

    rollback transaction;
    raiserror ('No se permite eliminar facturas del sistema.', 16, 1);
end;
go

-- Evitar poder actualizar una reserva que esté cancelada
create trigger tr_noactualizarreservacancelada
on Reserva
instead of update
as
begin
    set nocount on;

    if exists (
        select 1
        from inserted i
        join Reserva r on i.idReserva = r.idReserva
        where r.estado = 'CANCELADA'
    )
    begin
        raiserror('No se puede modificar una reserva en estado CANCELADA.', 16, 1);
        rollback transaction;
        return;
    end

    -- Si no está cancelada, permitir la actualización
    update Reserva
    set 
        idCliente = i.idCliente,
        idEmpresaHospedaje = i.idEmpresaHospedaje,
        idHabitacion = i.idHabitacion,
        fechaIngreso = i.fechaIngreso,
        cantidadPersonas = i.cantidadPersonas,
        tieneVehiculo = i.tieneVehiculo,
        fechaSalida = i.fechaSalida,
        horaSalida = i.horaSalida,
        estado = i.estado
    from inserted i
    where Reserva.idReserva = i.idReserva;
end;