using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevSkill.Inventory.Web.Migrations.InventoryDb
{
    /// <inheritdoc />
    public partial class AddAINVendorGroupToProducta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AIN",
                table: "Productsa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Productsa",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Productsa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VendorId",
                table: "Productsa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Productsa_GroupId",
                table: "Productsa",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Productsa_VendorId",
                table: "Productsa",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productsa_Groups_GroupId",
                table: "Productsa",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Productsa_Vendors_VendorId",
                table: "Productsa",
                column: "VendorId",
                principalTable: "Vendors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productsa_Groups_GroupId",
                table: "Productsa");

            migrationBuilder.DropForeignKey(
                name: "FK_Productsa_Vendors_VendorId",
                table: "Productsa");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Productsa_GroupId",
                table: "Productsa");

            migrationBuilder.DropIndex(
                name: "IX_Productsa_VendorId",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "AIN",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Productsa");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Productsa");
        }
    }
}
