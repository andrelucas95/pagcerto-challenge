using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Nsu = table.Column<int>(nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: true),
                    ReprovedAt = table.Column<DateTime>(nullable: true),
                    Anticipated = table.Column<bool>(nullable: false),
                    AcquirerConfirmation = table.Column<bool>(nullable: false),
                    GrossAmount = table.Column<decimal>(nullable: false),
                    Fee = table.Column<decimal>(nullable: false),
                    CardFinal = table.Column<string>(maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTransactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Key = table.Column<int>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    GrossAmount = table.Column<decimal>(nullable: false),
                    NetAmount = table.Column<decimal>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    AnticipatedValue = table.Column<decimal>(nullable: true),
                    ReceiptDate = table.Column<DateTime>(nullable: false),
                    TransferedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_CardTransactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "CardTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_Key",
                table: "Installments",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Installments_TransactionId",
                table: "Installments",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "CardTransactions");
        }
    }
}
