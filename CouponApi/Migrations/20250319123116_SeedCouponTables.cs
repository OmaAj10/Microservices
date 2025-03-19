using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { "10OFF", 10.0, 20 });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 2, "20OFF", 20.0, 40 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1,
                columns: new[] { "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { "20OFF", 20.0, 40 });
        }
    }
}
