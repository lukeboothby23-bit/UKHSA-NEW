using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UKHSA.Migrations
{
    /// <inheritdoc />
    public partial class AddReasonToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Requests",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "Approvals",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Requests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Expires",
                table: "Approvals",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
