using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modelo.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rangos",
                columns: table => new
                {
                    RangoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Escala = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rangos", x => x.RangoId);
                });

            migrationBuilder.CreateTable(
                name: "TorneosCasuales",
                columns: table => new
                {
                    TorneoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Premio = table.Column<int>(type: "int", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TorneosCasuales", x => x.TorneoId);
                });

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    PartidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreLocal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreVisitante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartidaJugada = table.Column<bool>(type: "bit", nullable: false),
                    ResultadoPartidaId = table.Column<int>(type: "int", nullable: false),
                    TorneoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.PartidaId);
                    table.ForeignKey(
                        name: "FK_Partidas_TorneosCasuales_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "TorneosCasuales",
                        principalColumn: "TorneoId");
                });

            migrationBuilder.CreateTable(
                name: "ResultadoPartidas",
                columns: table => new
                {
                    ResultadoPartidaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GanadorLocal = table.Column<bool>(type: "bit", nullable: false),
                    GanadorVisitante = table.Column<bool>(type: "bit", nullable: false),
                    RondasLocal = table.Column<int>(type: "int", nullable: false),
                    RondasVisitante = table.Column<int>(type: "int", nullable: false),
                    MuertesLocal = table.Column<int>(type: "int", nullable: false),
                    MuertesVisitante = table.Column<int>(type: "int", nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    Mapa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartidaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoPartidas", x => x.ResultadoPartidaId);
                    table.ForeignKey(
                        name: "FK_ResultadoPartidas_Partidas_PartidaId",
                        column: x => x.PartidaId,
                        principalTable: "Partidas",
                        principalColumn: "PartidaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CapitanId = table.Column<int>(type: "int", nullable: false),
                    TorneoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.EquipoId);
                    table.ForeignKey(
                        name: "FK_Equipos_TorneosCasuales_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "TorneosCasuales",
                        principalColumn: "TorneoId");
                });

            migrationBuilder.CreateTable(
                name: "Jugadores",
                columns: table => new
                {
                    JugadorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangoId = table.Column<int>(type: "int", nullable: false),
                    EnEquipo = table.Column<bool>(type: "bit", nullable: false),
                    EquipoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugadores", x => x.JugadorId);
                    table.ForeignKey(
                        name: "FK_Jugadores_Equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "Equipos",
                        principalColumn: "EquipoId");
                    table.ForeignKey(
                        name: "FK_Jugadores_Rangos_RangoId",
                        column: x => x.RangoId,
                        principalTable: "Rangos",
                        principalColumn: "RangoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_CapitanId",
                table: "Equipos",
                column: "CapitanId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_TorneoId",
                table: "Equipos",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_EquipoId",
                table: "Jugadores",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Jugadores_RangoId",
                table: "Jugadores",
                column: "RangoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_TorneoId",
                table: "Partidas",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoPartidas_PartidaId",
                table: "ResultadoPartidas",
                column: "PartidaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Jugadores_CapitanId",
                table: "Equipos",
                column: "CapitanId",
                principalTable: "Jugadores",
                principalColumn: "JugadorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Jugadores_CapitanId",
                table: "Equipos");

            migrationBuilder.DropTable(
                name: "ResultadoPartidas");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Jugadores");

            migrationBuilder.DropTable(
                name: "Equipos");

            migrationBuilder.DropTable(
                name: "Rangos");

            migrationBuilder.DropTable(
                name: "TorneosCasuales");
        }
    }
}
