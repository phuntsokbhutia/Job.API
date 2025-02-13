using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeyOnJobApply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_user_job_apply_job_id_user_id",
                table: "user_job_apply",
                columns: new[] { "job_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_job_apply_user_id",
                table: "user_job_apply",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_job_apply_job_details_job_id",
                table: "user_job_apply",
                column: "job_id",
                principalTable: "job_details",
                principalColumn: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_job_apply_users_user_id",
                table: "user_job_apply",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_job_apply_job_details_job_id",
                table: "user_job_apply");

            migrationBuilder.DropForeignKey(
                name: "FK_user_job_apply_users_user_id",
                table: "user_job_apply");

            migrationBuilder.DropIndex(
                name: "IX_user_job_apply_job_id_user_id",
                table: "user_job_apply");

            migrationBuilder.DropIndex(
                name: "IX_user_job_apply_user_id",
                table: "user_job_apply");
        }
    }
}
