using LevantamientoDeRed.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LevantamientoDeRed.Database
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string, IdentityUserClaim<string>, UsuarioRol, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public virtual DbSet<Poste>? Postes { get; set; }
        public virtual DbSet<Mufa>? Mufas { get; set; }
        public virtual DbSet<Gpon>? Gpons { get; set; }
        public virtual DbSet<Accion>? Acciones { get; set; }
        public virtual DbSet<Cliente>? Clientes { get; set; }
        public virtual DbSet<Cable>? Cables { get; set; }
        public virtual DbSet<Punto>? Puntos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opciones) : base(opciones) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Accion>(b => b.ToTable("Acciones"));

            builder.Entity<Usuario>(b =>
            {
                b.ToTable("Usuarios");

                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UsuarioRoles)
                    .WithOne(e => e.Usuario)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.Acciones)
                    .WithOne(e => e.Usuario)
                    .HasForeignKey(a => a.UsuarioId)
                    .IsRequired();
            });

            builder.Entity<Rol>(b =>
            {
                b.ToTable("Roles");

                b.HasMany(e => e.UsuarioRoles)
                    .WithOne(e => e.Rol)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<Punto>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Cable", "Cable")
                        .WithMany("Puntos")
                        .HasForeignKey("CableId")
                        .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Gpon>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Poste", "Poste")
                .WithMany("Gpons")
                .HasForeignKey("PosteId")
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Mufa>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Poste", "Poste")
                .WithMany("Mufas")
                .HasForeignKey("PosteId")
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Cliente>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Mufa", "Mufa")
                .WithMany("Clientes")
                .HasForeignKey("MufaId")
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Contrato>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Servicio", "Servicio")
                .WithMany("Contratos")
                .HasForeignKey("ServicioId")
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Mufa>(e =>
            {
                e.HasOne("LevantamientoDeRed.Entities.Gpon", "Gpon")
                .WithMany("Mufas")
                .HasForeignKey("GponId")
                .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
