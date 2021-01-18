using Microsoft.EntityFrameworkCore.Migrations;

namespace ThanhTuan.IDP.Migrations
{
  public partial class userpersonalinformation : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "PersonId",
          table: "Employees");

      migrationBuilder.RenameColumn(
          name: "Person_Phone",
          table: "Employees",
          newName: "Phone");

      migrationBuilder.RenameColumn(
          name: "Person_LastName",
          table: "Employees",
          newName: "LastName");

      migrationBuilder.RenameColumn(
          name: "Person_IdentityNumber",
          table: "Employees",
          newName: "IdentityNumber");

      migrationBuilder.RenameColumn(
          name: "Person_Gender",
          table: "Employees",
          newName: "Gender");

      migrationBuilder.RenameColumn(
          name: "Person_FullName",
          table: "Employees",
          newName: "FullName");

      migrationBuilder.RenameColumn(
          name: "Person_FirstName",
          table: "Employees",
          newName: "FirstName");

      migrationBuilder.RenameColumn(
          name: "Person_Email",
          table: "Employees",
          newName: "Email");

      migrationBuilder.RenameColumn(
          name: "Person_DisplayName",
          table: "Employees",
          newName: "DisplayName");

      migrationBuilder.RenameColumn(
          name: "Person_Birthday",
          table: "Employees",
          newName: "Birthday");

      migrationBuilder.RenameColumn(
          name: "Person_Address",
          table: "Employees",
          newName: "Address");

      migrationBuilder.AlterColumn<int>(
          name: "Gender",
          table: "Employees",
          nullable: false,
          oldClrType: typeof(int),
          oldType: "integer",
          oldNullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.RenameColumn(
          name: "Phone",
          table: "Employees",
          newName: "Person_Phone");

      migrationBuilder.RenameColumn(
          name: "LastName",
          table: "Employees",
          newName: "Person_LastName");

      migrationBuilder.RenameColumn(
          name: "IdentityNumber",
          table: "Employees",
          newName: "Person_IdentityNumber");

      migrationBuilder.RenameColumn(
          name: "Gender",
          table: "Employees",
          newName: "Person_Gender");

      migrationBuilder.RenameColumn(
          name: "FullName",
          table: "Employees",
          newName: "Person_FullName");

      migrationBuilder.RenameColumn(
          name: "FirstName",
          table: "Employees",
          newName: "Person_FirstName");

      migrationBuilder.RenameColumn(
          name: "Email",
          table: "Employees",
          newName: "Person_Email");

      migrationBuilder.RenameColumn(
          name: "DisplayName",
          table: "Employees",
          newName: "Person_DisplayName");

      migrationBuilder.RenameColumn(
          name: "Birthday",
          table: "Employees",
          newName: "Person_Birthday");

      migrationBuilder.RenameColumn(
          name: "Address",
          table: "Employees",
          newName: "Person_Address");

      migrationBuilder.AlterColumn<int>(
          name: "Person_Gender",
          table: "Employees",
          type: "integer",
          nullable: true,
          oldClrType: typeof(int));

      migrationBuilder.AddColumn<int>(
          name: "PersonId",
          table: "Employees",
          type: "integer",
          nullable: false,
          defaultValue: 0);
    }
  }
}
