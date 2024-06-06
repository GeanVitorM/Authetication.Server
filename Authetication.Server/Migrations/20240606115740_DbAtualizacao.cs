using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Authetication.Server.Migrations
{
    /// <inheritdoc />
    public partial class DbAtualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "integer", nullable: false),
                    NomeAdmin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailAdmin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.IdAdmin);
                    table.ForeignKey(
                        name: "FK_Admins_Usuarios_IdAdmin",
                        column: x => x.IdAdmin,
                        principalTable: "Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordenadores",
                columns: table => new
                {
                    IdCoordenador = table.Column<int>(type: "integer", nullable: false),
                    NomeCoordenador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailCoordenador = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordenadores", x => x.IdCoordenador);
                    table.ForeignKey(
                        name: "FK_Coordenadores_Usuarios_IdCoordenador",
                        column: x => x.IdCoordenador,
                        principalTable: "Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fisioterapeutas",
                columns: table => new
                {
                    IdFisio = table.Column<int>(type: "integer", nullable: false),
                    NomeFisio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailFisio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Matricula = table.Column<int>(type: "integer", nullable: false),
                    SemestreFisio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fisioterapeutas", x => x.IdFisio);
                    table.ForeignKey(
                        name: "FK_Fisioterapeutas_Usuarios_IdFisio",
                        column: x => x.IdFisio,
                        principalTable: "Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    IdPaciente = table.Column<int>(type: "integer", nullable: false),
                    NomePaciente = table.Column<string>(type: "text", nullable: true),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    UF = table.Column<string>(type: "text", nullable: true),
                    Endereco = table.Column<string>(type: "text", nullable: true),
                    NumeroCasa = table.Column<string>(type: "text", nullable: true),
                    DataDeNascimento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sexo = table.Column<char>(type: "character(1)", nullable: true),
                    Proficao = table.Column<string>(type: "text", nullable: true),
                    DiagnosticoClinico = table.Column<string>(type: "text", nullable: true),
                    DiagnosticoFisio = table.Column<string>(type: "text", nullable: true),
                    EmailPaciente = table.Column<string>(type: "text", nullable: true),
                    TipoUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Pacientes_Usuarios_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Coordenadores");

            migrationBuilder.DropTable(
                name: "Fisioterapeutas");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
