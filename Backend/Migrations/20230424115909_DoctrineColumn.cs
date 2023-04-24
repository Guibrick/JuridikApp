using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuridikApp.Migrations
{
    /// <inheritdoc />
    public partial class DoctrineColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicableDoctrine",
                table: "Queries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicableDoctrine",
                table: "Queries");
        }
    }
}
