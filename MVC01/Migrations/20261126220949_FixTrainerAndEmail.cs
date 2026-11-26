using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC01.Migrations
{
    /// <inheritdoc />
    public partial class FixTrainerAndEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop Email Check Constraints temporarily
            migrationBuilder.Sql(@"
        DECLARE @sql NVARCHAR(MAX) = N'';

        SELECT @sql +=
            N'ALTER TABLE ' +
            QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + N'.' +
            QUOTENAME(OBJECT_NAME(parent_object_id)) +
            N' DROP CONSTRAINT ' + QUOTENAME(name) + N';'
        FROM sys.check_constraints
        WHERE parent_object_id IN (
            OBJECT_ID(N'dbo.Trainers'),
            OBJECT_ID(N'dbo.Members')
        )
        AND definition LIKE N'%Email%';

        EXEC sp_executesql @sql;
    ");

            // Remove old CreatedAt column
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Trainers");

            // Configure HireDate
            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            // Change Trainer Email
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Trainers",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Change Member Email
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Restore Email Check Constraints
            migrationBuilder.AddCheckConstraint(
                name: "CK_GymUser_Email1",
                table: "Trainers",
                sql: "Email LIKE '_%@_%._%'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_GymUser_Email",
                table: "Members",
                sql: "Email LIKE '_%@_%._%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HireDate",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Trainers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Trainers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);
        }
    }
}
