using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class TabelaFilme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generos_Filmes_FilmeEntidadeId",
                table: "Generos");

            migrationBuilder.RenameColumn(
                name: "FilmeEntidadeId",
                table: "Generos",
                newName: "FilmeId");

            migrationBuilder.RenameIndex(
                name: "IX_Generos_FilmeEntidadeId",
                table: "Generos",
                newName: "IX_Generos_FilmeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Generos_Filmes_FilmeId",
                table: "Generos",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generos_Filmes_FilmeId",
                table: "Generos");

            migrationBuilder.RenameColumn(
                name: "FilmeId",
                table: "Generos",
                newName: "FilmeEntidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Generos_FilmeId",
                table: "Generos",
                newName: "IX_Generos_FilmeEntidadeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Generos_Filmes_FilmeEntidadeId",
                table: "Generos",
                column: "FilmeEntidadeId",
                principalTable: "Filmes",
                principalColumn: "Id");
        }
    }
}
