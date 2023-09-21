using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api2.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ms_user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mt_productivity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mt_productivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mt_productivity_ms_user_IdUser",
                        column: x => x.IdUser,
                        principalTable: "ms_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mt_productivity_IdUser",
                table: "mt_productivity",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mt_productivity");

            migrationBuilder.DropTable(
                name: "ms_user");
        }
    }
}
