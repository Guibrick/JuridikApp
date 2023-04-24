using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuridikApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Queries",
                columns: table => new
                {
                    QueryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CaseDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CasePlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseClaimant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseDefendant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicableLaw = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicableJurisprudence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Judgement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queries", x => x.QueryId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queries");
        }
    }
}
