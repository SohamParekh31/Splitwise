using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.DomainModel.Migrations
{
    public partial class allownull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBooks_Settlements_SettlementId",
                table: "PaymentBooks");

            migrationBuilder.AlterColumn<int>(
                name: "SettlementId",
                table: "PaymentBooks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBooks_Settlements_SettlementId",
                table: "PaymentBooks",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "SettlementId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBooks_Settlements_SettlementId",
                table: "PaymentBooks");

            migrationBuilder.AlterColumn<int>(
                name: "SettlementId",
                table: "PaymentBooks",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBooks_Settlements_SettlementId",
                table: "PaymentBooks",
                column: "SettlementId",
                principalTable: "Settlements",
                principalColumn: "SettlementId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
