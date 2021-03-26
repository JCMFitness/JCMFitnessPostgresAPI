using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JCMFitnessPostgresAPI.Migrations
{
    public partial class Third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Users_UserID",
                table: "UserWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID",
                table: "UserWorkouts");

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

            migrationBuilder.AlterColumn<string>(
                name: "WorkoutID",
                table: "UserWorkouts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserWorkouts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    ExerciseID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TimerValue = table.Column<int>(type: "integer", nullable: false),
                    Repititions = table.Column<int>(type: "integer", nullable: false),
                    Sets = table.Column<int>(type: "integer", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.ExerciseID);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ExerciseID = table.Column<string>(type: "text", nullable: true),
                    WorkoutID = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Workouts_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkouts_UserID",
                table: "UserWorkouts",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseID",
                table: "WorkoutExercises",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutID",
                table: "WorkoutExercises",
                column: "WorkoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Users_UserID",
                table: "UserWorkouts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID",
                table: "UserWorkouts",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "WorkoutID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Users_UserID",
                table: "UserWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID",
                table: "UserWorkouts");

            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWorkouts",
                table: "UserWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_UserWorkouts_UserID",
                table: "UserWorkouts");

            migrationBuilder.AlterColumn<string>(
                name: "WorkoutID",
                table: "UserWorkouts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "UserWorkouts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Users_UserID",
                table: "UserWorkouts",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWorkouts_Workouts_WorkoutID",
                table: "UserWorkouts",
                column: "WorkoutID",
                principalTable: "Workouts",
                principalColumn: "WorkoutID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
