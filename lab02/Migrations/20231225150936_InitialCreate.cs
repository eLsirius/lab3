using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab02.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Operation = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    Day = table.Column<int>(type: "INTEGER", nullable: false),
                    Result = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOperations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntervalOperations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Operation = table.Column<string>(type: "TEXT", nullable: true),
                    Year1 = table.Column<int>(type: "INTEGER", nullable: false),
                    Year2 = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntervalOperations", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "LeapOperatons",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Operation = table.Column<string>(type: "TEXT", nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeapOperatons", x => x.OperationId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOperations");

            migrationBuilder.DropTable(
                name: "IntervalOperations");

            migrationBuilder.DropTable(
                name: "LeapOperatons");
        }
    }
}
