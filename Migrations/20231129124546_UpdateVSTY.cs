using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVSTY : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CNVSTPTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNVSTPTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNVSTPTTHeo");

            migrationBuilder.CreateTable(
                name: "CNVSTYTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTY = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTYTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTYTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTYTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTY = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTYTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTYTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTYTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoVSTY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayVSTY = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTYTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTYTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTYTTGiaCam_IdTrangTraiGiaCam",
                table: "CNVSTYTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTYTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNVSTYTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTYTTHeo_IdTrangTraiHeo",
                table: "CNVSTYTTHeo",
                column: "IdTrangTraiHeo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CNVSTYTTGiaCam");

            migrationBuilder.DropTable(
                name: "CNVSTYTTGiaSuc");

            migrationBuilder.DropTable(
                name: "CNVSTYTTHeo");

            migrationBuilder.CreateTable(
                name: "CNVSTPTTGiaCam",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTrangTraiGiaCam = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTGiaCam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTGiaCam_TrangTraiGiaCam_IdTrangTraiGiaCam",
                        column: x => x.IdTrangTraiGiaCam,
                        principalTable: "TrangTraiGiaCam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTPTTGiaSuc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTrangTraiDaiGiaSuc = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTGiaSuc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTGiaSuc_TrangTraiDaiGiaSuc_IdTrangTraiDaiGiaSuc",
                        column: x => x.IdTrangTraiDaiGiaSuc,
                        principalTable: "TrangTraiDaiGiaSuc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CNVSTPTTHeo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdTrangTraiHeo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayVSTP = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoVSTP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CNVSTPTTHeo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CNVSTPTTHeo_TrangTraiHeo_IdTrangTraiHeo",
                        column: x => x.IdTrangTraiHeo,
                        principalTable: "TrangTraiHeo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTGiaCam_IdTrangTraiGiaCam",
                table: "CNVSTPTTGiaCam",
                column: "IdTrangTraiGiaCam");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTGiaSuc_IdTrangTraiDaiGiaSuc",
                table: "CNVSTPTTGiaSuc",
                column: "IdTrangTraiDaiGiaSuc");

            migrationBuilder.CreateIndex(
                name: "IX_CNVSTPTTHeo_IdTrangTraiHeo",
                table: "CNVSTPTTHeo",
                column: "IdTrangTraiHeo");
        }
    }
}
