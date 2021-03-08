using Microsoft.EntityFrameworkCore.Migrations;

namespace JCMFitnessPostgresAPI.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserWorkouts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts",
                columns: new[] { "UserID", "WorkoutID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "UserWorkouts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts",
                column: "Id");
        }
    }
}
