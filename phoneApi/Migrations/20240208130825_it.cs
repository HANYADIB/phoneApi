using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phoneApi.Migrations
{
    /// <inheritdoc />
    public partial class it : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Suppliers",
                newName: "Namesup");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Namecat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namesup",
                table: "Suppliers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Namecat",
                table: "Categories",
                newName: "Name");
        }
    }
}
