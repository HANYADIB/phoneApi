using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phoneApi.Migrations
{
    /// <inheritdoc />
    public partial class OOO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Categories",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Categories");
        }
    }
}
