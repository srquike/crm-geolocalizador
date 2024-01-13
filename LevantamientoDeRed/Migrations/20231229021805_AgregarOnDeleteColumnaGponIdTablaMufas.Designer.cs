﻿// <auto-generated />
using System;
using LevantamientoDeRed.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LevantamientoDeRed.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231229021805_AgregarOnDeleteColumnaGponIdTablaMufas")]
    partial class AgregarOnDeleteColumnaGponIdTablaMufas
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LevantamientoDeRed.Entities.Accion", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("AccionRegistrada")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Fecha")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Tabla")
                        .HasColumnType("text");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Acciones", (string)null);
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Cable", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Estado")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Tipo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cables");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Cliente", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("Activo")
                        .HasColumnType("boolean");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContratoId")
                        .HasColumnType("text");

                    b.Property<Point>("Coordenadas")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MufaId")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.HasIndex("MufaId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Contrato", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset>("FechaFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PrecioMensual")
                        .HasColumnType("double precision");

                    b.Property<string>("ServicioId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ServicioId");

                    b.ToTable("Contrato");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Gpon", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PosteId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PosteId");

                    b.ToTable("Gpons");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Mufa", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("GponId")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PosteId")
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GponId");

                    b.HasIndex("PosteId");

                    b.ToTable("Mufas");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Poste", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<Point>("Coordenadas")
                        .HasColumnType("geometry");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Postes");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Punto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CableId")
                        .HasColumnType("text");

                    b.Property<Point>("Coordenadas")
                        .HasColumnType("geometry");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CableId");

                    b.ToTable("Puntos");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Rol", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Servicio", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Apellido")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.UsuarioRol", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Accion", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Usuario", "Usuario")
                        .WithMany("Acciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Cliente", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Contrato", "Contrato")
                        .WithMany("Cliente")
                        .HasForeignKey("ContratoId");

                    b.HasOne("LevantamientoDeRed.Entities.Mufa", "Mufa")
                        .WithMany("Clientes")
                        .HasForeignKey("MufaId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Contrato");

                    b.Navigation("Mufa");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Contrato", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Servicio", "Servicio")
                        .WithMany("Contratos")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Gpon", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Poste", "Poste")
                        .WithMany("Gpons")
                        .HasForeignKey("PosteId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Poste");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Mufa", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Gpon", "Gpon")
                        .WithMany("Mufas")
                        .HasForeignKey("GponId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("LevantamientoDeRed.Entities.Poste", "Poste")
                        .WithMany("Mufas")
                        .HasForeignKey("PosteId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Gpon");

                    b.Navigation("Poste");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Punto", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Cable", "Cable")
                        .WithMany("Puntos")
                        .HasForeignKey("CableId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Cable");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.UsuarioRol", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Rol", "Rol")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LevantamientoDeRed.Entities.Usuario", "Usuario")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Usuario", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Usuario", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LevantamientoDeRed.Entities.Usuario", null)
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Cable", b =>
                {
                    b.Navigation("Puntos");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Contrato", b =>
                {
                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Gpon", b =>
                {
                    b.Navigation("Mufas");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Mufa", b =>
                {
                    b.Navigation("Clientes");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Poste", b =>
                {
                    b.Navigation("Gpons");

                    b.Navigation("Mufas");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Rol", b =>
                {
                    b.Navigation("UsuarioRoles");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Servicio", b =>
                {
                    b.Navigation("Contratos");
                });

            modelBuilder.Entity("LevantamientoDeRed.Entities.Usuario", b =>
                {
                    b.Navigation("Acciones");

                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("Tokens");

                    b.Navigation("UsuarioRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
