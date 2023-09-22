using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.Property(p => p.Username).HasColumnName("Username").HasMaxLength(100).IsRequired();
        builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(255).IsRequired();
        builder.Property(p => p.Password).HasColumnName("Password").HasMaxLength(250).IsRequired();

        builder
        .HasMany(p => p.Roles)
        .WithMany(p => p.Usuarios)
        .UsingEntity<UsuarioRol>(
            j=>j
            .HasOne(p => p.Rol)
            .WithMany(p => p.UsuarioRoles)
            .HasForeignKey(p => p.IdRolfk),

            j => j
            .HasOne(p => p.Usuario)
            .WithMany(p => p.UsuarioRoles)
            .HasForeignKey(p => p.IdUsuariofk),

            j => {
                j.ToTable("usuarioRol");
                j.HasKey(p => new { p.IdUsuariofk, p.IdRolfk});
            });
        builder.HasMany(p => p.RefreshTokens)
        .WithOne(p => p.Usuario)
        .HasForeignKey(p => p.IdUsuariofk);
    }
}
