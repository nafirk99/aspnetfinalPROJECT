using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class AddTquantityModelAquantityStockPLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableQuantity",
                table: "Productsa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Productsa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModelNumber",
                table: "Productsa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "StockPrice",
                table: "Productsa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Productsa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productsa_LocationId",
                table: "Productsa",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productsa_Locations_LocationId",
                table: "Productsa",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productsa_Locations_LocationId",
                table: "Productsa");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Productsa_LocationId",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "AvailableQuantity",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "ModelNumber",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "StockPrice",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Productsa");
        }
    }
}
