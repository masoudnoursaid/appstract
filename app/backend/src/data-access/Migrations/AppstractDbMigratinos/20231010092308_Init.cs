using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.AppstractDbMigratinos
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProvidedId = table.Column<string>(type: "longtext", nullable: false),
                    PayHubPaymentId = table.Column<string>(type: "longtext", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "longtext", nullable: false),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
