using Microsoft.EntityFrameworkCore.Migrations;

namespace Filed.PaymentGateway.Data.Migrations
{
    public partial class PaymentStateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentState_PaymentInformation_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentState_PaymentId",
                table: "PaymentState",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentState");
        }
    }
}
