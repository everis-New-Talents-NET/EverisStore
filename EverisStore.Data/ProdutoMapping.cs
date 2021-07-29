using EverisStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EverisStore.Data
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasMaxLength(250);

            builder.OwnsOne(c => c.Dimensoes, cm =>
            {
                cm.Property(c => c.Altura)
                    .HasColumnName("Altura");

                cm.Property(c => c.Largura)
                    .HasColumnName("Largura");

                cm.Property(c => c.Profundidade)
                    .HasColumnName("Profundidade");
            });

            builder.ToTable("Produtos");
        }
    }
}