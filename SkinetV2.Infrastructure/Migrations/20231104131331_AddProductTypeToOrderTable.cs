using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkinetV2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTypeToOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemOrdered_ProductType",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemOrdered_ProductType",
                table: "OrderItems");
        }
    }
}
