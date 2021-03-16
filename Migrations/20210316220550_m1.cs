using Microsoft.EntityFrameworkCore.Migrations;

namespace WebProg_Projekat_2021.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ParkingId);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    VoziloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    Tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.VoziloId);
                });

            migrationBuilder.CreateTable(
                name: "Mesto",
                columns: table => new
                {
                    MestoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broj = table.Column<int>(type: "int", nullable: false),
                    VoziloId = table.Column<int>(type: "int", nullable: true),
                    ParkingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesto", x => x.MestoId);
                    table.ForeignKey(
                        name: "FK_Mesto_Parking_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parking",
                        principalColumn: "ParkingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mesto_Vozilo_VoziloId",
                        column: x => x.VoziloId,
                        principalTable: "Vozilo",
                        principalColumn: "VoziloId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesto_ParkingId",
                table: "Mesto",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesto_VoziloId",
                table: "Mesto",
                column: "VoziloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesto");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "Vozilo");
        }
    }
}
