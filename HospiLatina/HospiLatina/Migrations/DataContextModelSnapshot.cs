﻿// <auto-generated />
using System;
using HospiLatina.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HospiLatina.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HospiLatina.Data.Calificacion", b =>
                {
                    b.Property<int>("IdCalificacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCalificacion"));

                    b.Property<string>("Comentario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("Puntuacion")
                        .HasColumnType("int");

                    b.HasKey("IdCalificacion");

                    b.HasIndex("IdCita");

                    b.ToTable("Calificaciones");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Cita", b =>
                {
                    b.Property<int>("IdCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCita"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdConsultorio")
                        .HasColumnType("int");

                    b.Property<int>("IdEstudiante")
                        .HasColumnType("int");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<int>("IdProcedimiento")
                        .HasColumnType("int");

                    b.Property<int>("IdProfesor")
                        .HasColumnType("int");

                    b.Property<int>("IdSala")
                        .HasColumnType("int");

                    b.HasKey("IdCita");

                    b.HasIndex("IdConsultorio");

                    b.HasIndex("IdEstudiante");

                    b.HasIndex("IdPaciente");

                    b.HasIndex("IdProcedimiento");

                    b.HasIndex("IdProfesor");

                    b.HasIndex("IdSala");

                    b.ToTable("Citas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Consultorio", b =>
                {
                    b.Property<int>("IdConsultorio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdConsultorio"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdConsultorio");

                    b.ToTable("Consultorios");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.DetalleFactura", b =>
                {
                    b.Property<int>("IdDetalle")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalle"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("IdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdProcedimiento")
                        .HasColumnType("int");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.HasKey("IdDetalle");

                    b.HasIndex("IdFactura");

                    b.HasIndex("IdProcedimiento");

                    b.ToTable("DetallesFactura");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Equipo", b =>
                {
                    b.Property<int>("IdEquipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEquipo"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaSoporte")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEquipo");

                    b.ToTable("Equipos");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Estudiante", b =>
                {
                    b.Property<int>("IdEstudiante")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEstudiante"));

                    b.Property<int>("AnoDeGraduacion")
                        .HasColumnType("int");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HorarioAtencion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEstudiante");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Factura", b =>
                {
                    b.Property<int>("IdFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFactura"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPaciente")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("IdFactura");

                    b.HasIndex("IdPaciente");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Paciente", b =>
                {
                    b.Property<int>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPaciente"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPaciente");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Procedimiento", b =>
                {
                    b.Property<int>("IdProcedimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProcedimiento"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("IdProcedimiento");

                    b.ToTable("Procedimientos");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Profesor", b =>
                {
                    b.Property<int>("IdProfesor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfesor"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProfesor");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.SalaCirugia", b =>
                {
                    b.Property<int>("IdSala")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSala"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSala");

                    b.ToTable("SalasCirugia");
                });

            modelBuilder.Entity("HospiLatina.Data.Calificacion", b =>
                {
                    b.HasOne("HospiLatina.Data.Entities.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Cita", b =>
                {
                    b.HasOne("HospiLatina.Data.Entities.Consultorio", "Consultorio")
                        .WithMany("Citas")
                        .HasForeignKey("IdConsultorio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.Estudiante", "Estudiante")
                        .WithMany("Citas")
                        .HasForeignKey("IdEstudiante")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.Paciente", "Paciente")
                        .WithMany("Citas")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.Procedimiento", "Procedimiento")
                        .WithMany("Citas")
                        .HasForeignKey("IdProcedimiento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.Profesor", "Profesor")
                        .WithMany("Citas")
                        .HasForeignKey("IdProfesor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.SalaCirugia", "Sala")
                        .WithMany("Citas")
                        .HasForeignKey("IdSala")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Consultorio");

                    b.Navigation("Estudiante");

                    b.Navigation("Paciente");

                    b.Navigation("Procedimiento");

                    b.Navigation("Profesor");

                    b.Navigation("Sala");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.DetalleFactura", b =>
                {
                    b.HasOne("HospiLatina.Data.Entities.Factura", "Factura")
                        .WithMany("DetallesFactura")
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HospiLatina.Data.Entities.Procedimiento", "Procedimiento")
                        .WithMany("DetallesFactura")
                        .HasForeignKey("IdProcedimiento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factura");

                    b.Navigation("Procedimiento");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Factura", b =>
                {
                    b.HasOne("HospiLatina.Data.Entities.Paciente", "Paciente")
                        .WithMany("Facturas")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Consultorio", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Estudiante", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Factura", b =>
                {
                    b.Navigation("DetallesFactura");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Paciente", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("Facturas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Procedimiento", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("DetallesFactura");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.Profesor", b =>
                {
                    b.Navigation("Citas");
                });

            modelBuilder.Entity("HospiLatina.Data.Entities.SalaCirugia", b =>
                {
                    b.Navigation("Citas");
                });
#pragma warning restore 612, 618
        }
    }
}
