using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalScheduleAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    IdCard = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BDiaTodo = table.Column<bool>(type: "bit", nullable: false),
                    SLocal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDescricao = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.IdCard);
                });

            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    IdToDo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    STitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDescricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BConcluido = table.Column<bool>(type: "bit", nullable: false),
                    IdCard = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.IdToDo);
                    table.ForeignKey(
                        name: "FK_ToDos_Cards_IdCard",
                        column: x => x.IdCard,
                        principalTable: "Cards",
                        principalColumn: "IdCard");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_IdCard",
                table: "ToDos",
                column: "IdCard");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
