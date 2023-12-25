using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab02.Migrations
{
    /// <inheritdoc />
    public partial class GodPleaseWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "LeapOperatons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OperationId",
                table: "IntervalOperations",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "LeapOperatons",
                newName: "OperationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "IntervalOperations",
                newName: "OperationId");
        }
    }
}
