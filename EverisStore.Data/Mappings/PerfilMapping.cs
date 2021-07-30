using EverisStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EverisStore.Data.Mappings
{
    public class PerfilMapping : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Regra)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Descricao)
               .IsRequired();

            builder.ToTable("Perfis");
        }
    }
}