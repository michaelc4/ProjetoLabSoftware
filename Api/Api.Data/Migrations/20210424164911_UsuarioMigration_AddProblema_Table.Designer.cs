﻿// <auto-generated />
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210424164911_UsuarioMigration_AddProblema_Table")]
    partial class UsuarioMigration_AddProblema_Table
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("Api.Domain.Entities.ProblemaEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DesProblema")
                        .HasColumnType("LONGTEXT");

                    b.Property<string>("IndAprovado")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("IndAtivo")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("IndTipoBeneficio")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("IndTipoSolucao")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<int>("NumProbabilidadeInvestir")
                        .HasColumnType("int");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Problema");
                });

            modelBuilder.Entity("Api.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DesEmail")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DesEspecialidade")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("DesExperiencia")
                        .HasMaxLength(3000)
                        .HasColumnType("longtext");

                    b.Property<string>("DesImagem")
                        .HasColumnType("LONGTEXT");

                    b.Property<string>("DesNome")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("DesSenha")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("DesTelefone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("Local")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DesEmail")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Api.Domain.Entities.ProblemaEntity", b =>
                {
                    b.HasOne("Api.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
