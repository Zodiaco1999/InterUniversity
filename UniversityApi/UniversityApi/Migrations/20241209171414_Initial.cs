using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credito",
                columns: table => new
                {
                    CreditoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Creditos = table.Column<byte>(type: "tinyint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credito", x => x.CreditoId);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Creditos = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.MateriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroIdentificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    FechaInscrito = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Creditos = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante_1", x => x.EstudianteId);
                    table.ForeignKey(
                        name: "FK_Estudiante_Usuario",
                        column: x => x.EstudianteId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    FechaContratacion = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesor_1", x => x.ProfesorId);
                    table.ForeignKey(
                        name: "FK_Profesor_Usuario",
                        column: x => x.ProfesorId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "MateriaProfesor",
                columns: table => new
                {
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    ProfesorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaProfesor_1", x => new { x.MateriaId, x.ProfesorId });
                    table.ForeignKey(
                        name: "FK_MateriaProfesor_Materia",
                        column: x => x.MateriaId,
                        principalTable: "Materia",
                        principalColumn: "MateriaId");
                    table.ForeignKey(
                        name: "FK_MateriaProfesor_Profesor",
                        column: x => x.ProfesorId,
                        principalTable: "Profesor",
                        principalColumn: "ProfesorId");
                });

            migrationBuilder.CreateTable(
                name: "Clase",
                columns: table => new
                {
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clase", x => new { x.ProfesorId, x.EstudianteId });
                    table.ForeignKey(
                        name: "FK_Clase_Estudiante",
                        column: x => x.EstudianteId,
                        principalTable: "Estudiante",
                        principalColumn: "EstudianteId");
                    table.ForeignKey(
                        name: "FK_Clase_MateriaProfesor",
                        columns: x => new { x.MateriaId, x.ProfesorId },
                        principalTable: "MateriaProfesor",
                        principalColumns: new[] { "MateriaId", "ProfesorId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clase_EstudianteId",
                table: "Clase",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Clase_MateriaId_ProfesorId",
                table: "Clase",
                columns: new[] { "MateriaId", "ProfesorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MateriaProfesor_ProfesorId",
                table: "MateriaProfesor",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "UQ__Usuario__FCA68D9105274D61",
                table: "Usuario",
                column: "NumeroIdentificacion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clase");

            migrationBuilder.DropTable(
                name: "Credito");

            migrationBuilder.DropTable(
                name: "Estudiante");

            migrationBuilder.DropTable(
                name: "MateriaProfesor");

            migrationBuilder.DropTable(
                name: "Materia");

            migrationBuilder.DropTable(
                name: "Profesor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
