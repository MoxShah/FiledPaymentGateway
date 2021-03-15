using Microsoft.EntityFrameworkCore.Migrations;

namespace Filed.PaymentGateway.Data.Migrations
{
    public partial class PaymentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CardHolder",
                table: "PaymentInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "PaymentInformation",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "PaymentInformation");

            migrationBuilder.AlterColumn<string>(
                name: "CardHolder",
                table: "PaymentInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
