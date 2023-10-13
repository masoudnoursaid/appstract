using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.PaymentDbMigrations
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
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ApiSecret = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ApiKey = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    AuthorizedIpAddresses = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    FullName = table.Column<string>(type: "longtext", nullable: false),
                    Symbol = table.Column<string>(type: "longtext", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MerchantOwners",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MerchantOwners", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProcessStatus = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentStatus", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "longtext", nullable: false),
                    Successful = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Items = table.Column<string>(type: "longtext", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    DisplayTitle = table.Column<string>(type: "longtext", nullable: false),
                    Provider = table.Column<string>(type: "longtext", nullable: false),
                    GeographicRestrictionEnforced = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sandbox = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    MinValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Icon = table.Column<string>(type: "longtext", nullable: true),
                    GetWay = table.Column<int>(type: "int", nullable: false),
                    GeographicSanctions = table.Column<string>(type: "longtext", nullable: false),
                    SupportedCountries = table.Column<string>(type: "longtext", nullable: false),
                    MerchantOwnerId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethods_MerchantOwners_MerchantOwnerId",
                        column: x => x.MerchantOwnerId,
                        principalTable: "MerchantOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Mobile = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PaymentId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProvidedId = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    CountryCode = table.Column<string>(type: "longtext", nullable: true),
                    City = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    Firstname = table.Column<string>(type: "longtext", nullable: true),
                    Lastname = table.Column<string>(type: "longtext", nullable: true),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true),
                    PaymentId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProvidedId = table.Column<string>(type: "longtext", nullable: true),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    ClientRedirectUrl = table.Column<string>(type: "longtext", nullable: false),
                    ClientWebHookUrl = table.Column<string>(type: "longtext", nullable: false),
                    PaymentMethodId = table.Column<string>(type: "varchar(255)", nullable: true),
                    PayerId = table.Column<string>(type: "varchar(255)", nullable: true),
                    StatusId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CurrencyId = table.Column<string>(type: "varchar(255)", nullable: false),
                    TransactionId = table.Column<string>(type: "varchar(255)", nullable: true),
                    CustomerId = table.Column<string>(type: "varchar(255)", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ApplicationId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Payers_PayerId",
                        column: x => x.PayerId,
                        principalTable: "Payers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_PaymentStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PaymentStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_PaymentId",
                table: "Customer",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payers_PaymentId",
                table: "Payers",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_MerchantOwnerId",
                table: "PaymentMethods",
                column: "MerchantOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_Title",
                table: "PaymentMethods",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ApplicationId",
                table: "Payments",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CurrencyId",
                table: "Payments",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PayerId",
                table: "Payments",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StatusId",
                table: "Payments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TransactionId",
                table: "Payments",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Payments_PaymentId",
                table: "Customer",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payers_Payments_PaymentId",
                table: "Payers",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Payments_PaymentId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Payers_Payments_PaymentId",
                table: "Payers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Payers");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "MerchantOwners");
        }
    }
}
