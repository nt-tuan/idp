using Microsoft.EntityFrameworkCore.Migrations;

namespace ThanhTuan.IDP.Migrations
{
  public partial class addmoreuserfields : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "Fullname",
          table: "AspNetUsers",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Image",
          table: "AspNetUsers",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "Nickname",
          table: "AspNetUsers",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Fullname",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "Image",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "Nickname",
          table: "AspNetUsers");
    }
  }
}
