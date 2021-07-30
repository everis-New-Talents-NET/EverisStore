using EverisStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EverisStore.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(c => c.Email)
                   .IsRequired();

            builder.Property(c => c.Senha)
                   .IsRequired();

            builder.Property(c => c.PerfilId)
                   .IsRequired();

            builder.HasOne(lnq => lnq.Perfil)
                   .WithMany(lnq => lnq.Usuarios)
                   .HasForeignKey(lnq => lnq.PerfilId);

            builder.ToTable("Usuarios");
        }
    }
}