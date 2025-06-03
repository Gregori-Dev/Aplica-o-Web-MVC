using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarFilmeEGenro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filmes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    DataLancamento = table.Column<string>(type: "text", nullable: false),
                    Sinopse = table.Column<string>(type: "text", nullable: false),
                    CaminhoPoster = table.Column<string>(type: "text", nullable: false),
                    MediaVotos = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    FilmeEntidadeId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Generos_Filmes_FilmeEntidadeId",
                        column: x => x.FilmeEntidadeId,
                        principalTable: "Filmes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Generos_FilmeEntidadeId",
                table: "Generos",
                column: "FilmeEntidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Filmes");
        }
    }
}
