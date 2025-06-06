﻿// <auto-generated />
using GlobalSolutionRopz.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace GlobalSolutionRopz.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250529233444_GlobalSolutionRopz")]
    partial class GlobalSolutionRopz
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GlobalSolutionRopz.Model.Alerta", b =>
                {
                    b.Property<int>("id_alerta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_alerta"));

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("estado ");

                    b.Property<int>("temperatura")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("temperatura ");

                    b.Property<string>("tipo_mensagem")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("tipo_mensagem  ");

                    b.HasKey("id_alerta");

                    b.ToTable("Api_Global_Dotnet_Alerta");
                });

            modelBuilder.Entity("GlobalSolutionRopz.Model.Mensagem", b =>
                {
                    b.Property<int>("tipo_mensagem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tipo_mensagem"));

                    b.HasKey("tipo_mensagem");

                    b.ToTable("Api_Global_Dotnet_Mensagem");
                });

            modelBuilder.Entity("GlobalSolutionRopz.Model.Usuario", b =>
                {
                    b.Property<int>("id_usuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_usuario"));

                    b.Property<string>("cep")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("cep");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("endereco")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("endereco");

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("estado");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("nome");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("telefone")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("id_usuario");

                    b.ToTable("Api_Global_Dotnet_Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
