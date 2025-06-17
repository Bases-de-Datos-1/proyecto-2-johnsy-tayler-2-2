using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelesCaribe.Models;

public partial class GestionHoteleraContext : DbContext
{
    public GestionHoteleraContext()
    {
    }

    public GestionHoteleraContext(DbContextOptions<GestionHoteleraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comodidade> Comodidades { get; set; }

    public virtual DbSet<EmpresaHospedaje> EmpresaHospedajes { get; set; }

    public virtual DbSet<EmpresaRecreacion> EmpresaRecreacions { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FotosHabitacion> FotosHabitacions { get; set; }

    public virtual DbSet<Habitacion> Habitacions { get; set; }

    public virtual DbSet<Rede> Redes { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<ServiciosHospedaje> ServiciosHospedajes { get; set; }

    public virtual DbSet<TelefonosCliente> TelefonosClientes { get; set; }

    public virtual DbSet<TelefonosEmpresa> TelefonosEmpresas { get; set; }

    public virtual DbSet<TipoActividad> TipoActividads { get; set; }

    public virtual DbSet<TipoHabitacion> TipoHabitacions { get; set; }

    public virtual DbSet<TipoHospedaje> TipoHospedajes { get; set; }

    public virtual DbSet<TipoRedSocial> TipoRedSocials { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<TipoServicioHospedaje> TipoServicioHospedajes { get; set; }

    public virtual DbSet<VwActividadesRecreativa> VwActividadesRecreativas { get; set; }

    public virtual DbSet<VwBusquedaHospedaje> VwBusquedaHospedajes { get; set; }

    public virtual DbSet<VwDetalleReserva> VwDetalleReservas { get; set; }

    public virtual DbSet<VwFacturacion> VwFacturacions { get; set; }

    public virtual DbSet<VwHabitacionesConComodidade> VwHabitacionesConComodidades { get; set; }

    public virtual DbSet<VwHabitacionesDisponible> VwHabitacionesDisponibles { get; set; }

    public virtual DbSet<VwHotelesDemandum> VwHotelesDemanda { get; set; }

    public virtual DbSet<VwInfoCompletaHospedaje> VwInfoCompletaHospedajes { get; set; }

    public virtual DbSet<VwRangoEdadesCliente> VwRangoEdadesClientes { get; set; }

    public virtual DbSet<VwReporteFacturacion> VwReporteFacturacions { get; set; }

    public virtual DbSet<VwReservasFinalizadasPorTipo> VwReservasFinalizadasPorTipos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JOHNSY\\SQLEXPRESS;Database=GestionHotelera;Integrated Security=True;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.IdActividad).HasName("PK__Activida__327F9BED8ECAC66F");

            entity.ToTable("Actividad");

            entity.HasIndex(e => e.IdTipoActividad, "ix_actividad_tipo");

            entity.Property(e => e.IdActividad).HasColumnName("idActividad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdEmpresaRecreacion).HasColumnName("idEmpresaRecreacion");
            entity.Property(e => e.IdTipoActividad).HasColumnName("idTipoActividad");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdEmpresaRecreacionNavigation).WithMany(p => p.Actividads)
                .HasForeignKey(d => d.IdEmpresaRecreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Actividad__idEmp__75A278F5");

            entity.HasOne(d => d.IdTipoActividadNavigation).WithMany(p => p.Actividads)
                .HasForeignKey(d => d.IdTipoActividad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Actividad__idTip__74AE54BC");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__885457EEF857D3DF");

            entity.ToTable("Cliente", tb => tb.HasTrigger("tr_evitardeletecliente"));

            entity.HasIndex(e => e.Correo, "UQ__Cliente__2A586E0BD7104ED8").IsUnique();

            entity.HasIndex(e => e.Identificacion, "UQ__Cliente__C196DEC792B58D7F").IsUnique();

            entity.HasIndex(e => e.Correo, "ix_cliente_correo");

            entity.HasIndex(e => e.Identificacion, "ix_cliente_identificacion");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido2");
            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Distrito)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("distrito");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("pais");
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoIdentificacion");
        });

        modelBuilder.Entity<Comodidade>(entity =>
        {
            entity.HasKey(e => e.IdComodidad).HasName("PK__Comodida__BAD83D03BD932391");

            entity.Property(e => e.IdComodidad).HasColumnName("idComodidad");
            entity.Property(e => e.Comodidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("comodidad");
            entity.Property(e => e.IdTipoHabitacion).HasColumnName("idTipoHabitacion");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Comodidades)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comodidad__idTip__4F7CD00D");
        });

        modelBuilder.Entity<EmpresaHospedaje>(entity =>
        {
            entity.HasKey(e => e.IdEmpresaHospedaje).HasName("PK__EmpresaH__82A9100784B6D209");

            entity.ToTable("EmpresaHospedaje");

            entity.HasIndex(e => e.Correo, "UQ__EmpresaH__2A586E0B350D415A").IsUnique();

            entity.HasIndex(e => e.CedulaJuridica, "UQ__EmpresaH__DD9A4FB4239E8B23").IsUnique();

            entity.HasIndex(e => new { e.Provincia, e.Nombre }, "ix_empresahospedaje_provincia_nombre");

            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.Barrio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("barrio");
            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.CedulaJuridica)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cedulaJuridica");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Distrito)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("distrito");
            entity.Property(e => e.IdTipoHospedaje).HasColumnName("idTipoHospedaje");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.Senas)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senas");

            entity.HasOne(d => d.IdTipoHospedajeNavigation).WithMany(p => p.EmpresaHospedajes)
                .HasForeignKey(d => d.IdTipoHospedaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmpresaHo__idTip__3B75D760");
        });

