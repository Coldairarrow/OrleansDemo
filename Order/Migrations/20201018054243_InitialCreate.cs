using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, comment: "主键"),
                    Name = table.Column<string>(nullable: true, comment: "商品名"),
                    TotalStock = table.Column<int>(nullable: false, comment: "总库存"),
                    UsedStock = table.Column<int>(nullable: false, comment: "已用库存")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Name", "TotalStock", "UsedStock" },
                values: new object[] { new Guid("7e1f54d9-ab72-e583-4375-0565349c3982"), "商品", 10000, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
