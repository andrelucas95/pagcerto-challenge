using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class AddNetAmountOnCardTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetAmount",
                table: "CardTransactions",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "CardTransactions");
        }
    }
}