        modelBuilder.Entity<EmpresaRecreacion>(entity =>
        {
            entity.HasKey(e => e.IdEmpresaRecreacion).HasName("PK__EmpresaR__691BB3291ADE331D");

            entity.ToTable("EmpresaRecreacion");

            entity.HasIndex(e => e.Correo, "UQ__EmpresaR__2A586E0BE95CCB9F").IsUnique();

            entity.HasIndex(e => e.CedulaJuridica, "UQ__EmpresaR__DD9A4FB477BAC0CD").IsUnique();

            entity.Property(e => e.IdEmpresaRecreacion).HasColumnName("idEmpresaRecreacion");
            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.CedulaJuridica)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cedulaJuridica");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Distrito)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("distrito");
            entity.Property(e => e.Encargado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("encargado");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.Senas)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senas");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__3CD5687EF73E4FFF");

            entity.ToTable("Factura", tb => tb.HasTrigger("tr_bloqueardeletefactura"));

            entity.HasIndex(e => e.Fecha, "ix_factura_fecha");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("formaPago");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.ImporteTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importeTotal");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdReserva)
                .HasConstraintName("FK__Factura__idReser__6B24EA82");
        });

        modelBuilder.Entity<FotosHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdFoto).HasName("PK__FotosHab__69D65094CCF2F171");

            entity.ToTable("FotosHabitacion");

            entity.Property(e => e.IdFoto).HasColumnName("idFoto");
            entity.Property(e => e.IdTipoHabitacion).HasColumnName("idTipoHabitacion");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.FotosHabitacions)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FotosHabi__idTip__52593CB8");
        });

        modelBuilder.Entity<Habitacion>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__D9D53BE251A64799");

            entity.ToTable("Habitacion");

            entity.HasIndex(e => new { e.IdEmpresaHospedaje, e.Numero }, "ix_habitacion_empresa_numero");

            entity.HasIndex(e => new { e.IdEmpresaHospedaje, e.Numero }, "uq_habitacion_numero_empresa").IsUnique();

            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.IdTipoHabitacion).HasColumnName("idTipoHabitacion");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.IdEmpresaHospedajeNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdEmpresaHospedaje)
                .HasConstraintName("FK__Habitacio__idEmp__5812160E");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitacions)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__idTip__571DF1D5");
        });

        modelBuilder.Entity<Rede>(entity =>
        {
            entity.HasKey(e => e.IdRed).HasName("PK__Redes__3C87902A628E8D57");

            entity.HasIndex(e => e.IdEmpresaHospedaje, "ix_redes_idempresa");

            entity.Property(e => e.IdRed).HasColumnName("idRed");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.IdTipoRed).HasColumnName("idTipoRed");
            entity.Property(e => e.Url)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("url");

            entity.HasOne(d => d.IdEmpresaHospedajeNavigation).WithMany(p => p.Redes)
                .HasForeignKey(d => d.IdEmpresaHospedaje)
                .HasConstraintName("FK__Redes__idEmpresa__4316F928");

            entity.HasOne(d => d.IdTipoRedNavigation).WithMany(p => p.Redes)
                .HasForeignKey(d => d.IdTipoRed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Redes__idTipoRed__440B1D61");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reserva__94D104C8F6F1D92D");

            entity.ToTable("Reserva", tb =>
                {
                    tb.HasTrigger("tr_bloqueardeletereserva");
                    tb.HasTrigger("tr_reserva_cierregenerafactura");
                });

            entity.HasIndex(e => e.Estado, "ix_reserva_estado");

            entity.HasIndex(e => new { e.FechaIngreso, e.FechaSalida }, "ix_reserva_fechas");

            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.CantidadPersonas).HasColumnName("cantidadPersonas");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("ACTIVA")
                .HasColumnName("estado");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaSalida).HasColumnName("fechaSalida");
            entity.Property(e => e.HoraSalida)
                .HasDefaultValue(new TimeOnly(12, 0, 0))
                .HasColumnName("horaSalida");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.TieneVehiculo).HasColumnName("tieneVehiculo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idClien__656C112C");

            entity.HasOne(d => d.IdEmpresaHospedajeNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEmpresaHospedaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reserva__idEmpre__6754599E");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHabitacion)
                .HasConstraintName("FK__Reserva__idHabit__66603565");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__CEB98119463B6BB4");

            entity.ToTable("Servicio");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.IdEmpresaRecreacion).HasColumnName("idEmpresaRecreacion");
            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");

            entity.HasOne(d => d.IdEmpresaRecreacionNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdEmpresaRecreacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Servicio__idEmpr__7B5B524B");

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdTipoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Servicio__idTipo__7A672E12");
        });

        modelBuilder.Entity<ServiciosHospedaje>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__CEB981191FB11671");

            entity.ToTable("ServiciosHospedaje");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");

            entity.HasOne(d => d.IdEmpresaHospedajeNavigation).WithMany(p => p.ServiciosHospedajes)
                .HasForeignKey(d => d.IdEmpresaHospedaje)
                .HasConstraintName("FK__Servicios__idEmp__48CFD27E");

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.ServiciosHospedajes)
                .HasForeignKey(d => d.IdTipoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Servicios__idTip__49C3F6B7");
        });

        modelBuilder.Entity<TelefonosCliente>(entity =>
        {
            entity.HasKey(e => e.IdTelefono).HasName("PK__Telefono__39C142DFB0C56FDB");

            entity.ToTable("TelefonosCliente");

            entity.Property(e => e.IdTelefono).HasColumnName("idTelefono");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.TelefonosClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__Telefonos__idCli__5EBF139D");
        });

        modelBuilder.Entity<TelefonosEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdTelefono).HasName("PK__Telefono__39C142DF14E51533");

            entity.ToTable("TelefonosEmpresa");

            entity.Property(e => e.IdTelefono).HasColumnName("idTelefono");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.Numero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero");

            entity.HasOne(d => d.IdEmpresaHospedajeNavigation).WithMany(p => p.TelefonosEmpresas)
                .HasForeignKey(d => d.IdEmpresaHospedaje)
                .HasConstraintName("FK__Telefonos__idEmp__3E52440B");
        });

        modelBuilder.Entity<TipoActividad>(entity =>
        {
            entity.HasKey(e => e.IdTipoActividad).HasName("PK__TipoActi__A3477EC54604FB7F");

            entity.ToTable("TipoActividad");

            entity.Property(e => e.IdTipoActividad).HasColumnName("idTipoActividad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdTipo).HasName("PK__TipoHabi__BDD0DFE144872A5C");

            entity.ToTable("TipoHabitacion");

            entity.HasIndex(e => e.Nombre, "ix_tipohabitacion_nombre");

            entity.Property(e => e.IdTipo).HasColumnName("idTipo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TipoCama)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoCama");
        });

        modelBuilder.Entity<TipoHospedaje>(entity =>
        {
            entity.HasKey(e => e.IdTipoHospedaje).HasName("PK__TipoHosp__0CC167A742D60A5F");

            entity.ToTable("TipoHospedaje");

            entity.Property(e => e.IdTipoHospedaje).HasColumnName("idTipoHospedaje");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoRedSocial>(entity =>
        {
            entity.HasKey(e => e.IdTipoRed).HasName("PK__TipoRedS__F814FCE9340B59D6");

            entity.ToTable("TipoRedSocial");

            entity.Property(e => e.IdTipoRed).HasColumnName("idTipoRed");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PK__TipoServ__27861676C0AF416F");

            entity.ToTable("TipoServicio");

            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoServicioHospedaje>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PK__TipoServ__27861676575A3DE6");

            entity.ToTable("TipoServicioHospedaje");

            entity.Property(e => e.IdTipoServicio).HasColumnName("idTipoServicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<VwActividadesRecreativa>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ActividadesRecreativas");

            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Empresa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empresa");
            entity.Property(e => e.Encargado)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("encargado");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.ServiciosIncluidos)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("servicios_incluidos");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoActividad)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwBusquedaHospedaje>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_BusquedaHospedajes");

            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Distrito)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("distrito");
            entity.Property(e => e.IdEmpresaHospedaje).HasColumnName("idEmpresaHospedaje");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");
            entity.Property(e => e.NombreHotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreHotel");
            entity.Property(e => e.PrecioMinimo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_minimo");
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.Senas)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senas");
            entity.Property(e => e.Servicios)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("servicios");
            entity.Property(e => e.TipoHospedaje)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwDetalleReserva>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_DetalleReservas");

            entity.Property(e => e.CantidadPersonAs).HasColumnName("cantidadPersonAS");
            entity.Property(e => e.Cliente)
                .HasMaxLength(152)
                .IsUnicode(false);
            entity.Property(e => e.FechAsalida).HasColumnName("fechASalida");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("datetime")
                .HasColumnName("fechaIngreso");
            entity.Property(e => e.HorAsalida).HasColumnName("horASalida");
            entity.Property(e => e.Hotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotel");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.Noches).HasColumnName("noches");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalEstimado)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("total_estimado");
            entity.Property(e => e.Vehiculo)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("vehiculo");
        });

        modelBuilder.Entity<VwFacturacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Facturacion");

            entity.Property(e => e.Cliente)
                .HasMaxLength(152)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FormaPaGo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("formaPaGO");
            entity.Property(e => e.Hotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotel");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.ImporteTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importeTotal");
            entity.Property(e => e.Noches).HasColumnName("noches");
        });

        modelBuilder.Entity<VwHabitacionesConComodidade>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_HabitacionesConComodidades");

            entity.Property(e => e.Comodidades)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Hotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotel");
            entity.Property(e => e.IdHabitacion).HasColumnName("idHabitacion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwHabitacionesDisponible>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_HabitacionesDisponibles");

            entity.Property(e => e.Comodidades)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreHotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreHotel");
            entity.Property(e => e.NumeroHabitacion).HasColumnName("numero_Habitacion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.TipoCama)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoCama");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwHotelesDemandum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_HotelesDemanda");

            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.Hotel)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.TotalReservas).HasColumnName("total_reservas");
        });

        modelBuilder.Entity<VwInfoCompletaHospedaje>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_InfoCompletaHospedaje");

            entity.Property(e => e.Barrio)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("barrio");
            entity.Property(e => e.Canton)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("canton");
            entity.Property(e => e.CedulaJuridica)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cedulaJuridica");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Distrito)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("distrito");
            entity.Property(e => e.Latitud)
                .HasColumnType("decimal(10, 8)")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("decimal(11, 8)")
                .HasColumnName("longitud");
            entity.Property(e => e.NombreHotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreHotel");
            entity.Property(e => e.Provincia)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("provincia");
            entity.Property(e => e.RedesSociales)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("redesSociales");
            entity.Property(e => e.Senas)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("senas");
            entity.Property(e => e.Servicios)
                .HasMaxLength(8000)
                .IsUnicode(false)
                .HasColumnName("servicios");
            entity.Property(e => e.TipoHospedaje)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwRangoEdadesCliente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_RangoEdadesClientes");

            entity.Property(e => e.Cliente)
                .HasMaxLength(101)
                .IsUnicode(false);
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
        });

        modelBuilder.Entity<VwReporteFacturacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ReporteFacturacion");

            entity.Property(e => e.Cliente)
                .HasMaxLength(152)
                .IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.FormaPaGo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("formaPaGO");
            entity.Property(e => e.Hotel)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotel");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdReserva).HasColumnName("idReserva");
            entity.Property(e => e.ImporteTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importeTotal");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwReservasFinalizadasPorTipo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ReservasFinalizadasPorTipo");

            entity.Property(e => e.PrimeraFecha)
                .HasColumnType("datetime")
                .HasColumnName("primera_fecha");
            entity.Property(e => e.TipoHabitacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalReservas).HasColumnName("total_reservas");
            entity.Property(e => e.UltimaFecha).HasColumnName("ultima_fecha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
