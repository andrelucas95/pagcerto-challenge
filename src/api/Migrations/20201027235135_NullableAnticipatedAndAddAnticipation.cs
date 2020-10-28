using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class NullableAnticipatedAndAddAnticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Anticipated",
                table: "CardTransactions",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "AnticipationId",
                table: "CardTransactions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Anticipations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    AnalysisStartedAt = table.Column<DateTime>(nullable: true),
                    AnalysisFinalizedAt = table.Column<DateTime>(nullable: true),
                    AnalysisStatus = table.Column<int>(nullable: false),
                    RequestedAmount = table.Column<decimal>(nullable: false),
                    AnticipatedAmount = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anticipations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTransactions_AnticipationId",
                table: "CardTransactions",
                column: "AnticipationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTransactions_Anticipations_AnticipationId",
                table: "CardTransactions",
                column: "AnticipationId",
                principalTable: "Anticipations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTransactions_Anticipations_AnticipationId",
                table: "CardTransactions");

            migrationBuilder.DropTable(
                name: "Anticipations");

            migrationBuilder.DropIndex(
                name: "IX_CardTransactions_AnticipationId",
                table: "CardTransactions");

            migrationBuilder.DropColumn(
                name: "AnticipationId",
                table: "CardTransactions");

            migrationBuilder.AlterColumn<bool>(
                name: "Anticipated",
                table: "CardTransactions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
