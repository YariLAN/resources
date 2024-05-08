using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Provider.Migrations
{
    public partial class Add_Blips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypesBlips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesBlips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<int>(nullable: false),
                    Position = table.Column<double[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blips_TypesBlips_TypeId",
                        column: x => x.TypeId,
                        principalTable: "TypesBlips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blips_TypeId",
                table: "Blips",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blips");

            migrationBuilder.DropTable(
                name: "TypesBlips");
        }
    }
}
