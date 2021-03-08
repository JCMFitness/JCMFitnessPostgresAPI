using Microsoft.EntityFrameworkCore.Migrations;

namespace JCMFitnessPostgresAPI.Migrations
{
    public partial class Fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Users_UserID1",
                table: "UserWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID1",
                table: "UserWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkouts_UserID1",
                table: "UserWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkouts_WorkoutID1",
                table: "UserWorkouts");

            migrationBuilder.DropColumn(
                name: "UserID1",
                table: "UserWorkouts");

            migrationBuilder.DropColumn(
                name: "WorkoutID1",
                table: "UserWorkouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID1",
                table: "UserWorkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WorkoutID1",
                table: "UserWorkouts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkouts_UserID1",
                table: "UserWorkouts",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkouts_WorkoutID1",
                table: "UserWorkouts",
                column: "WorkoutID1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Users_UserID1",
                table: "UserWorkouts",
                column: "UserID1",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID1",
                table: "UserWorkouts",
                column: "WorkoutID1",
                principalTable: "Workouts",
                principalColumn: "WorkoutID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
