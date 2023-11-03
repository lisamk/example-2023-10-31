using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Example.Web.Migrations
{
    /// <inheritdoc />
    public partial class Registrations_EmailHash_EmailHashSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailHash",
                table: "Registrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailHashSalt",
                table: "Registrations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailHash",
                table: "Registrations");

            migrationBuilder.DropColumn(
                name: "EmailHashSalt",
                table: "Registrations");
        }
    }
}
