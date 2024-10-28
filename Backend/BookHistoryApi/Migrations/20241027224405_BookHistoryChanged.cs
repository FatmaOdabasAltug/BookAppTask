using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookHistoryApi.Migrations
{
    /// <inheritdoc />
    public partial class BookHistoryChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangedProperty",
                table: "BookHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "BookHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "BookHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedProperty",
                table: "BookHistories");

            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "BookHistories");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "BookHistories");
        }
    }
}
