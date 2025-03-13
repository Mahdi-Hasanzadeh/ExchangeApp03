using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations.UserManagementDb
{
    /// <inheritdoc />
    public partial class tablesupdated11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Databasename",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServerAddress",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Databasename",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ServerAddress",
                table: "Users");
        }
    }
}
