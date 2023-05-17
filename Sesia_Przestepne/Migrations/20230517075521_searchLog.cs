using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sesia_Przestepne.Migrations
{
    public partial class searchLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SearchTime",
                table: "People",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTime",
                table: "People");
        }
    }
}
