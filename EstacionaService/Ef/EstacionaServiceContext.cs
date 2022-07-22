using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EstacionaService.Ef
{
    public partial class EstacionaServiceContext : DbContext
    {
        public EstacionaServiceContext()
        {
        }

        public EstacionaServiceContext(DbContextOptions<EstacionaServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientesAtivosPatio> ClientesAtivosPatios { get; set; }
        public virtual DbSet<MensalistaPf> MensalistaPfs { get; set; }
        public virtual DbSet<Pagamento> Pagamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexao = File.ReadAllText("C:\\Users\\Lacir Junior\\Documents\\Conexoes\\ConnectionFile.txt");

            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(conexao);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientesAtivosPatio>(entity =>
            {
                entity.ToTable("ClientesAtivosPatio");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Entrada).HasColumnType("datetime");

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.Saida).HasColumnType("datetime");

                entity.Property(e => e.TempoGasto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVeiculo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValorApagar)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ValorAPagar");
            });

            modelBuilder.Entity<MensalistaPf>(entity =>
            {
                entity.HasKey(e => e.Cpf)
                    .HasName("PK__Mensalis__C1F89730F7912D52");

                entity.ToTable("MensalistaPF");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.DataEntrada).HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVeiculo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Valor).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Pagamento>(entity =>
            {
                entity.ToTable("Pagamento");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.DataPag)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.TempoGasto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoVeiculo)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Pagamento)
                    .HasForeignKey<Pagamento>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pagamento__ID__2180FB33");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
